using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Gen_Provincia
	{
		public decimal Genpro_Id { get; set; }
		public string Genpro_Codigo { get; set; } = string.Empty;
		public string Genpro_Descripcion { get; set; } = string.Empty;
		public decimal Genreg_Id { get; set; }
	}
}