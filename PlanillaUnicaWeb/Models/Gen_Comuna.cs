using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Gen_Comuna
	{
		public decimal Gencom_Id { get; set; }
		public string Gencom_Codigo { get; set; } = string.Empty;
		public string Gencom_Descripcion { get; set; } = string.Empty;
		public decimal Genpro_Id { get; set; }
	}
}