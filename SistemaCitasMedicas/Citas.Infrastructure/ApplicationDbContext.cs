using Microsoft.EntityFrameworkCore;
using Citas.Domain;

namespace Citas.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Definimos las tablas de la base de datos
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuramos la relación 1 a 1 opcional entre Cita y Diagnostico
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Diagnostico)
                .WithOne()
                .HasForeignKey<Diagnostico>(d => d.CitaId);

            // 🌱 SEMBRADO DE DATOS AUTOMÁTICO (Para pruebas en Docker)
            modelBuilder.Entity<Medico>().HasData(
                new Medico { Id = 1, Nombre = "Carlos", Apellido = "Mendoza", Especialidad = "Cardiología" },
                new Medico { Id = 2, Nombre = "Hazael", Apellido = "Ramos", Especialidad = "Pediatría" }
            );

            modelBuilder.Entity<Paciente>().HasData(
                new Paciente { Id = 1, Nombre = "Juan", Apellido = "Pérez", DocumentoIdentidad = "12345678" },
                new Paciente { Id = 2, Nombre = "María", Apellido = "López", DocumentoIdentidad = "87654321" }
            );
        }
    }
}