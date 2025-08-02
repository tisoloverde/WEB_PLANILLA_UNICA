using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanillaUnicaWeb.Utility
{
	public class Resource
	{
        //SERVICIOS DE API DE ACCOUNT
        public const string Account_LoginUsuario = "api/LoginUsuario";
        public const string Account_UsuarioMail = "api/Usuario/GetUsuarioEmail";
        public const string Account_Acceso = "api/Acceso";
        public const string Account_Usuario = "api/Usuario";
        public const string Account_Perfil = "api/PerfilUsuario";
        public const string Gen_Area = "api/Area";
        public const string Account_Sistema = "api/Sistema";

        //SERVICIOS DE API PLANILLA UNICA
        public const string PlanillaUnica_Colaborador = "api/Colaborador";
        public const string PlanillaUnica_LicenciaConducir = "api/LicenciaConducir";
        public const string PlanillaUnica_NivelEstudio = "api/NivelEstudio";
        public const string PlanillaUnica_EstadoCivil = "api/EstadoCivil";
        public const string PlanillaUnica_VinculoContacto = "api/VinculoContacto";
        public const string PlanillaUnica_Dotacion = "api/Dotacion";
        public const string PlanillaUnica_TipoPersonalOfertado = "api/TipoPersonalOfertado";
        public const string PlanillaUnica_Familia = "api/Familia";
        public const string PlanillaUnica_CargoGenerico = "api/CargoGenerico";
        public const string PlanillaUnica_Clasificacion = "api/Clasificacion";
        public const string PlanillaUnica_Referencia1 = "api/Referencia1";
        public const string PlanillaUnica_Referencia2 = "api/Referencia2";

        //SERVICIOS DE API GENERICA
        public const string Generica_CentrosCosto = "api/CentroCosto";
        public const string Generica_Empresa = "api/Empresa";
        public const string Generica_Mes = "api/Mes";
        public const string Generica_Ano = "api/Ano";
        public const string Generica_Banco = "api/Banco";
        public const string Generica_Pais = "api/Pais";
        public const string Generica_Region = "api/Region";
        public const string Generica_Provincia = "api/Provincia";
        public const string Generica_Comuna = "api/Comuna";
    }
}