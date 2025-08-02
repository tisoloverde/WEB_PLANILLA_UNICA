using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Gen_Region
	{
		public decimal Genreg_Id { get; set; }
		public string Genreg_Codigo { get; set; } = string.Empty;
		public string Genreg_Descripcion { get; set; } = string.Empty;
	}
}