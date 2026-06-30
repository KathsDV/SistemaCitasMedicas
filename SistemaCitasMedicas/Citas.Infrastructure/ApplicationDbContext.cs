using Microsoft.EntityFrameworkCore;
using Citas.Domain;

namespace Citas.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //tablas de la base de datos
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //configuramos la relación 1 a 1 opcional entre la cita y el diagnostico
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Diagnostico)
                .WithOne()
                .HasForeignKey<Diagnostico>(d => d.CitaId);

            modelBuilder.Entity<Medico>().HasData(
                new Medico { Id = 1, Nombre = "Carlos", Apellido = "Mendoza", Especialidad = "Cardiología" },
                new Medico { Id = 2, Nombre = "Hazael", Apellido = "Ramos", Especialidad = "Pediatría" },
                new Medico { Id = 3, Nombre = "Laura", Apellido = "Guzmán", Especialidad = "Ginecología" },
                new Medico { Id = 4, Nombre = "Andrés", Apellido = "Villatoro", Especialidad = "Medicina General" },
                new Medico { Id = 5, Nombre = "Sofía", Apellido = "Castellanos", Especialidad = "Dermatología" }
            );

            modelBuilder.Entity<Paciente>().HasData(
                new Paciente { Id = 1, Nombre = "Juan", Apellido = "Pérez", DocumentoIdentidad = "12345678" },
                new Paciente { Id = 2, Nombre = "María", Apellido = "López", DocumentoIdentidad = "87654321" },
                new Paciente { Id = 3, Nombre = "Diego", Apellido = "Torres", DocumentoIdentidad = "45678912" },
                new Paciente { Id = 4, Nombre = "Elena", Apellido = "Rivas", DocumentoIdentidad = "98765432" },
                new Paciente { Id = 5, Nombre = "Ricardo", Apellido = "Benítez", DocumentoIdentidad = "32165498" }
            );
        }
    }
}