using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Rh_Cargo_Generico
	{
		public decimal Rhcargen_Id { get; set; }
		public string Rhcargen_Descripcion { get; set; } 
		public decimal Rhfam_Id { get; set; }
		public decimal Rhcla_Id { get; set; }
	}
}