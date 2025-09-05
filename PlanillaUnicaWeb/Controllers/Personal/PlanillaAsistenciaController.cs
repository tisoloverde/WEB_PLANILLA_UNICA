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
        public async Task<JsonResult> BuscaSemanasPeriodo(int periodo)
        {

            var httpClient = new HttpClient();
            List<Rh_Calendario_Semanas> semanas = new List<Rh_Calendario_Semanas>();
            HttpResponseMessage response;
            string contents;

            try
            {
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


                return Json(new
                {
                    success = true,
                    message = "Semanas cargadas correctamente",
                    data = semanas
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno del servidor al cargar semanas",
                    data = (object)null
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public async Task<ActionResult> Cargar(FiltroPlanilla filtroPlanilla, List<AsistenciaLista> lstAsistencia)
        {

            //GRABACION 
            if (lstAsistencia != null)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response;
                AsistenciaRegistro asistenciaEnvia = new AsistenciaRegistro();
                List<AsistenciaRegistro> lstAsistenciaEnvia = new List<AsistenciaRegistro>();
                string contents;


                foreach (var asistencia in lstAsistencia)
                {
                    if (asistencia.Seleccionado)
                    {
                        int diaSemana = 1;
                        Boolean sw_bloqueado = false;
                        for (DateTime fecha = filtroPlanilla.FechaInicio; fecha <= filtroPlanilla.FechaTermino;)
                        {
                            asistenciaEnvia = new AsistenciaRegistro();
                            asistenciaEnvia.Rhcol_Id = asistencia.Rhcol_Id;
                            switch (diaSemana)
                            {
                                case 1:
                                    asistenciaEnvia.Rhasicon_Id = asistencia.Lun_Id;
                                    sw_bloqueado = asistencia.Lun_Status;
                                    break;
                                case 2:
                                    asistenciaEnvia.Rhasicon_Id = asistencia.Mar_Id;
                                    sw_bloqueado = asistencia.Mar_Status;
                                    break;
                                case 3:
                                    asistenciaEnvia.Rhasicon_Id = asistencia.Mie_Id;
                                    sw_bloqueado = asistencia.Mie_Status;
                                    break;
                                case 4:
                                    asistenciaEnvia.Rhasicon_Id = asistencia.Jue_Id;
                                    sw_bloqueado = asistencia.Jue_Status;
                                    break;
                                case 5:
                                    asistenciaEnvia.Rhasicon_Id = asistencia.Vie_Id;
                                    sw_bloqueado = asistencia.Vie_Status;
                                    break;
                                case 6:
                                    asistenciaEnvia.Rhasicon_Id = asistencia.Sab_Id;
                                    sw_bloqueado = asistencia.Sab_Status;
                                    break;
                                case 7:
                                    asistenciaEnvia.Rhasicon_Id = asistencia.Dom_Id;
                                    sw_bloqueado = asistencia.Dom_Status;
                                    break;
                                default:
                                    break;
                            }
                            asistenciaEnvia.Rhsis_Fecha = fecha.ToString("yyyy-MM-dd");
                            asistenciaEnvia.Rhsis_H50 = asistencia.H50;
                            asistenciaEnvia.Rhsis_H100 = asistencia.H100;
                            asistenciaEnvia.Rhsis_Atraso = asistencia.Atraso;
							if (string.IsNullOrEmpty(asistencia.Observacion)) 
                            {
                                asistenciaEnvia.Rhsis_Observacion = "";
                            }
                            else
							{
                                asistenciaEnvia.Rhsis_Observacion = asistencia.Observacion;
                            }
                            asistenciaEnvia.Accusu_Id = Convert.ToDecimal(Session["user_Id"]);
                            asistenciaEnvia.Rhcargen_Id = asistencia.Rhcargen_Id;
                            asistenciaEnvia.Rhref1_Id = asistencia.Rhref1_Id;
                            asistenciaEnvia.Rhref2_Id = asistencia.Rhref2_Id;
                            asistenciaEnvia.Rhcargen_Id_B = asistencia.Rhcargen_Id_Terreno;
                            asistenciaEnvia.Rhref1_Id_B = asistencia.Rhref1_Id_Terreno;
                            asistenciaEnvia.Rhref2_Id_B = asistencia.Rhref2_Id_Terreno;
                            if (!asistenciaEnvia.Rhasicon_Id.Equals(0) && !sw_bloqueado) // Si tiene un valor distinto de 0, y que no sea dato que viene de REX
                                lstAsistenciaEnvia.Add(asistenciaEnvia);
                            diaSemana += 1;
                            fecha = fecha.AddDays(1);
                        }
                    }

                }


                response = await client.PutAsJsonAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Asistencia + "/NewAsistencia", lstAsistenciaEnvia);
                contents = await response.Content.ReadAsStringAsync();
                Resultado resultadoExe = JsonConvert.DeserializeObject<Resultado>(contents);

                if (response.StatusCode.ToString().Equals("OK"))
                {
                    TempData["Estado"] = resultadoExe.Estado;
                    TempData["Mensaje"] = resultadoExe.Mensaje;
                }
                else
                {
                    TempData["Estado"] = "ERROR";
                    TempData["Mensaje"] = "Ha ocurrido un error inesperado al grabar la asistencia." + response.StatusCode.ToString();
                }

            }
            // FIN GRABACION 




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

            Rh_Asistencia_Concepto conceptoDefecto = new Rh_Asistencia_Concepto();
            conceptoDefecto.Rhasicon_Id = 0;
            conceptoDefecto.Rhasicon_Descripcion = "";
            conceptoDefecto.Rhasicon_Sigla = "---";
            conceptoDefecto.Rhasicon_Visible = "N";
            asistenciaConceptos.Insert(0, conceptoDefecto); //Insertar como primera opcion por defecto -- Seleccione --


            List<Rh_Asistencia_Concepto> asistenciaConceptosVisibles = asistenciaConceptos.Where(x => x.Rhasicon_Visible.Equals("S")).ToList(); //Filtra solo los conceptos visibles
            List<Rh_Asistencia_Concepto> asistenciaConceptosRexmas = asistenciaConceptos.Where(x => new[] { "0", "7", "8", "9", "13", "14", "15", "16", "17", "22", "23", "24", "27", "28", "29", "31", "32", "33" }.Contains(x.Rhasicon_Id.ToString())).ToList();
            List<Rh_Asistencia_Concepto> asistenciaConceptosInternos = asistenciaConceptos.Where(x => new[] { "0", "9", "11", "14", "15", "16", "17", "19", "20", "21", "23", "24", "25", "26", "27", "28", "29", "31", "32", "33" }.Contains(x.Rhasicon_Id.ToString())).ToList();

            


            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_CargoGenerico + "/GetAllCargoGenerico");
            List<Rh_Cargo_Generico> cargosGenericos = JsonConvert.DeserializeObject<List<Rh_Cargo_Generico>>(json);
            Rh_Cargo_Generico cargoDefecto = new Rh_Cargo_Generico();
            cargoDefecto.Rhcargen_Id = 0;
            cargoDefecto.Rhcargen_Descripcion = "-- Seleccione --";
            cargosGenericos = cargosGenericos.Where(x => x.Rhcargen_Vigencia.Equals("S")).ToList(); //Solo vigentes
            cargosGenericos = cargosGenericos.OrderBy(x => x.Rhcargen_Descripcion).ToList(); //Ordenado alfabetico
            cargosGenericos.Insert(0, cargoDefecto); //Insertar como primera opcion por defecto -- Seleccione --

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Referencia1 + "/GetAllReferencia1");
            List<Rh_Referencia_1> referencias1 = JsonConvert.DeserializeObject<List<Rh_Referencia_1>>(json);
            Rh_Referencia_1 ref1Defecto = new Rh_Referencia_1();
            ref1Defecto.Rhref1_Id = 0;
            ref1Defecto.Rhref1_Descripcion = "-- Seleccione --";
            referencias1 = referencias1.Where(x => x.Rhref1_Vigencia.Equals("S")).ToList(); //Solo carga vigentes
            referencias1 = referencias1.OrderBy(x => x.Rhref1_Descripcion).ToList(); //Ordenado alfabetico
            referencias1.Insert(0, ref1Defecto); //Insertar como primera opcion por defecto -- Seleccione --

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Referencia2 + "/GetAllReferencia2");
            List<Rh_Referencia_2> referencias2 = JsonConvert.DeserializeObject<List<Rh_Referencia_2>>(json);
            Rh_Referencia_2 ref2Defecto = new Rh_Referencia_2();
            ref2Defecto.Rhref2_Id = 0;
            ref2Defecto.Rhref2_Descripcion = "-- Seleccione --";
            referencias2 = referencias2.Where(x => x.Rhref2_Vigencia.Equals("S")).ToList(); //Solo carga vigentes
            referencias2 = referencias2.OrderBy(x => x.Rhref2_Descripcion).ToList(); //Ordenado alfabetico
            referencias2.Insert(0, ref2Defecto); //Insertar como primera opcion por defecto -- Seleccione --

            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiPlanillaUnica"] + Resource.PlanillaUnica_Asistencia + "/GetAsistenciaLista?centroCosto=" + filtroPlanilla.Gencencos_Id + "&periodo=" + filtroPlanilla.Periodo + "&fechaInicio=" + filtroPlanilla.FechaInicio + "&fechaTermino=" + filtroPlanilla.FechaTermino);
            List<AsistenciaLista> asistenciaList = JsonConvert.DeserializeObject<List<AsistenciaLista>>(json);

            int contDiasConValor;

            // Recorremos salida del list para adecuar las muestra en vista, colores, valores 
            foreach (var asistencia in asistenciaList)
            {
                contDiasConValor = 0;

                // Para contar los dias que vienen con valor y dar color al registro en la vista (circulo color antes del nombre del colaborador)
                if (asistencia.Lun_Id > 0) contDiasConValor += 1;
                if (asistencia.Mar_Id > 0) contDiasConValor += 1;
                if (asistencia.Mie_Id > 0) contDiasConValor += 1;
                if (asistencia.Jue_Id > 0) contDiasConValor += 1;
                if (asistencia.Vie_Id > 0) contDiasConValor += 1;
                if (asistencia.Sab_Id > 0) contDiasConValor += 1;
                if (asistencia.Dom_Id > 0) contDiasConValor += 1;


                switch (contDiasConValor)
                {
                    case 0:
                        asistencia.CircleColorColaborador = "circulo-rojo";
                        break;
                    case 7:
                        asistencia.CircleColorColaborador = "circulo-verde";
                        break;
                    default:
                        asistencia.CircleColorColaborador = "circulo-amarillo";
                        break;
                }

            }

            myModel.Add(empresasList);
            myModel.Add(centrosCostoList);
            myModel.Add(anosList);
            myModel.Add(asistenciaConceptos);

            myModel.Add(asistenciaConceptosVisibles);
            myModel.Add(asistenciaConceptosRexmas);
            myModel.Add(asistenciaConceptosInternos);

            myModel.Add(filtroPlanilla);
            myModel.Add(cargosGenericos);
            myModel.Add(referencias1);
            myModel.Add(referencias2);
            myModel.Add(asistenciaList);

            ViewBag.Message2 = "PLANILLAASISTENCIA";
            return View(myModel);
        }

      
    }
}