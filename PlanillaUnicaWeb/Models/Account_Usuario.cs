using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Account_Usuario
	{
        public decimal Accusu_Id { get; set; }
        public decimal Accusu_Rut { get; set; }
        public string Accusu_Dv { get; set; }
        [Display(Name = "Nombre Usuario")]
        [Required(ErrorMessage = "*")]
        [StringLength(80, ErrorMessage = "max 80")]
        public string Accusu_Nombre { get; set; }
        [Display(Name = "Apellido Paterno Usuario")]
        [Required(ErrorMessage = "*")]
        [StringLength(40, ErrorMessage = "max 40")]
        public string Accusu_Apel_Pater { get; set; }
        [Display(Name = "Apellido Materno Usuario")]
        [Required(ErrorMessage = "*")]
        [StringLength(40, ErrorMessage = "max 40")]
        public string Accusu_Apel_Mater { get; set; }
        [Display(Name = "Correo Electrónico Usuario")]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        public string Accusu_Email { get; set; } 
        public string Accusu_Fono { get; set; } = "";
        public string Accusu_Fono_Movil { get; set; } = "";
        public DateTime Accusu_Fecha_Creacion { get; set; }
        public DateTime Accusu_Fecha_Modificacion { get; set; }
        public string Accusu_Vigencia { get; set; }
        public Int32 Accestlog_Id { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal Genara_Id { get; set; }
        public string Password { get; set; }
        [Display(Name = "Rut Usuario")]
        [Required(ErrorMessage = "*")]
        [StringLength(12, ErrorMessage = "max 12")]
        public string Rut_Compuesto { get; set; }
    }
}