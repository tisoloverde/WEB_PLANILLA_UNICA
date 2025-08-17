using Newtonsoft.Json;
using PlanillaUnicaWeb.Models;
using PlanillaUnicaWeb.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlanillaUnicaWeb.Controllers.Personal
{
    public class PlanillaAsistenciaController : BaseController
    {
        List<Object> myModel = new List<object>();

        // GET: PlanillaAsistencia
        public async Task<ActionResult> Index()
        {
            FiltroPlanilla filtroPlanilla = new FiltroPlanilla();

            decimal usuarioId = Convert.ToDecimal(Session["user_Id"]);

            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Empresa);
            List<Gen_Empresa> empresasList = JsonConvert.DeserializeObject<List<Gen_Empresa>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_CentrosCosto + "/GetAllCentroCostoUsuario?usuarioId=" + usuarioId);
            List<Gen_Centro_Costo> centrosCostoList = JsonConvert.DeserializeObject<List<Gen_Centro_Costo>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Ano + "/GetAllAnos?agregaMas=0&agregaMenos=5");
            List<Gen_Ano> anosList = JsonConvert.DeserializeObject<List<Gen_Ano>>(json);

            //Account_Sistema sistemaEdit = new Account_Sistema();
            //Account_Sistema sistemaNew = new Account_Sistema();

            myModel.Add(empresasList);
            myModel.Add(centrosCostoList);
            myModel.Add(anosList);
            myModel.Add(filtroPlanilla);

            ViewBag.Message2 = "PLANILLAASISTENCIA";
            return View(myModel);
        }

        [HttpGet]
        public async Task<ActionResult> BuscaSemanasPeriodo(int periodo)
        {

            var httpClient = new HttpClient();
            //Account_Menu_Perfil_Opcion menuPerfilOpcion = new Account_Menu_Perfil_Opcion();
            //Account_Perfil perfilOpcion = new Account_Perfil();
            List<Rh_Calendario_Semanas> semanas = new List<Rh_Calendario_Semanas>();
            HttpResponseMessage response;
            string contents;
            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Asistencia + "/GetSemanasAnoLista?ano=" + periodo),
                };

                response = await client.SendAsync(request);
                contents = await response.Content.ReadAsStringAsync();
                semanas = JsonConvert.DeserializeObject<List<Rh_Calendario_Semanas>>(contents);
            }

            var ResultJson = Json(new { status = "succes", data = semanas }, JsonRequestBehavior.AllowGet);
            return ResultJson;

        }

        public async Task<ActionResult> Cargar(FiltroPlanilla filtroPlanilla)
        {
            decimal usuarioId = Convert.ToDecimal(Session["user_Id"]);

            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Empresa);
            List<Gen_Empresa> empresasList = JsonConvert.DeserializeObject<List<Gen_Empresa>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_CentrosCosto + "/GetAllCentroCostoUsuario?usuarioId=" + usuarioId);
            List<Gen_Centro_Costo> centrosCostoList = JsonConvert.DeserializeObject<List<Gen_Centro_Costo>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Ano + "/GetAllAnos?agregaMas=0&agregaMenos=5");
            List<Gen_Ano> anosList = JsonConvert.DeserializeObject<List<Gen_Ano>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Asistencia + "/GetAsistenciaConceptos");
            List<Rh_Asistencia_Concepto> asistenciaConceptos = JsonConvert.DeserializeObject<List<Rh_Asistencia_Concepto>>(json);

            //asistenciaConceptos = asistenciaConceptos.Where(x => x.Rhasicon_Visible.Equals("S")).ToList(); //Filtra solo los conceptos visibles


            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Asistencia + "/GetAsistenciaLista?centroCosto=" + filtroPlanilla.Gencencos_Id + "&periodo=" + filtroPlanilla.Periodo + "&fechaInicio=" + filtroPlanilla.FechaInicio + "&fechaTermino=" + filtroPlanilla.FechaTermino);
            List<AsistenciaLista> asistenciaList = JsonConvert.DeserializeObject<List<AsistenciaLista>>(json);

            myModel.Add(empresasList);
            myModel.Add(centrosCostoList);
            myModel.Add(anosList);
            myModel.Add(asistenciaConceptos);
            myModel.Add(filtroPlanilla);
            myModel.Add(asistenciaList);

            ViewBag.Message2 = "PLANILLAASISTENCIA";
            return View(myModel);
        }
    }
}