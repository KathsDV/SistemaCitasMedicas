using System;
using System.Threading.Tasks;
using Citas.Domain;

namespace Citas.Application
{
    public class RegistrarDiagnosticoUseCase
    {
        private readonly ICitaRepository _repository;

        public RegistrarDiagnosticoUseCase(ICitaRepository repository)
        {
            _repository = repository;
        }

        public async Task EjecutarAsync(Diagnostico diagnostico)
        {
            //validamos que la cita realmente exista antes de diagnosticar
            var cita = await _repository.ObtenerCitaPorIdAsync(diagnostico.CitaId);
            if (cita == null)
            {
                throw new Exception("La cita especificada no existe.");
            }

            await _repository.RegistrarDiagnosticoAsync(diagnostico);
        }
    }
}