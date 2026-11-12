(() => {
  'use strict';

  /* ---------- utilidades ---------- */
  const $ = (s, el = document) => el.querySelector(s);
  const app = $('#app');

  const ESC = { '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' };
  const esc = (s) => String(s == null ? '' : s).replace(/[&<>"']/g, (c) => ESC[c]);
  const raw = (s) => ({ __raw: s });
  // Plantilla que escapa todo lo interpolado salvo raw(...): los datos clinicos nunca se inyectan como HTML.
  const h = (strings, ...vals) =>
    strings.reduce((a, s, i) => a + s + (i < vals.length ? (vals[i] && vals[i].__raw !== undefined ? vals[i].__raw : esc(vals[i])) : ''), '');
  const join = (arr) => raw(arr.join(''));

  const store = {
    get(k) { try { return localStorage.getItem(k); } catch (e) { return null; } },
    set(k, v) { try { localStorage.setItem(k, v); } catch (e) { /* sin almacenamiento */ } },
    del(k) { try { localStorage.removeItem(k); } catch (e) { /* idem */ } }
  };
  const K_TOKEN = 'pac.token';
  const K_HIST = 'pac.histSize2';

  const isDesktop = () => window.matchMedia('(min-width: 960px)').matches;

  const norm = (s) => String(s || '').toLowerCase().normalize('NFD').replace(/[̀-ͯ]/g, '');

  /* ---------- fechas ---------- */
  const pad = (n) => String(n).padStart(2, '0');
  const ymd = (d) => `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`;
  const parseYmd = (s) => { const [y, m, d] = s.split('-').map(Number); return new Date(y, m - 1, d); };
  const addDays = (d, n) => { const x = new Date(d); x.setDate(x.getDate() + n); return x; };
  const toLocalInput = (d) => `${ymd(d)}T${pad(d.getHours())}:${pad(d.getMinutes())}`;
  const fmtHora = (d) => `${pad(d.getHours())}:${pad(d.getMinutes())}`;
  const cap = (s) => s.charAt(0).toUpperCase() + s.slice(1);
  const fmtFechaLarga = (d) => cap(d.toLocaleDateString('es-AR', { weekday: 'long', day: 'numeric', month: 'long', year: 'numeric' }));
  const fmtFechaCorta = (d) => cap(d.toLocaleDateString('es-AR', Object.assign({ weekday: 'short', day: 'numeric', month: 'short' }, d.getFullYear() === new Date().getFullYear() ? {} : { year: 'numeric' })).replace(/./g, '').replace(',', ''));
  const edad = (iso) => {
    if (!iso) return null;
    const n = new Date(iso), t = new Date();
    let a = t.getFullYear() - n.getFullYear();
    if (t.getMonth() < n.getMonth() || (t.getMonth() === n.getMonth() && t.getDate() < n.getDate())) a--;
    return a >= 0 ? a : null;
  };

  /* ---------- iconos ---------- */
  const I = (p) => raw(`<svg class="icon" viewBox="0 0 24 24" aria-hidden="true">${p}</svg>`);
  const ICON = {
    agenda: I('<rect x="3" y="4" width="18" height="17" rx="2"/><path d="M8 2v4M16 2v4M3 10h18"/>'),
    users: I('<circle cx="9" cy="8" r="3.5"/><path d="M2.5 20c.6-3.5 3.3-5.5 6.5-5.5s5.9 2 6.5 5.5"/><path d="M16 4.7a3.5 3.5 0 0 1 0 6.6M18 14.8c2 .7 3.2 2.3 3.5 5.2"/>'),
    back: I('<path d="M15 5l-7 7 7 7"/>'),
    next: I('<path d="M9 5l7 7-7 7"/>'),
    plus: I('<path d="M12 5v14M5 12h14"/>'),
    search: I('<circle cx="11" cy="11" r="7"/><path d="M20 20l-3.5-3.5"/>'),
    edit: I('<path d="M4 20h4L19 9l-4-4L4 16z"/>'),
    out: I('<path d="M9 4H5a1 1 0 0 0-1 1v14a1 1 0 0 0 1 1h4M16 8l4 4-4 4M20 12H9"/>')
  };

  /* ---------- API ---------- */
  let token = store.get(K_TOKEN);

  async function api(method, path, body) {
    let res;
    try {
      res = await fetch('/api' + path, {
        method,
        headers: Object.assign({ 'Content-Type': 'application/json' }, token ? { Authorization: 'Bearer ' + token } : {}),
        body: body === undefined ? undefined : JSON.stringify(body)
      });
    } catch (e) {
      throw new Error('No se pudo conectar con el servidor. Verifique que la PC del consultorio esté encendida y que esté en la misma red.');
    }
    const text = await res.text();
    let data = null;
    if (text) { try { data = JSON.parse(text); } catch (e) { /* no era JSON */ } }
    if (res.status === 401 && !path.startsWith('/auth/')) {
      logout(false);
      throw new Error('La sesión venció. Ingrese nuevamente.');
    }
    if (!res.ok) {
      const err = new Error((data && data.error) || 'Error ' + res.status);
      err.status = res.status;
      throw err;
    }
    return data;
  }

  function setToken(t) { token = t; store.set(K_TOKEN, t); }
  function logout(go = true) { token = null; store.del(K_TOKEN); cache.pacientes = null; if (go) location.hash = '#/login'; else if (!/^#\/(login|vincular)/.test(location.hash)) location.hash = '#/login'; }

  const cache = { pacientes: null };
  async function pacientes(force) {
    if (!cache.pacientes || force) cache.pacientes = await api('GET', '/pacientes');
    return cache.pacientes;
  }

  /* ---------- toast / dialogo ---------- */
  let toastTimer;
  function toast(msg, err) {
    const t = $('#toast');
    t.textContent = msg;
    t.className = 'show' + (err ? ' err' : '');
    clearTimeout(toastTimer);
    toastTimer = setTimeout(() => { t.className = ''; }, err ? 5000 : 2500);
  }
  const fail = (e) => toast(e.message || 'Error inesperado', true);

  function confirmar({ titulo, texto, ok = 'Aceptar', peligro = false, casilla = null }) {
    return new Promise((resolve) => {
      const d = $('#dlg');
      d.innerHTML = h`
        <h2>${titulo}</h2>
        <p>${texto}</p>
        ${casilla ? raw(h`<label class="check"><input type="checkbox" id="dlg-chk"> ${casilla}</label>`) : ''}
        <div class="actions">
          <button class="btn" value="cancel" type="button" id="dlg-no">Cancelar</button>
          <button class="btn ${peligro ? 'danger' : 'primary'}" type="button" id="dlg-si">${ok}</button>
        </div>`;
      const fin = (v) => { const c = $('#dlg-chk'); const chk = c ? c.checked : false; d.close(); resolve(v ? { ok: true, casilla: chk } : { ok: false }); };
      $('#dlg-no').onclick = () => fin(false);
      $('#dlg-si').onclick = () => fin(true);
      d.oncancel = (e) => { e.preventDefault(); fin(false); };
      d.showModal();
    });
  }

  /* ---------- estructura comun ---------- */
  const LOGO = 'icons/icon-192.png';

  function shell(activo, contenido) {
    return h`
      <div class="shell">
        <header class="topbar">
          <img src="${LOGO}" alt="">
          <span class="title">Pacientes</span>
          <button class="btn-icon" data-act="salir" aria-label="Cerrar sesión">${ICON.out}</button>
        </header>
        <aside class="rail">
          <div class="brand"><img src="${LOGO}" alt="">Pacientes</div>
          <a href="#/agenda" ${activo === 'agenda' ? raw('aria-current="page"') : ''}>${ICON.agenda} Agenda</a>
          <a href="#/pacientes" ${activo === 'pacientes' ? raw('aria-current="page"') : ''}>${ICON.users} Pacientes</a>
          <div class="spacer"></div>
          <button class="navlink" data-act="salir">${ICON.out} Cerrar sesión</button>
        </aside>
        <main class="main">${raw(contenido)}</main>
        <nav class="tabbar" aria-label="Principal">
          <a href="#/agenda" ${activo === 'agenda' ? raw('aria-current="page"') : ''}>${ICON.agenda}Agenda</a>
          <a href="#/pacientes" ${activo === 'pacientes' ? raw('aria-current="page"') : ''}>${ICON.users}Pacientes</a>
        </nav>
      </div>`;
  }

  const cargando = () => '<div class="spinner">Cargando…</div>';
  const errorBox = (m) => h`<div class="error-box">${m}</div><div class="actions"><button class="btn" data-act="reintentar">Reintentar</button></div>`;
  const render = (html) => { app.innerHTML = html; };

  /* ---------- router ---------- */
  function parseHash() {
    const raw = location.hash.replace(/^#/, '') || '/agenda';
    const [path, qs] = raw.split('?');
    return { parts: path.split('/').filter(Boolean), query: new URLSearchParams(qs || ''), path };
  }

  let navToken = 0;
  async function route() {
    const my = ++navToken;
    const { parts, query } = parseHash();
    const [a, b, c, d] = parts;

    if (a === 'vincular' && b) return vincular(b);
    if (a === 'login') return vistaLogin();
    if (!token) { location.hash = '#/login'; return; }

    try {
      if (a === 'agenda' || !a) return await vistaAgenda(query.get('fecha'), my);
      if (a === 'sesiones' && b === 'nueva') return await formSesion({ pid: null, sid: null, fecha: query.get('fecha') });
      if (a === 'pacientes') {
        if (!b) return await vistaPacientes(null, my);
        if (b === 'nuevo') return await formPaciente(null);
        const id = Number(b);
        if (!c) return await vistaPacientes(id, my);
        if (c === 'editar') return await formPaciente(id);
        if (c === 'sesiones' && d === 'nueva') return await formSesion({ pid: id, sid: null, fecha: query.get('fecha') });
        if (c === 'sesiones' && d) return await formSesion({ pid: id, sid: Number(d) });
      }
      location.hash = '#/agenda';
    } catch (e) {
      if (my !== navToken) return;
      render(shell('', errorBox(e.message)));
    }
  }
  window.addEventListener('hashchange', route);

  /* ---------- login / vincular ---------- */
  function vistaLogin(msg) {
    render(h`
      <div class="login">
        <form class="card" id="f-login" autocomplete="on" method="post" action="#/login">
          <img src="${LOGO}" alt="">
          <h1>Pacientes</h1>
          <input class="sr" type="text" name="username" autocomplete="username" value="consultorio" tabindex="-1" aria-hidden="true" readonly>
          <div class="field">
            <label for="pin">PIN de acceso</label>
            <input id="pin" name="password" type="password" autocomplete="current-password" required autofocus>
          </div>
          ${msg ? raw(h`<div class="error-box">${msg}</div>`) : ''}
          <button class="btn primary block" type="submit">Ingresar</button>
          <p class="small">Si es la primera vez en este teléfono, pida el código QR en la PC del consultorio
            (abrir <b>http://localhost:${esc(location.port || 80)}/vincular.html</b>) y escanéelo con la cámara.</p>
        </form>
      </div>`);
    $('#f-login').addEventListener('submit', async (ev) => {
      ev.preventDefault();
      const btn = ev.target.querySelector('button[type=submit]');
      btn.disabled = true;
      try {
        const r = await api('POST', '/auth/login', { pin: $('#pin').value });
        setToken(r.token);
        location.hash = '#/agenda';
      } catch (e) { vistaLogin(e.message); }
    });
  }

  async function vincular(codigo) {
    render('<div class="login"><div class="card"><div class="spinner">Vinculando este dispositivo…</div></div></div>');
    try {
      const r = await api('POST', '/auth/vincular', { codigo });
      setToken(r.token);
      history.replaceState(null, '', location.pathname + '#/agenda');
      route();
    } catch (e) {
      history.replaceState(null, '', location.pathname + '#/login');
      vistaLogin(e.message);
    }
  }

  /* ---------- agenda ---------- */
  async function vistaAgenda(fechaStr, my) {
    const hoy = ymd(new Date());
    const fecha = fechaStr || hoy;
    const d = parseYmd(fecha);
    render(shell('agenda', cargando()));
    const turnos = await api('GET', '/agenda?fecha=' + fecha);
    if (my !== navToken) return;

    const lista = turnos.length
      ? h`<ul class="agenda">${join(turnos.map((t) => {
          const f = new Date(t.fechaHora);
          return h`<li><a class="turno" href="#/pacientes/${t.pacienteId}">
              <span class="hora">${fmtHora(f)}</span>
              <div><div class="quien">${t.pacienteNombre || 'Paciente'}</div>
              ${t.notas ? raw(h`<div class="nota">${t.notas}</div>`) : ''}</div></a></li>`;
        }))}</ul>`
      : h`<div class="empty"><strong>Sin sesiones este día</strong>Use “Nueva sesión” para agendar una.</div>`;

    render(shell('agenda', h`
      <div class="page-head"><h1>Agenda</h1>
        <a class="btn primary" href="#/sesiones/nueva?fecha=${fecha}">${ICON.plus} Nueva sesión</a></div>
      <div class="daynav">
        <a class="btn-icon" href="#/agenda?fecha=${ymd(addDays(d, -1))}" aria-label="Día anterior">${ICON.back}</a>
        <div class="date">${fmtFechaLarga(d)}${fecha === hoy ? raw('<small>Hoy</small>') : ''}</div>
        <a class="btn-icon" href="#/agenda?fecha=${ymd(addDays(d, 1))}" aria-label="Día siguiente">${ICON.next}</a>
      </div>
      ${fecha !== hoy ? raw(h`<p style="margin-bottom:1rem"><a class="btn sm" href="#/agenda">Ir a hoy</a></p>`) : ''}
      ${raw(lista)}`));
  }

  /* ---------- pacientes (lista + ficha) ---------- */
  let busqueda = '';

  function itemsPacientes(lista, sel) {
    const terms = norm(busqueda).split(/\s+/).filter(Boolean);
    const f = lista.filter((p) => { const t = norm(`${p.nombreApellido} ${p.dni} ${p.nroTelefono}`); return terms.every((x) => t.includes(x)); });
    const cuenta = `${f.length} ${f.length === 1 ? 'paciente' : 'pacientes'}`;
    const html = f.length
      ? h`<ul class="plist">${join(f.map((p) => h`<li><a href="#/pacientes/${p.id}" ${p.id === sel ? raw('aria-current="true"') : ''}>
            <div class="n">${p.nombreApellido}</div>
            <div class="s">${[p.dni && 'DNI ' + p.dni, p.nroTelefono].filter(Boolean).join(' · ') || 'Sin datos de contacto'}</div></a></li>`))}</ul>`
      : h`<div class="empty"><strong>Sin resultados</strong>Pruebe con otro nombre o DNI.</div>`;
    return { cuenta, html };
  }

  async function vistaPacientes(id, my) {
    const montado = $('.split');
    if (montado && cache.pacientes) {
      // Ya esta la lista en pantalla: solo cambia el detalle (la lista conserva su scroll y busqueda).
      montado.classList.toggle('has-detail', !!id);
      document.querySelectorAll('#plist a').forEach((a) => {
        if (a.getAttribute('href') === '#/pacientes/' + id) a.setAttribute('aria-current', 'true'); else a.removeAttribute('aria-current');
      });
      $('#detalle').innerHTML = id ? cargando() : '<div class="placeholder"><div><strong style="font-size:1.4rem;display:block">Seleccione un paciente</strong>para ver su historial</div></div>';
      if (id) await fichaPaciente(id, my);
      return;
    }
    render(shell('pacientes', cargando()));
    const lista = await pacientes(true);
    if (my !== navToken) return;
    const r = itemsPacientes(lista, id);

    render(shell('pacientes', h`
      <div class="split ${id ? 'has-detail' : ''}">
        <section class="list-pane" aria-label="Pacientes">
          <div class="page-head" style="margin-bottom:.8rem"><h1>Pacientes</h1>
            <a class="btn primary sm" href="#/pacientes/nuevo">${ICON.plus} Nuevo</a></div>
          <div class="search">${ICON.search}
            <input type="search" id="q" placeholder="Buscar paciente…" value="${busqueda}" autocomplete="off" enterkeyhint="search"></div>
          <div class="count" id="cuenta">${r.cuenta}</div>
          <div class="plist-wrap" id="plist">${raw(r.html)}</div>
        </section>
        <section class="detail-pane" id="detalle" aria-label="Ficha del paciente">
          ${id ? raw(cargando()) : raw('<div class="placeholder"><div><strong style="font-size:1.4rem;display:block">Seleccione un paciente</strong>para ver su historial</div></div>')}
        </section>
      </div>`));

    $('#q').addEventListener('input', (e) => {
      busqueda = e.target.value;
      const x = itemsPacientes(cache.pacientes, id);
      $('#cuenta').textContent = x.cuenta;
      $('#plist').innerHTML = x.html;
    });

    if (id) await fichaPaciente(id, my);
  }

  function histSize() {
    const v = parseInt(store.get(K_HIST), 10);
    return v >= 16 && v <= 160 ? v : (isDesktop() ? 64 : 32);
  }

  async function fichaPaciente(id, my) {
    const box = $('#detalle');
    let p;
    try { p = await api('GET', '/pacientes/' + id); } catch (e) {
      if (my !== navToken) return;
      box.innerHTML = e.status === 404 ? h`<div class="empty"><strong>Paciente no encontrado</strong></div>` : errorBox(e.message);
      return;
    }
    if (my !== navToken) return;
    current.paciente = p;
    const e = edad(p.diaNacimiento);
    const meta = [e != null && `${e} años`, p.dni && `DNI ${p.dni}`].filter(Boolean);

    const sesiones = p.sesiones || [];
    const mas = [['Teléfono', p.nroTelefono], ['Género', p.genero], ['Dirección', p.direccion], ['Localidad', p.localidad],
      ['Nacimiento', p.diaNacimiento && new Date(p.diaNacimiento).toLocaleDateString('es-AR')]].filter((x) => x[1]);

    box.innerHTML = h`
      <div class="ficha">
        <div class="ficha-head">
          <a class="btn-icon back back-m" href="#/pacientes" aria-label="Volver a la lista">${ICON.back}</a>
          <div class="who">
            <h1 class="nombre">${p.nombreApellido}</h1>
            <p class="meta">${meta.join(' · ')}${meta.length ? ' · ' : ''}<span class="obs" id="obs">${raw(htmlObs(p))}</span></p>
          </div>
          <a class="btn sm" href="#/pacientes/${p.id}/editar" aria-label="Editar paciente">${ICON.edit}<span class="lbl">Editar</span></a>
        </div>

        <section class="hist" id="hist" style="--hist:${histSize()}px">
          <div class="hist-head">
            <h2>Historial <small>(${sesiones.length})</small></h2>
            <div class="zoom" role="group" aria-label="Tamaño del texto">
              <button type="button" data-act="zoom-" aria-label="Achicar texto">A−</button>
              <button type="button" data-act="zoom+" aria-label="Agrandar texto">A+</button>
            </div>
            <a class="btn primary sm" href="#/pacientes/${p.id}/sesiones/nueva">${ICON.plus}<span><span class="lbl-l">Nueva </span>sesión</span></a>
          </div>
          ${sesiones.length
            ? raw(h`<ol class="entries">${join([...sesiones].sort((a, b) => new Date(a.fechaHora) - new Date(b.fechaHora)).map((s) => {
                const f = new Date(s.fechaHora);
                return h`<li class="entry">
                    <div class="entry-top"><span class="cuando">${fmtFechaCorta(f)} · ${fmtHora(f)}</span>
                      <a class="edit" href="#/pacientes/${p.id}/sesiones/${s.id}" aria-label="Editar sesión">${ICON.edit}</a></div>
                    <div class="notas ${s.notas ? '' : 'vacia'}">${s.notas || 'Sin notas'}</div></li>`;
              }))}</ol>`)
            : raw(h`<div class="empty"><strong>Sin sesiones todavía</strong>Agregue la primera con “Nueva sesión”.</div>`)}
        </section>

        ${mas.length ? raw(h`<details class="mas"><summary>Más datos</summary>
          <dl class="kv">${join(mas.map((x) => h`<dt>${x[0]}</dt><dd>${x[1]}</dd>`))}</dl></details>`) : ''}
      </div>`;
  }

  const htmlObs = (p) => h`
    <span class="obs-text ${p.observaciones ? '' : 'vacia'}">${p.observaciones || 'Sin observaciones'}</span>
    <button class="btn-icon obs-edit" data-act="obs-editar" aria-label="Editar observaciones">${ICON.edit}</button>`;

  const current = { paciente: null };

  /* ---------- formularios ---------- */
  async function formPaciente(id) {
    let p = { nombreApellido: '', dni: '', nroTelefono: '', genero: '', direccion: '', localidad: '', diaNacimiento: null, observaciones: '' };
    if (id) { render(shell('pacientes', cargando())); p = await api('GET', '/pacientes/' + id); }
    const fnac = p.diaNacimiento ? ymd(new Date(p.diaNacimiento)) : '';
    const volver = id ? `#/pacientes/${id}` : '#/pacientes';

    render(shell('pacientes', h`
      <div class="page-head"><a class="btn-icon back" href="${volver}" aria-label="Volver">${ICON.back}</a>
        <h1>${id ? 'Editar paciente' : 'Nuevo paciente'}</h1></div>
      <form class="form" id="f-pac" autocomplete="off">
        <div class="field"><label for="nombre">Nombre y apellido *</label>
          <input id="nombre" type="text" required maxlength="200" value="${p.nombreApellido}" autocapitalize="words"></div>
        <div class="row2">
          <div class="field"><label for="dni">DNI</label><input id="dni" type="text" inputmode="numeric" value="${p.dni}"></div>
          <div class="field"><label for="tel">Teléfono</label><input id="tel" type="tel" value="${p.nroTelefono}"></div>
        </div>
        <div class="row2">
          <div class="field"><label for="nac">Fecha de nacimiento</label><input id="nac" type="date" value="${fnac}"></div>
          <div class="field"><label for="gen">Género</label>
            <input id="gen" type="text" list="generos" value="${p.genero}"><datalist id="generos"><option>Femenino</option><option>Masculino</option><option>Otro</option></datalist></div>
        </div>
        <div class="row2">
          <div class="field"><label for="dir">Dirección</label><input id="dir" type="text" value="${p.direccion}"></div>
          <div class="field"><label for="loc">Localidad</label><input id="loc" type="text" value="${p.localidad}"></div>
        </div>
        <div class="field"><label for="obs">Observaciones</label><textarea id="obs">${p.observaciones}</textarea></div>
        <div class="actions">
          <button class="btn primary grow" type="submit">Guardar</button>
          <a class="btn" href="${volver}">Cancelar</a>
          ${id ? raw('<button class="btn danger" type="button" data-act="del-paciente">Eliminar</button>') : ''}
        </div>
      </form>`));

    $('#f-pac').addEventListener('submit', async (ev) => {
      ev.preventDefault();
      const btn = ev.target.querySelector('button[type=submit]');
      btn.disabled = true;
      const dto = {
        nombreApellido: $('#nombre').value.trim(), dni: $('#dni').value.trim() || null, nroTelefono: $('#tel').value.trim() || null,
        genero: $('#gen').value.trim() || null, direccion: $('#dir').value.trim() || null, localidad: $('#loc').value.trim() || null,
        diaNacimiento: $('#nac').value ? $('#nac').value + 'T00:00:00' : null, observaciones: $('#obs').value || null
      };
      try {
        const r = id ? await api('PUT', '/pacientes/' + id, dto) : await api('POST', '/pacientes', dto);
        cache.pacientes = null;
        toast('Paciente guardado');
        location.hash = '#/pacientes/' + r.id;
      } catch (e) { btn.disabled = false; fail(e); }
    });

    current.delPaciente = async () => {
      const n = (p.sesiones || []).length;
      const r = await confirmar({
        titulo: 'Eliminar paciente', ok: 'Eliminar', peligro: true,
        texto: `Se eliminará a ${p.nombreApellido}. Esta acción no se puede deshacer.`,
        casilla: n ? `Eliminar también sus ${n} sesiones` : null
      });
      if (!r.ok) return;
      if (n && !r.casilla) { toast('Para eliminarlo, marque la casilla de eliminar también las sesiones.', true); return; }
      try { await api('DELETE', `/pacientes/${id}?eliminarSesiones=${n ? 'true' : 'false'}`); cache.pacientes = null; toast('Paciente eliminado'); location.hash = '#/pacientes'; }
      catch (e) { fail(e); }
    };
  }

  async function formSesion({ pid, sid, fecha }) {
    render(shell(pid ? 'pacientes' : 'agenda', cargando()));
    let sesion = null, pac = null;
    if (pid) {
      pac = await api('GET', '/pacientes/' + pid);
      if (sid) { sesion = (pac.sesiones || []).find((s) => s.id === sid); if (!sesion) throw new Error('Sesión no encontrada'); }
    }
    const lista = pid ? null : await pacientes(true);

    let f;
    if (sesion) f = new Date(sesion.fechaHora);
    else {
      f = fecha ? parseYmd(fecha) : new Date();
      const n = new Date();
      f.setHours(fecha && fecha !== ymd(n) ? 9 : n.getHours(), fecha && fecha !== ymd(n) ? 0 : (Math.ceil(n.getMinutes() / 5) * 5) % 60, 0, 0);
    }
    const volver = pid ? `#/pacientes/${pid}` : '#/agenda' + (fecha ? '?fecha=' + fecha : '');
    let elegido = pid;

    render(shell(pid ? 'pacientes' : 'agenda', h`
      <div class="page-head"><a class="btn-icon back" href="${volver}" aria-label="Volver">${ICON.back}</a>
        <h1>${sesion ? 'Editar sesión' : 'Nueva sesión'}</h1></div>
      <form class="form form-sesion" id="f-ses" autocomplete="off">
        ${pid
          ? raw(h`<div class="field"><label>Paciente</label><div class="card" style="padding:.7rem 1rem"><b>${pac.nombreApellido}</b></div></div>`)
          : raw(h`<div class="field"><label for="qp">Paciente *</label>
              <div class="picker"><div class="search">${ICON.search}<input id="qp" type="search" placeholder="Buscar paciente…" autocomplete="off"></div>
              <div id="pick"></div></div></div>`)}
        <div class="field"><label for="fh">Fecha y hora</label><input id="fh" type="datetime-local" required value="${toLocalInput(f)}"></div>
        <div class="field"><label for="notas">Notas de la sesión</label>
          <textarea id="notas" maxlength="4500" autofocus>${sesion ? sesion.notas || '' : ''}</textarea>
          <span class="hint" id="cnt"></span></div>
        <div class="actions">
          <button class="btn primary grow" type="submit">Guardar</button>
          <a class="btn" href="${volver}">Cancelar</a>
          ${sesion ? raw('<button class="btn danger" type="button" data-act="del-sesion">Eliminar</button>') : ''}
        </div>
      </form>`));

    const cnt = () => { $('#cnt').textContent = `${$('#notas').value.length} / 4500`; };
    $('#notas').addEventListener('input', cnt); cnt();

    if (!pid) {
      const pintar = () => {
        const terms = norm($('#qp').value).split(/\s+/).filter(Boolean);
        const res = lista.filter((p) => { const t = norm(`${p.nombreApellido} ${p.dni}`); return terms.every((x) => t.includes(x)); }).slice(0, 6);
        $('#pick').innerHTML = res.map((p) => h`<button type="button" class="btn block" data-act="pick" data-id="${p.id}" style="justify-content:flex-start;${raw(p.id === elegido ? 'border-color:var(--accent);background:var(--accent-soft)' : '')}">${p.nombreApellido}${p.dni ? raw(h` <span class="muted">· ${p.dni}</span>`) : ''}</button>`).join('');
      };
      $('#qp').addEventListener('input', pintar); pintar();
      current.pick = (id) => { elegido = id; pintar(); };
    }

    $('#f-ses').addEventListener('submit', async (ev) => {
      ev.preventDefault();
      if (!elegido) { toast('Elija un paciente', true); return; }
      const btn = ev.target.querySelector('button[type=submit]');
      btn.disabled = true;
      const dto = { fechaHora: $('#fh').value.length === 16 ? $('#fh').value + ':00' : $('#fh').value, notas: $('#notas').value || null };
      try {
        if (sesion) await api('PUT', `/pacientes/${pid}/sesiones/${sid}`, dto);
        else await api('POST', `/pacientes/${elegido}/sesiones`, dto);
        toast('Sesión guardada');
        location.hash = pid ? '#/pacientes/' + pid : '#/agenda?fecha=' + dto.fechaHora.slice(0, 10);
      } catch (e) { btn.disabled = false; fail(e); }
    });

    current.delSesion = async () => {
      const r = await confirmar({ titulo: 'Eliminar sesión', texto: 'Se eliminará esta sesión y sus notas. No se puede deshacer.', ok: 'Eliminar', peligro: true });
      if (!r.ok) return;
      try { await api('DELETE', `/pacientes/${pid}/sesiones/${sid}`); toast('Sesión eliminada'); location.hash = '#/pacientes/' + pid; }
      catch (e) { fail(e); }
    };
  }

  /* ---------- acciones (delegacion de eventos) ---------- */
  const actions = {
    salir() { logout(true); },
    reintentar() { route(); },
    'zoom+'() { zoom(6); },
    'zoom-'() { zoom(-6); },
    'del-paciente'() { current.delPaciente(); },
    'del-sesion'() { current.delSesion(); },
    pick(el) { current.pick(Number(el.dataset.id)); },
    'obs-editar'() {
      const p = current.paciente;
      const d = $('#dlg');
      d.innerHTML = h`
        <h2>Observaciones</h2>
        <textarea id="obs-ta" style="min-height:10rem;width:100%">${p.observaciones || ''}</textarea>
        <div class="actions" style="margin-top:1rem">
          <button class="btn" type="button" id="dlg-no">Cancelar</button>
          <button class="btn primary" type="button" id="dlg-si">Guardar</button>
        </div>`;
      d.oncancel = null;
      $('#dlg-no').onclick = () => d.close();
      $('#dlg-si').onclick = async (ev) => {
        const btn = ev.currentTarget;
        const v = $('#obs-ta').value;
        btn.disabled = true;
        try {
          await api('PUT', `/pacientes/${p.id}/observaciones`, { observaciones: v });
          p.observaciones = v;
          $('#obs').innerHTML = htmlObs(p);
          d.close();
          toast('Observaciones guardadas');
        } catch (e) { btn.disabled = false; fail(e); }
      };
      d.showModal();
      $('#obs-ta').focus();
    }
  };

  function zoom(delta) {
    const el = $('#hist');
    if (!el) return;
    const n = Math.max(16, Math.min(160, (parseInt(getComputedStyle(el).getPropertyValue('--hist'), 10) || histSize()) + delta));
    el.style.setProperty('--hist', n + 'px');
    store.set(K_HIST, String(n));
  }

  document.addEventListener('click', (ev) => {
    const el = ev.target.closest('[data-act]');
    if (el && actions[el.dataset.act]) { ev.preventDefault(); actions[el.dataset.act](el); }
  });

  /* ---------- arranque ---------- */
  route();
})();
