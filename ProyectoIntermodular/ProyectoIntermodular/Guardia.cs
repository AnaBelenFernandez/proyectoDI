using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntermodular
{
    public class Guardia
    {
        public int Id { get ; set; }
        public int Profesor1 { get; set; }
        public int Profesor2 { get; set; }
        public EstadoEnum Estado { get; set; }
        public DateTime Fecha { get; set; }
        public int HorarioBean { get; set; }
        public int DiaSemana { get; set; }
        public int Hora { get; set; }
        public int AvisosGuardias { get; set; }
        public string Grupo { get; set; }
        public string Aula { get; set; }
        public string Observaciones { get; set; }
    }
}
