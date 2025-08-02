using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Models
{
	public class Account_Menu_Opcion
	{
        public decimal Accmenopc_Id { get; set; }
        public string Accmenopc_Descripcion { get; set; }
        public decimal Accmenopc_Menu_Padre { get; set; }
        public decimal Accmenopc_Orden { get; set; }
        public string Accmenopc_Accion { get; set; }
        public string Accmenopc_Controller { get; set; }
        public string Accmenopc_Titulo { get; set; }
    }
}