using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Rh_Asistencia_Concepto
	{
		public decimal Rhasicon_Id { get; set; }
		public string Rhasicon_Sigla { get; set; } = string.Empty;
		public string Rhasicon_Descripcion { get; set; } = string.Empty;
		public decimal Rhasicon_Descuento { get; set; }
		public string Rhasicon_Visible { get; set; } = string.Empty;
		public string Rhasicon_Pagado_Nopagado { get; set; } = string.Empty;
		public decimal Rhasimar_Id { get; set; }
		public decimal Rhasitipmar_Id { get; set; }
		public decimal Rhasicla_Id { get; set; }
	}
}