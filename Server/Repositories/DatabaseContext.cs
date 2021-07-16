using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class DatabaseContext : DbContext
    {
        string ConnectionString = "dbPacientes.db";
        public DatabaseContext()
        {

        }
        public DatabaseContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Sesion> Sesiones{ get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Paciente>().ToTable("pacientes");
        //    modelBuilder.Entity<Paciente>(e => { e.HasKey(k => k.Id); });
        //    base.OnModelCreating(modelBuilder);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: @"FileName="+ ConnectionString, 
                sqliteOptionsAction: op =>
                {
                    op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
