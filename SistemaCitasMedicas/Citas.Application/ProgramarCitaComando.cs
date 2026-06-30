using System;

namespace Citas.Application
{
    public class ProgramarCitaComando
    {
        public int id { get; set; } = 0;
        public DateTime fechaHora { get; set; } = DateTime.Now;
        public int pacienteId { get; set; }
        public int medicoId { get; set; }
    }
}