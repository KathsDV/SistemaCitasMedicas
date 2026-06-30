using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Citas.Application;
using Citas.Domain;
using Microsoft.Win32;


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

        //api/Citas/medicos -> Requerimiento Frontend: Listar médicos
        [HttpGet("medicos")]
        public async Task<IActionResult> GetMedicos()
        {
            var medicos = await _repository.ObtenerMedicosAsync();
            return Ok(medicos);
        }

        //api/Citas/pacientes -> Requerimiento Frontend: Listar pacientes
        [HttpGet("pacientes")]
        public async Task<IActionResult> GetPacientes()
        {
            var pacientes = await _repository.ObtenerPacientesAsync();
            return Ok(pacientes);
        }

        //api/Citas -> Requerimiento Frontend: Mostrar citas programadas
        [HttpGet]
        public async Task<IActionResult> GetCitas()
        {
            var citas = await _repository.ObtenerCitasProgramadasAsync();
            return Ok(citas);
        }

        //api/Citas/programar -> Requerimiento Frontend: Registrar y programar cita
        [HttpPost("programar")]
        public async Task<IActionResult> ProgramarCita([FromBody] ProgramarCitaComando comando)
        {
            try
            {
                var cita = new Cita
                {
                    FechaHora = DateTime.SpecifyKind(comando.fechaHora, DateTimeKind.Utc),
                    PacienteId = comando.pacienteId,
                    MedicoId = comando.medicoId
                };

                await _programarCitaUseCase.EjecutarAsync(cita);
                return Ok(new { mensaje = "Cita programada con éxito" });
            }
            catch (System.Exception ex)
            {
                // Extraemos el error interno real que manda la Base de Datos
                var errorReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { error = $"Error BD: {errorReal}" });
            }
        }

        //api/Citas/diagnostico -> Requerimiento Frontend: Registrar diagnóstico del médico
        [HttpPost("diagnostico")]
        public async Task<IActionResult> RegistrarDiagnostico([FromBody] Diagnostico diagnostico)
        {
            await _registrarDiagnosticoUseCase.EjecutarAsync(diagnostico);
            return Ok(new { mensaje = "Diagnóstico registrado con éxito" });
        }
    }
}