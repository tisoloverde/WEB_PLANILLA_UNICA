using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Rh_Calendario_Semanas
	{
		public decimal NumeroSemana { get; set; }
		public int Ano { get; set; }
		public string FechaInicioSemana { get; set; } = string.Empty;
		public string FechaFinSemana { get; set; } = string.Empty;
		public string DiaInicioSemana { get; set; } = string.Empty;
		public string DiaFinSemana { get; set; } = string.Empty;
		public Boolean Activa { get; set; } = false;
	}
}