using Newtonsoft.Json;
using PlanillaUnicaWeb.Models;
using PlanillaUnicaWeb.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlanillaUnicaWeb.Controllers.Personal
{
    public class ListaPersonalController : BaseController
    {
        List<Object> myModel = new List<object>();

        // GET: ListaPersonal
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //Edit colaborador
            Rh_Colaborador editColaborador = new Rh_Colaborador();
            Rh_Colaborador newColaborador = new Rh_Colaborador();
            Rh_Colaborador fichaColaborador = new Rh_Colaborador();

            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Colaborador + "/GetAllColaboradores");
            List<ColaboradorLista> colaboradoresList = JsonConvert.DeserializeObject<List<ColaboradorLista>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Banco );
            List<Gen_Banco> bancoList = JsonConvert.DeserializeObject<List<Gen_Banco>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Pais);
            List<Gen_Pais> paisList = JsonConvert.DeserializeObject<List<Gen_Pais>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_LicenciaConducir + "/GetAllLicenciaConducir");
            List<Rh_Licencia_Conducir> licenciasConducirList = JsonConvert.DeserializeObject<List<Rh_Licencia_Conducir>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_NivelEstudio + "/GetAllNivelEstudio");
            List<Rh_Nivel_Estudio> nivelesEstudioList = JsonConvert.DeserializeObject<List<Rh_Nivel_Estudio>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_EstadoCivil + "/GetAllEstadoCivil");
            List<Rh_Estado_Civil> estadosCivilList = JsonConvert.DeserializeObject<List<Rh_Estado_Civil>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_VinculoContacto + "/GetAllVinculoContacto");
            List<Rh_Vinculo_Contacto> vinculosContactoList = JsonConvert.DeserializeObject<List<Rh_Vinculo_Contacto>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Region);
            List<Gen_Region> regionList = JsonConvert.DeserializeObject<List<Gen_Region>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Provincia);
            List<Gen_Provincia> provinciaList = JsonConvert.DeserializeObject<List<Gen_Provincia>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Comuna);
            List<Gen_Comuna> comunaList = JsonConvert.DeserializeObject<List<Gen_Comuna>>(json);

            Gen_Genero genero = new Gen_Genero();
            List<Gen_Genero> generosList = new List<Gen_Genero>();
            genero.Gensex_Id = "F";
            genero.Gensex_Descripcion = "Femenino";
            generosList.Add(genero);
            genero = new Gen_Genero();
            genero.Gensex_Id = "M";
            genero.Gensex_Descripcion = "Masculino";
            generosList.Add(genero);


            //Account_Sistema sistemaEdit = new Account_Sistema();
            //Account_Sistema sistemaNew = new Account_Sistema();

            myModel.Add(colaboradoresList);
            myModel.Add(editColaborador);
            myModel.Add(fichaColaborador);
            myModel.Add(newColaborador);
            myModel.Add(bancoList);
            myModel.Add(paisList);
            myModel.Add(licenciasConducirList);
            myModel.Add(nivelesEstudioList);
            myModel.Add(estadosCivilList);
            myModel.Add(vinculosContactoList);
            myModel.Add(regionList);
            myModel.Add(provinciaList);
            myModel.Add(comunaList);
            myModel.Add(generosList);


            ViewBag.Message2 = "COLABORADORES";
            return View(myModel);

        }


        [HttpGet]
        public async Task<ActionResult> BuscaColaboradorPorId(decimal colaboradorId)
        {

            var httpClient = new HttpClient();
            //Account_Menu_Perfil_Opcion menuPerfilOpcion = new Account_Menu_Perfil_Opcion();
            //Account_Perfil perfilOpcion = new Account_Perfil();
            ColaboradorLista colaborador = new ColaboradorLista();
            HttpResponseMessage response;
            string contents;
            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ConfigurationManager.AppSettings["ApiPlanillaUnica"] +  Resource.PlanillaUnica_Colaborador + "/GetColaboradorId?colaboradorId=" + colaboradorId),
                };

                response = await client.SendAsync(request);
                contents = await response.Content.ReadAsStringAsync();
                colaborador = JsonConvert.DeserializeObject<ColaboradorLista>(contents);
            }

            var ResultJson = Json(new { status = "succes", data = colaborador }, JsonRequestBehavior.AllowGet);
            return ResultJson;

        }
    }
}