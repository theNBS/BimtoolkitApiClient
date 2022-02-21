using UniclassAPIClient.Dal;
using UniclassAPIClient.Models;
using UniclassAPIClient.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace UniclassAPIClient.Controllers
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

            return View(model);
        }
    }
}