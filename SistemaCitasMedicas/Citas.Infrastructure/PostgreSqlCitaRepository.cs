using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Citas.Application;
using Citas.Domain;

namespace Citas.Infrastructure
{
    public class PostgreSqlCitaRepository : ICitaRepository
    {
        private readonly ApplicationDbContext _context;

        public PostgreSqlCitaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medico>> ObtenerMedicosAsync()
        {
            return await _context.Medicos.ToListAsync();
        }

        public async Task<IEnumerable<Paciente>> ObtenerPacientesAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<IEnumerable<Cita>> ObtenerCitasProgramadasAsync()
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Diagnostico)
                .ToListAsync();
        }

        public async Task<Cita?> ObtenerCitaPorIdAsync(int citaId)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Diagnostico)
                .FirstOrDefaultAsync(c => c.Id == citaId);
        }

        public async Task ProgramarCitaAsync(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
            await _context.SaveChangesAsync();
        }

        public async Task RegistrarDiagnosticoAsync(Diagnostico diagnostico)
        {
            await _context.Diagnosticos.AddAsync(diagnostico);
            await _context.SaveChangesAsync();
        }
    }
}