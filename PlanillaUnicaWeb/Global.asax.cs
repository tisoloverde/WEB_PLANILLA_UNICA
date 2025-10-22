using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;

namespace PlanillaUnicaWeb
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			// Registrar ModelBinder personalizado para decimales
			ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
			ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}

	// ModelBinder para decimales (ASP.NET MVC 5 - sintaxis correcta)
	public class DecimalModelBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			
			if (value != null && !string.IsNullOrEmpty(value.AttemptedValue))
			{
				var stringValue = value.AttemptedValue.Replace(',', '.');
				
				if (decimal.TryParse(stringValue, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result))
				{
					bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
					return result;
				}
			}
			
			return null;
		}
	}
}
