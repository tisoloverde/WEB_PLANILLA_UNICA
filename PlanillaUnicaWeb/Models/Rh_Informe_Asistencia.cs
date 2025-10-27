using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
    public class Rh_Informe_Asistencia
    {
        public string Email { get; set; } = string.Empty;
        public int CentroCosto { get; set; }
        public string FechaInicio { get; set; } = string.Empty;
        public string FechaTermino { get; set; } = string.Empty;
        public List<Rh_Tipo_Informe> TiposInforme { get; set; }
    }
}