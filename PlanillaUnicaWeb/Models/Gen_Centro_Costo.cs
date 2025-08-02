using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Gen_Centro_Costo
	{
		public decimal Gencencos_Id { get; set; }
		public string Gencencos_Codigo { get; set; }
		public string Gencencos_Descripcion { get; set; }
		public decimal Genemp_Id { get; set; }
		public decimal Gencli_Id { get; set; }
		public decimal Gencencos_Dotacion { get; set; }
	}
}