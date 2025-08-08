using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class DotacionLista
	{
		public decimal Rhdot_Id { get; set; }
		public decimal Gencencos_Id { get; set; }
		public decimal Rhdot_Periodo { get; set; }
		public decimal Rhtipperofe_Id { get; set; }
		public string Rhtipperofe_Descripcion { get; set; } = string.Empty;
		public decimal Rhfam_Id { get; set; }
		public string Rhfam_Descripcion { get; set; } = string.Empty;
		public decimal Id_Cargo_Mandante { get; set; }
		public string Cargo_Mandante { get; set; } = string.Empty;
		public decimal Id_Cargo_Generico { get; set; }
		public string Cargo_Generico { get; set; } = string.Empty;
		public decimal Id_Clasificacion { get; set; }
		public string Codigo_Clasificacion { get; set; } = string.Empty;
		public string Clasificacion { get; set; } = string.Empty;
		public decimal Id_Ref1 { get; set; }
		public string Descripcion_Ref1 { get; set; } = string.Empty;
		public decimal Id_Ref2 { get; set; }
		public string Descripcion_Ref2 { get; set; } = string.Empty;
		public decimal Rhdot_Ene { get; set; }
		public decimal Rhdot_Feb { get; set; }
		public decimal Rhdot_Mar { get; set; }
		public decimal Rhdot_Abr { get; set; }
		public decimal Rhdot_May { get; set; }
		public decimal Rhdot_Jun { get; set; }
		public decimal Rhdot_Jul { get; set; }
		public decimal Rhdot_Ago { get; set; }
		public decimal Rhdot_Sep { get; set; }
		public decimal Rhdot_Oct { get; set; }
		public decimal Rhdot_Nov { get; set; }
		public decimal Rhdot_Dic { get; set; }
	}
}