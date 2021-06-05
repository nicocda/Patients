using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repositories
{
    public class PacienteRepositorio
    {
        public DatabaseContext Context;
        public PacienteRepositorio()
        {
            Context = new DatabaseContext();
            Context.Database.EnsureCreated();
        }

        public List<Paciente> ObtenerTodos()
        {
            return Context.Pacientes.ToList();
        }

        public Paciente ObtenerUno(int id)
        {
            return Context.Pacientes.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Guardar(Paciente p)
        {
            if(p.Id == 0)
                Context.Add(p);
            Context.SaveChanges();
        }

        internal Paciente ObtenerPorNombreyDni(string nombreApellido, string dni)
        {
            return Context.Pacientes.FirstOrDefault(x => x.NombreApellido == nombreApellido && x.DNI == dni);

        }
    }
}
