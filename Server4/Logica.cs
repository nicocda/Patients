﻿using Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Logica
    {

        PacienteRepositorio Repositorio;
        //public Logica()
        //{
        //    Repositorio = new PacienteRepositorio();
        //}

        public Logica(string databaseName)
        {
            Repositorio = new PacienteRepositorio(databaseName);
        }
        public void AgregarSesion(Sesion sesion, int pacienteId)
        {
            if (sesion == null)
                throw new Exception("No hay sesion");
            Paciente p = ObtenerPaciente(pacienteId);

            p.AgregarSesion(sesion);

            Repositorio.Guardar(p);
        }

        public List<Sesion> ObtenerSesionesDia(DateTime dia)
        {
            return Repositorio.BuscarSesionesDia(dia);
        }

        public List<Paciente> ObtenerPacientes()
        {
            try
            {
                return Repositorio.ObtenerTodos();
            }
            catch
            {
                return new List<Paciente>();
            }
        }

        public List<Sesion> ListarSesiones(int pacienteId)
        {
            try
            {
                Paciente p = ObtenerPaciente(pacienteId);
                //if(p.Sesiones == null)
                //{
                //   p.Sesiones = Repositorio.BuscarSesionesPaciente()
                //}
                return p.Sesiones;
            }
            catch
            {
                return new List<Sesion>();
            }
        }
        public void EliminarSesion(int sesionId, int pacienteId)
        {
            if (sesionId == 0)
                throw new Exception("No se especifico sesion");
            Paciente p = ObtenerPaciente(pacienteId);
            if (p.Sesiones == null)
                throw new Exception("El paciente no tiene ninguna sesion");
            Sesion s = p.Sesiones.FirstOrDefault(x => x.Id == sesionId);
            if (s == null)
                throw new Exception("No se encontro la sesion");
            p.Sesiones.Remove(s);
            Repositorio.Guardar(p);
        }

        public void EliminarPaciente(Paciente paciente,bool eliminaSesiones)
        {
            var tran = Repositorio.Context.Database.BeginTransaction();
            try
            {
                var p = Repositorio.ObtenerPacienteId(paciente.Id);

                if (eliminaSesiones)
                    Repositorio.EliminarSesiones(p.Sesiones);
                else if (p.Sesiones != null && p.Sesiones.Count > 1)
                    throw new Exception("El paciente contiene sesiones, Borrelas antes de eliminar el paciente");

                Repositorio.EliminarPaciente(p);
                tran.Commit();
            }
            catch (Exception ex)
            {
                if (tran != null)
                    tran.Rollback();
                throw ex;
            }
        }

        public void EliminarSesion(Sesion s)
        {
            Repositorio.EliminarSesiones(s);
        }

        public Paciente GuardarPaciente(Paciente p)
        {
            if (p == null)
                throw new Exception("Error al reconocer un paciente");
            if (string.IsNullOrEmpty(p.NombreApellido))
                throw new Exception("Indique nombre y apellido");

            Paciente pacienteDB = Repositorio.ObtenerUno(p.Id);
            if (pacienteDB == null)
            {
                pacienteDB = Repositorio.ObtenerPorNombreyDni(p.NombreApellido, p.DNI);
                if (pacienteDB != null)
                    throw new Exception("Ya existe un paciente con ese nombre y Dni");
                pacienteDB = new Paciente();
            }
            pacienteDB.NombreApellido = p.NombreApellido;
            pacienteDB.NroTelefono = p.NroTelefono;
            pacienteDB.Sesiones = p.Sesiones ?? new List<Sesion>();
            pacienteDB.DiaNacimiento = p.DiaNacimiento;
            pacienteDB.Direccion = p.Direccion;
            pacienteDB.DNI = p.DNI;
            pacienteDB.Genero = p.Genero;
            pacienteDB.Localidad = p.Localidad;
            pacienteDB.Observaciones = p.Observaciones;
            pacienteDB.Localidad = p.Localidad;

            Repositorio.Guardar(pacienteDB);
            return pacienteDB;
        }

        public Paciente ObtenerPaciente(int pacienteId)
        {
            if (pacienteId == 0)
                throw new Exception("No se especifico paciente");

            Paciente p = Repositorio.ObtenerUno(pacienteId);
            if (p == null)
                throw new Exception("Paciente no encontrado");
            return p;
        }

        public Paciente ObtenerPacienteDni(string dni)
        {
            if (string.IsNullOrEmpty(dni))
                throw new Exception("No se especifico dni");

            Paciente p = Repositorio.ObtenerPacienteDni(dni);

            return p;
        }

        public List<Paciente> ObtenerPacienteNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
                throw new Exception("No se especifico nombre");

            //List<string> claves = nombre.ToLower().Trim().Split(' ').ToList();

            List<Paciente> p = Repositorio.ObtenerPacienteNombre(nombre);

            return p;
        }

        public void EditarObservacionPaciente(int idPacienteSeleccionado, string text)
        {
            Paciente p = Repositorio.ObtenerPacienteId(idPacienteSeleccionado);
            p.Observaciones = text;
            Repositorio.Guardar(p);
        }

    }
}
