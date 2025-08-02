using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Rh_Tipo_Personal_Ofertado
	{
		public decimal Rhtipperofe_Id { get; set; }
		public string Rhtipperofe_Descripcion { get; set; } = string.Empty;
		public string Rhtipperofe_Vigencia { get; set; } = string.Empty;
	}
}