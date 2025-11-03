using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
    public class FiltroIndicadorGestion
    {
        public decimal EmpresaId { get; set; }
        public decimal CentroCostoId { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int TipoInforme { get; set; }
    }
}