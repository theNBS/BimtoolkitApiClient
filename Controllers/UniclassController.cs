using BIMToolkitAPIClient.Dal;
using BIMToolkitAPIClient.Models;
using BIMToolkitAPIClient.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIMToolkitAPIClient.Controllers
{
    public class UniclassController : Controller
    {
        // GET: Uniclass
        public ActionResult Index(string notation, string lod, string loi)
        {
            //generate link back to parent
            var parts = notation.Split('_');
            if(parts.Length <= 1)
            {
                ViewBag.BackLink = "/";
            }
            else
            {
                 ViewBag.BackLink = "/Uniclass?notation=" + string.Join("_", parts.Take(parts.Length -1));
            }

            //get information for current Unicalss notation
            var model = new ClassificationPage();
            var cn = ApiAccess.CallApi(string.Format("definitions/uniclass2015/{0}/1", notation));
            model.Classification = JsonConvert.DeserializeObject<ClassificationNode>(cn);

            //check querstring for specified lod and loi levels
            if (Request.QueryString["lod"] != null)
            {
                ViewBag.LodLevel = lod;
            }
            else if (model.Classification.Lods != null)
            {
                ViewBag.LodLevel = model.Classification.Lods.First();
            }
            else
            {
                ViewBag.LodLevel = "0";
            }
            if (Request.QueryString["loi"] != null)
            {
                ViewBag.LoiLevel = loi;
            }
            else if (model.Classification.Lois != null)
            {
                ViewBag.LoiLevel = model.Classification.Lois.First();
            }
            else
            {
                ViewBag.LoiLevel = "0";
            }


            //check if LOI definitions are available
            if (model.Classification.Lois != null)
            {
                //get json and deserialize
                var loiJson = ApiAccess.CallApi(string.Format("definitions/loi/{0}/{1}", notation, ViewBag.LoiLevel));
                var objectLoi = JsonConvert.DeserializeObject<Definition>(loiJson);

                //for this simple example, we are just going to send the raw json data back to the view with a bit of formatting 
                model.Loi = JsonConvert.SerializeObject(objectLoi, Formatting.Indented);
            }

            return View(model);
        }
    }
}