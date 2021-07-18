using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace Server
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = "dbPacientes.db", ForeignKeys = true }.ConnectionString
            }, true)
        {

        }
        public DatabaseContext(string connectionString) :
            base(new SQLiteConnection(connectionString)
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = connectionString, ForeignKeys = false }.ConnectionString
            }, true)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Sesion> Sesiones{ get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Paciente>().ToTable("pacientes");
        //    modelBuilder.Entity<Paciente>(e => { e.HasKey(k => k.Id); });
        //    base.OnModelCreating(modelBuilder);
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite(connectionString: @"FileName="+ ConnectionString, 
        //        sqliteOptionsAction: op =>
        //        {
        //            op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        //        });
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
