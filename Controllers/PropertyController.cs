using BIMToolkitAPIClient.Dal;
using BIMToolkitAPIClient.Models;
using BIMToolkitAPIClient.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIMToolkitAPIClient.Controllers
{
    public class PropertyController : Controller
    {
        // GET: ClauseItemDescription
        public ActionResult Index()
        {         
            var model = new Properties();
            var propertyJson = ApiAccess.CallApi(string.Format("properties/properties"));
            model.PropertiesList.AddRange(JsonConvert.DeserializeObject<List<Property>>(propertyJson));            

            //for this simple example, we are just going to send the raw json data back to the view with a bit of formatting 
            //model.Json = JsonConvert.SerializeObject(model.PropertiesList, Formatting.Indented);

            return View(model);
        }

        public ActionResult Property(string searchTerm)
        {
            if (String.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction("Index", "Properties");
            }

            var model = new Properties();

            string propertyJson;
            int authEnvID;
            bool successfullyParsed = int.TryParse(searchTerm, out authEnvID);

            // search by ID
            if (successfullyParsed)
            {
                propertyJson = ApiAccess.CallApi(string.Format("properties/propertybyid/{0}", searchTerm));
                Property propertyById = JsonConvert.DeserializeObject<Property>(propertyJson);
                model.PropertiesList.Add(propertyById);

                ViewBag.Title = string.Format("{0} | {1}", propertyById.LongName, propertyById.ID);
            }
            // or by first few letters of name
            else
            {
                propertyJson = ApiAccess.CallApi(string.Format("properties/propertiesbyname/{0}", searchTerm));
                List<Property> propertiesByName = JsonConvert.DeserializeObject<List<Property>>(propertyJson);
                model.PropertiesList.AddRange(propertiesByName);

                ViewBag.Title = string.Format("{0} results beginning with '{1}'", propertiesByName.Count(), searchTerm.Trim());
            }
            
            model.Json = FormatJson(propertyJson);

            if (String.IsNullOrWhiteSpace(model.Json) || model.Json == "null")
            {
                return RedirectToAction("Index", "Property");
            }

            return View(model);
        }

        public static string FormatJson(string json)
        {
            return JToken.Parse(json).ToString();
        }

    }
}