using System.Collections.Generic;
using System.Threading.Tasks;
using Citas.Domain;

namespace Citas.Application
{
    public interface ICitaRepository
    {
        Task<IEnumerable<Medico>> ObtenerMedicosAsync();
        Task<IEnumerable<Paciente>> ObtenerPacientesAsync();
        Task<IEnumerable<Cita>> ObtenerCitasProgramadasAsync();
        Task ProgramarCitaAsync(Cita cita);
        Task RegistrarDiagnosticoAsync(Diagnostico diagnostico);
        Task<Cita?> ObtenerCitaPorIdAsync(int citaId);
    }
}
