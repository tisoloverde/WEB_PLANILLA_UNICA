using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Resultado
	{
		public decimal Id { get; set; }
		public int Cantidad { get; set; }
		public string Estado { get; set; }
		public string Mensaje { get; set; }
	}
}