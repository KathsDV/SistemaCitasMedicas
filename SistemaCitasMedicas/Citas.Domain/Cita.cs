using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citas.Domain
{
    public class Cita
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }

        public DateTime FechaHora { get; set; }

        // relación opcional: Una cita puede o no tener un diagnóstico aún
        public Diagnostico? Diagnostico { get; set; }
    }
}
