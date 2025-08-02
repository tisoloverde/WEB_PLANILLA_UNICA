using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Rh_Dotacion
	{
		public decimal Rhdot_Id { get; set; }
		[Required(ErrorMessage = "*")]
		public decimal Gencencos_Id { get; set; }
		[Required(ErrorMessage = "*")]
		public decimal Rhdot_Periodo { get; set; }
		[Required(ErrorMessage = "*")]
		public decimal Rhtipperofe_Id { get; set; }
		[Required(ErrorMessage = "*")]
		public decimal Rhfam_Id { get; set; }
		[Required(ErrorMessage = "*")]
		public decimal Rhcargen_Id_Mandante { get; set; }
		[Required(ErrorMessage = "*")]
		public decimal Rhcargen_Id_Unificado { get; set; }
		[Required(ErrorMessage = "*")]
		public decimal Rhref1_Id { get; set; }
		[Required(ErrorMessage = "*")]
		public decimal Rhref2_Id { get; set; }
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
		public string Rhdot_Vigencia { get; set; }
	}
}