﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Threading.Tasks;

namespace Server.Repositories
{
    public class PacienteRepositorio
    {
        public DatabaseContext Context;
        public PacienteRepositorio()
        {
            Context = new DatabaseContext();
        }

        public PacienteRepositorio(string connectionString)
        {
            Context = new DatabaseContext(connectionString);
        }


        public List<Paciente> ObtenerTodos()
        {
            return Context.Pacientes.ToList();
        }

        public Paciente ObtenerUno(int id)
        {
            return Context.Pacientes.Include("Sesiones")
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public void Guardar(Paciente p)
        {
            if (p.Id == 0)
                Context.Pacientes.Add(p);
            else
                Context.Entry<Paciente>(p).State = System.Data.Entity.EntityState.Modified;
            
            Context.SaveChanges();
        }

        internal List<Sesion> BuscarSesionesDia(DateTime dia)
        {
            DateTime finDia = dia.AddDays(1);
            return Context.Sesiones.Include("Paciente").Where(x => x.FechaHora > dia && x.FechaHora < finDia).OrderBy(x => x.FechaHora).ToList();
        }

        internal Paciente ObtenerPorNombreyDni(string nombreApellido, string dni)
        {
            return Context.Pacientes.FirstOrDefault(x => x.NombreApellido == nombreApellido && x.DNI == dni);

        }

        internal Paciente ObtenerPacienteDni(string dni)
        {
            return Context.Pacientes.FirstOrDefault(x => x.DNI == dni);
        }

        internal List<Paciente> ObtenerPacienteNombre(string nombre)
        {
            //List<string> claves = nombre.ToLower().Trim().Split(' ').ToList();
            //var query = Context.Pacientes
            //   .Where(x => claves.All(c => x.NombreApellido.ToLower().Trim().Contains(c)));
            string filter = nombre.ToLower().Trim();
            return Context.Pacientes.Where(x => x.NombreApellido.ToLower().Contains(filter)).ToList();

        }

        internal void EliminarPaciente(Paciente p)
        {
            Context.Pacientes.Attach(p);
            Context.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
        }

        internal void EliminarSesiones(Sesion s)
        {
            EliminarSesiones(new List<Sesion>() { s });
        }

        internal void EliminarSesiones(List<Sesion> sesiones)
        {
            for (int i = 0; sesiones.Count > i;i++)
            {
                Context.Sesiones.Attach(sesiones[i]);
                Context.Entry(sesiones[i]).State = System.Data.Entity.EntityState.Deleted;
            }
            Context.SaveChanges();
        }

        internal Paciente ObtenerPacienteId(int id)
        {
            return Context.Pacientes.FirstOrDefault(x => x.Id == id);
        }
    }
}
