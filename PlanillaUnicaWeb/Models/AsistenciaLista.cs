using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class AsistenciaLista
	{
		public decimal Rhcol_Id { get; set; }
		public decimal Rhcol_Rut { get; set; }
		public string Rhcol_Dv { get; set; } = string.Empty;
		public string Rhcol_Nombre { get; set; } = string.Empty;
		public string Rhcol_Apel_Pater { get; set; } = string.Empty;
		public string Rhcol_Apel_Mater { get; set; } = string.Empty;
		public decimal Rhcarliq_Id { get; set; }
		public string Rhcarliq_Descripcion { get; set; } = string.Empty;
		public decimal Rhcargen_Id { get; set; }
		public string Rhcargen_Descripcion { get; set; } = string.Empty;
		public decimal Rhcla_Id { get; set; }
		public string Rhcla_Codigo { get; set; } = string.Empty;
		public decimal Rhref1_Id { get; set; }
		public string Rhref1_Descripcion { get; set; } = string.Empty;
		public decimal Rhref2_Id { get; set; }
		public string Rhref2_Descripcion { get; set; } = string.Empty;

		public decimal Rhcargen_Id_Terreno { get; set; }
		public string Rhcargen_Descripcion_Terreno { get; set; } = string.Empty;


		public string Rhcon_Fecha_Inicio { get; set; } = string.Empty;
		public string Rhcon_Fecha_Termino { get; set; } = string.Empty;
		public decimal Rhcon_Contrato_Numero { get; set; }

		public decimal Lun_Id { get; set; }
		public decimal Mar_Id { get; set; }
		public decimal Mie_Id { get; set; }
		public decimal Jue_Id { get; set; }
		public decimal Vie_Id { get; set; }
		public decimal Sab_Id { get; set; }
		public decimal Dom_Id { get; set; }

		public decimal H50 { get; set; }
		public decimal H100 { get; set; }
		public decimal Atraso { get; set; }
		public string Observacion { get; set; }
	}
}