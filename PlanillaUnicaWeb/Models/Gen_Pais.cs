using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Gen_Pais
	{
		public decimal Genpai_Id { get; set; }
		public string Genpai_Descripcion { get; set; } = string.Empty;
		public string Genpai_Nacionalidad { get; set; } = string.Empty;
		public string Genpai_Vigencia { get; set; } = string.Empty;
	}
}