namespace Citas.Front.Models
{
    public class MedicoDto
    {
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string especialidad { get; set; } = string.Empty;
    }

    public class PacienteDto
    {
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string documentoIdentidad { get; set; } = string.Empty;
    }

    public class CitaDto
    {
        public int id { get; set; }
        public DateTime fechaHora { get; set; } = DateTime.Now;
        public int pacienteId { get; set; }
        public PacienteDto? paciente { get; set; }
        public int medicoId { get; set; }
        public MedicoDto? medico { get; set; }
        public DiagnosticoDto? diagnostico { get; set; }
    }

    public class DiagnosticoDto
    {
        public int id { get; set; } = 0;
        public string descripcion { get; set; } = string.Empty;
        public string tratamiento { get; set; } = string.Empty;
        public int citaId { get; set; }
    }

    public class ProgramarCitaComando
    {
        public int id { get; set; } = 0;
        public DateTime fechaHora { get; set; } = DateTime.Now;
        public int pacienteId { get; set; }
        public int medicoId { get; set; }
    }
}