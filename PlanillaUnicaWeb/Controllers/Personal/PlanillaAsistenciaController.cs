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
            FiltroDotacion filtroDotacion = new FiltroDotacion();

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
            myModel.Add(filtroDotacion);

            ViewBag.Message2 = "PLANILLAASISTENCIA";
            return View(myModel);
        }
    }
}