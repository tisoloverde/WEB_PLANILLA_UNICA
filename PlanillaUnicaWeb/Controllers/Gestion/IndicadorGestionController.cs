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

namespace PlanillaUnicaWeb.Controllers.Gestion
{
    public class IndicadorGestionController : BaseController
    {
        List<Object> myModel = new List<object>();

        // GET: IndicadorGestion
        public async Task<ActionResult> Index()
        {
            decimal usuarioId = Convert.ToDecimal(Session["user_Id"]);

            var httpClient = new HttpClient();
            
            // Cargar empresas
            var json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Empresa);
            List<Gen_Empresa> empresasList = JsonConvert.DeserializeObject<List<Gen_Empresa>>(json);

            // Cargar todos los centros de costo (no filtrar por usuario para tener opción TODOS)
            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_CentrosCosto + "/GetAllCentroCosto");
            List<Gen_Centro_Costo> centrosCostoList = JsonConvert.DeserializeObject<List<Gen_Centro_Costo>>(json);
            
            // Agregar opción TODOS al inicio
            Gen_Centro_Costo centroTodos = new Gen_Centro_Costo();
            centroTodos.Gencencos_Id = 0;
            centroTodos.Gencencos_Codigo = "TODOS";
            centroTodos.Gencencos_Descripcion = "TODOS";
            centrosCostoList.Insert(0, centroTodos);

            // Cargar años (similar a planilla única)
            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Ano + "/GetAllAnos?agregaMas=0&agregaMenos=5");
            List<Gen_Ano> anosList = JsonConvert.DeserializeObject<List<Gen_Ano>>(json);

            // Cargar años (similar a planilla única)
            json = await httpClient.GetStringAsync(ConfigurationManager.AppSettings["ApiGenerica"] + Resource.Generica_Mes);
            List<Gen_Mes> mesesList = JsonConvert.DeserializeObject<List<Gen_Mes>>(json);

            // Crear lista de tipos de informe
            List<Rh_Tipo_Informe_Indicador> tiposInformeList = new List<Rh_Tipo_Informe_Indicador>
            {
                new Rh_Tipo_Informe_Indicador { Id = 1, Descripcion = "7 días" },
                new Rh_Tipo_Informe_Indicador { Id = 2, Descripcion = "15 días" },
                new Rh_Tipo_Informe_Indicador { Id = 3, Descripcion = "Mensual" },
                new Rh_Tipo_Informe_Indicador { Id = 4, Descripcion = "Tercera Semana" }
            };

            // Crear modelo de filtro con valores por defecto
            FiltroIndicadorGestion filtroIndicador = new FiltroIndicadorGestion
            {
                EmpresaId = empresasList.FirstOrDefault()?.Genemp_Id ?? 0,
                CentroCostoId = 0, // TODOS por defecto
                Ano = DateTime.Now.Year, // Año actual
                Mes = DateTime.Now.Month, // Mes actual
                TipoInforme = 3 // Mensual por defecto
            };

            myModel.Add(empresasList);
            myModel.Add(centrosCostoList);
            myModel.Add(anosList);
            myModel.Add(mesesList);
            myModel.Add(tiposInformeList);
            myModel.Add(filtroIndicador);

            ViewBag.Message2 = "INDICADORES_GESTION";
            return View(myModel);
        }
    }
}