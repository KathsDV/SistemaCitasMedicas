using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Citas.Application;
using Citas.Domain;

namespace Citas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ProgramarCitaUseCase _programarCitaUseCase;
        private readonly RegistrarDiagnosticoUseCase _registrarDiagnosticoUseCase;
        private readonly ICitaRepository _repository;

        public CitasController(
            ProgramarCitaUseCase programarCitaUseCase,
            RegistrarDiagnosticoUseCase registrarDiagnosticoUseCase,
            ICitaRepository repository)
        {
            _programarCitaUseCase = programarCitaUseCase;
            _registrarDiagnosticoUseCase = registrarDiagnosticoUseCase;
            _repository = repository;
        }

        // 1. GET: api/Citas/medicos -> Requerimiento Frontend: Listar médicos
        [HttpGet("medicos")]
        public async Task<IActionResult> GetMedicos()
        {
            var medicos = await _repository.ObtenerMedicosAsync();
            return Ok(medicos);
        }

        // 2. GET: api/Citas/pacientes -> Requerimiento Frontend: Listar pacientes
        [HttpGet("pacientes")]
        public async Task<IActionResult> GetPacientes()
        {
            var pacientes = await _repository.ObtenerPacientesAsync();
            return Ok(pacientes);
        }

        // 3. GET: api/Citas -> Requerimiento Frontend: Mostrar citas programadas
        [HttpGet]
        public async Task<IActionResult> GetCitas()
        {
            var citas = await _repository.ObtenerCitasProgramadasAsync();
            return Ok(citas);
        }

        // 4. POST: api/Citas/programar -> Requerimiento Frontend: Registrar y programar cita
        [HttpPost("programar")]
        public async Task<IActionResult> ProgramarCita([FromBody] Cita cita)
        {
            await _programarCitaUseCase.EjecutarAsync(cita);
            return Ok(new { mensaje = "Cita programada con éxito" });
        }

        // 5. POST: api/Citas/diagnostico -> Requerimiento Frontend: Registrar diagnóstico del médico
        [HttpPost("diagnostico")]
        public async Task<IActionResult> RegistrarDiagnostico([FromBody] Diagnostico diagnostico)
        {
            await _registrarDiagnosticoUseCase.EjecutarAsync(diagnostico);
            return Ok(new { mensaje = "Diagnóstico registrado con éxito" });
        }
    }
}