using System.Threading.Tasks;
using Citas.Domain;

namespace Citas.Application
{
    public class ProgramarCitaUseCase
    {
        private readonly ICitaRepository _repository;

        //se inyecta el puerto (la interfaz)
        public ProgramarCitaUseCase(ICitaRepository repository)
        {
            _repository = repository;
        }

        public async Task EjecutarAsync(Cita cita)
        {
            //aquí irían reglas de negocio si hicieran falta
            await _repository.ProgramarCitaAsync(cita);
        }
    }
}