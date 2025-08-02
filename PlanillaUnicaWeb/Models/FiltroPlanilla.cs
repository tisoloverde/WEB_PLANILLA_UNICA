using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class FiltroPlanilla
	{
		public decimal Genemp_Id { get; set; }
		public decimal Gencencos_Id { get; set; }
		public decimal Periodo { get; set; }
		public decimal NumeroSemana { get; set; }
	}
}