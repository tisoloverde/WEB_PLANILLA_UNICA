using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Gen_Banco
	{
		public decimal Genban_Id { get; set; }
		public string Genban_Descripcion { get; set; } = string.Empty;
		public string Genban_Vigencia { get; set; } = string.Empty;
	}
}