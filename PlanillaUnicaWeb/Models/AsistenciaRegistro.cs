using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class AsistenciaRegistro
	{
		public decimal Rhcol_Id { get; set; }
		public decimal Rhasicon_Id { get; set; }
		public string Rhsis_Fecha { get; set; }
		public decimal Rhsis_H50 { get; set; }
		public decimal Rhsis_H100 { get; set; }
		public decimal Rhsis_Atraso { get; set; }
		public string Rhsis_Observacion { get; set; }
		public decimal Accusu_Id { get; set; }
		public decimal Rhcargen_Id { get; set; }
		public decimal Rhref1_Id { get; set; }
		public decimal Rhref2_Id { get; set; }
		public decimal Rhcargen_Id_B { get; set; }
		public decimal Rhref1_Id_B { get; set; }
		public decimal Rhref2_Id_B { get; set; }
	}
}