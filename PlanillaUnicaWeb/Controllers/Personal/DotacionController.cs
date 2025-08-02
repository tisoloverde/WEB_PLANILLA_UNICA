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
    public class DotacionController : BaseController
    {
        List<Object> myModel = new List<object>();

        // GET: Dotacion
        public async Task<ActionResult> Index()
        {
            FiltroDotacion filtroDotacion = new FiltroDotacion();
            
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Empresa);
            List<Gen_Empresa> empresasList = JsonConvert.DeserializeObject<List<Gen_Empresa>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_CentrosCosto + "/GetAllCentroCosto");
            List<Gen_Centro_Costo> centrosCostoList = JsonConvert.DeserializeObject<List<Gen_Centro_Costo>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Ano + "/GetAllAnos?agregaMas=0&agregaMenos=5");
            List<Gen_Ano> anosList = JsonConvert.DeserializeObject<List<Gen_Ano>>(json);

            

            myModel.Add(empresasList);
            myModel.Add(centrosCostoList);
            myModel.Add(anosList);
            myModel.Add(filtroDotacion);

            ViewBag.Message2 = "DOTACION";
            return View(myModel);
        }

        public async Task<ActionResult> Cargar(FiltroDotacion filtroDotacion)
        {

            Rh_Dotacion dotacionEdit = new Rh_Dotacion();
            Rh_Dotacion dotacionNew = new Rh_Dotacion();

            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Empresa);
            List<Gen_Empresa> empresasList = JsonConvert.DeserializeObject<List<Gen_Empresa>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_CentrosCosto + "/GetAllCentroCosto");
            List<Gen_Centro_Costo> centrosCostoList = JsonConvert.DeserializeObject<List<Gen_Centro_Costo>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Ano + "/GetAllAnos?agregaMas=0&agregaMenos=5");
            List<Gen_Ano> anosList = JsonConvert.DeserializeObject<List<Gen_Ano>>(json);

            //Rescatamos valoresd de la dotación para el centro de costo y periodo seleccionado
            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Dotacion + "/GetDotacionLista?centroCosto=" + filtroDotacion.Gencencos_Id + "&periodo=" + filtroDotacion.Periodo);
            List<DotacionLista> dotacionList = JsonConvert.DeserializeObject<List<DotacionLista>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_TipoPersonalOfertado + "/GetAllTipoPersonalOfertado");
            List<Rh_Tipo_Personal_Ofertado> tipoPersonalOfertadoList = JsonConvert.DeserializeObject<List<Rh_Tipo_Personal_Ofertado>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Familia + "/GetAllFamilia");
            List<Rh_Familia> familiaList = JsonConvert.DeserializeObject<List<Rh_Familia>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_CargoGenerico + "/GetAllCargoGenerico");
            List<Rh_Cargo_Generico> cargoGenericoList = JsonConvert.DeserializeObject<List<Rh_Cargo_Generico>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Clasificacion + "/GetAllClasificacion");
            List<Rh_Clasificacion> clasificacionList = JsonConvert.DeserializeObject<List<Rh_Clasificacion>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Referencia1 + "/GetAllReferencia1");
            List<Rh_Referencia_1> referencia1List = JsonConvert.DeserializeObject<List<Rh_Referencia_1>>(json);

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Referencia2 + "/GetAllReferencia2");
            List<Rh_Referencia_2> referencia2List = JsonConvert.DeserializeObject<List<Rh_Referencia_2>>(json);

            myModel.Add(empresasList);
            myModel.Add(centrosCostoList);
            myModel.Add(anosList);
            myModel.Add(filtroDotacion);
            myModel.Add(dotacionList);
            myModel.Add(tipoPersonalOfertadoList);
            myModel.Add(familiaList);
            myModel.Add(cargoGenericoList);
            myModel.Add(referencia1List);
			myModel.Add(referencia2List);
            myModel.Add(clasificacionList);
            myModel.Add(dotacionEdit);
            myModel.Add(dotacionNew);

            ViewBag.Message2 = "DOTACION";
            return View(myModel);
        }

        [HttpPost]
        public async Task<ActionResult> IngresaModifica(Rh_Dotacion dotacionEdit, Rh_Dotacion dotacionNew)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response;
                Rh_Dotacion dotacionEnvia = new Rh_Dotacion();
                string contents;

                if (dotacionEdit.Rhdot_Id > 0)
                {
                    dotacionEnvia.Rhdot_Id = dotacionEdit.Rhdot_Id;
                    dotacionEnvia.Gencencos_Id = dotacionEdit.Gencencos_Id;
                    dotacionEnvia.Rhdot_Periodo = dotacionEdit.Rhdot_Periodo;
                    dotacionEnvia.Rhtipperofe_Id = dotacionEdit.Rhtipperofe_Id;
                    dotacionEnvia.Rhfam_Id = dotacionEdit.Rhfam_Id;
                    dotacionEnvia.Rhcargen_Id_Mandante = dotacionEdit.Rhcargen_Id_Mandante;
                    dotacionEnvia.Rhcargen_Id_Unificado = dotacionEdit.Rhcargen_Id_Unificado;
                    dotacionEnvia.Rhref1_Id = dotacionEdit.Rhref1_Id;
                    dotacionEnvia.Rhref2_Id = dotacionEdit.Rhref2_Id;
                    dotacionEnvia.Rhdot_Ene = dotacionEdit.Rhdot_Ene;
                    dotacionEnvia.Rhdot_Feb = dotacionEdit.Rhdot_Feb;
                    dotacionEnvia.Rhdot_Mar = dotacionEdit.Rhdot_Mar;
                    dotacionEnvia.Rhdot_Abr = dotacionEdit.Rhdot_Abr;
                    dotacionEnvia.Rhdot_May = dotacionEdit.Rhdot_May;
                    dotacionEnvia.Rhdot_Jun = dotacionEdit.Rhdot_Jun;
                    dotacionEnvia.Rhdot_Jul = dotacionEdit.Rhdot_Jul;
                    dotacionEnvia.Rhdot_Ago = dotacionEdit.Rhdot_Ago;
                    dotacionEnvia.Rhdot_Sep = dotacionEdit.Rhdot_Sep;
                    dotacionEnvia.Rhdot_Oct = dotacionEdit.Rhdot_Oct;
                    dotacionEnvia.Rhdot_Nov = dotacionEdit.Rhdot_Nov;
                    dotacionEnvia.Rhdot_Dic = dotacionEdit.Rhdot_Dic;
                    dotacionEnvia.Rhdot_Vigencia = dotacionEdit.Rhdot_Vigencia;

                    response = await client.PostAsJsonAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Dotacion + "/UpdateDotacion", dotacionEnvia);
                    contents = await response.Content.ReadAsStringAsync();
                    Resultado resultadoExe = JsonConvert.DeserializeObject<Resultado>(contents);

                    if (response.StatusCode.ToString().Equals("OK"))
                    {
                        var ResultJson = Json(new { success = true, data = resultadoExe }, JsonRequestBehavior.AllowGet);
                        return ResultJson;
                    }
                    else
                    {
                        var ResultJson = Json(new { status = "error", data = resultadoExe }, JsonRequestBehavior.AllowGet);
                        return ResultJson;
                    }

                }
                else
                {
                    dotacionEnvia.Gencencos_Id = dotacionNew.Gencencos_Id;
                    dotacionEnvia.Rhdot_Periodo = dotacionNew.Rhdot_Periodo;
                    dotacionEnvia.Rhtipperofe_Id = dotacionNew.Rhtipperofe_Id;
                    dotacionEnvia.Rhfam_Id = dotacionNew.Rhfam_Id;
                    dotacionEnvia.Rhcargen_Id_Mandante = dotacionNew.Rhcargen_Id_Mandante;
                    dotacionEnvia.Rhcargen_Id_Unificado = dotacionNew.Rhcargen_Id_Unificado;
                    dotacionEnvia.Rhref1_Id = dotacionNew.Rhref1_Id;
                    dotacionEnvia.Rhref2_Id = dotacionNew.Rhref2_Id;
                    dotacionEnvia.Rhdot_Ene = dotacionNew.Rhdot_Ene;
                    dotacionEnvia.Rhdot_Feb = dotacionNew.Rhdot_Feb;
                    dotacionEnvia.Rhdot_Mar = dotacionNew.Rhdot_Mar;
                    dotacionEnvia.Rhdot_Abr = dotacionNew.Rhdot_Abr;
                    dotacionEnvia.Rhdot_May = dotacionNew.Rhdot_May;
                    dotacionEnvia.Rhdot_Jun = dotacionNew.Rhdot_Jun;
                    dotacionEnvia.Rhdot_Jul = dotacionNew.Rhdot_Jul;
                    dotacionEnvia.Rhdot_Ago = dotacionNew.Rhdot_Ago;
                    dotacionEnvia.Rhdot_Sep = dotacionNew.Rhdot_Sep;
                    dotacionEnvia.Rhdot_Oct = dotacionNew.Rhdot_Oct;
                    dotacionEnvia.Rhdot_Nov = dotacionNew.Rhdot_Nov;
                    dotacionEnvia.Rhdot_Dic = dotacionNew.Rhdot_Dic;
                    dotacionEnvia.Rhdot_Vigencia = "S";

                    response = await client.PutAsJsonAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Dotacion + "/NewDotacion", dotacionEnvia);
                    contents = await response.Content.ReadAsStringAsync();
                    Resultado resultadoExe = JsonConvert.DeserializeObject<Resultado>(contents);

                    if (response.StatusCode.ToString().Equals("OK"))
                    {
                        var ResultJson = Json(new { success = true, data = resultadoExe }, JsonRequestBehavior.AllowGet);
                        return ResultJson;
                    }
                    else
                    {
                        var ResultJson = Json(new { status = "error", data = resultadoExe }, JsonRequestBehavior.AllowGet);
                        return ResultJson;
                    }

                }
            } else
			{
                var ResultJson = Json(new { success = false , data = "Debe completar la información requerida." }, JsonRequestBehavior.AllowGet);
                return ResultJson;
            }
        }

    }
}