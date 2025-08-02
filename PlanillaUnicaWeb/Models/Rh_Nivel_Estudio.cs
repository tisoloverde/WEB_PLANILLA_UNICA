using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Rh_Nivel_Estudio
	{
		public decimal Rhnivest_Id { get; set; }
		public string Rhnivest_Descripcion { get; set; } = string.Empty;
		public string Rhnivest_Vigencia { get; set; } = string.Empty;
	}
}