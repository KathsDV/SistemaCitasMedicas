using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citas.Domain
{
    public class Diagnostico
    {
        public int Id { get; set; }
        public int CitaId { get; set; }
        public string Descripcion { get; set; } = string.Empty; // el diagnóstico médico
        public string Tratamiento { get; set; } = string.Empty;  // el tratamiento asignado
    }
}
