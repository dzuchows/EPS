using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataUploader.Models;

namespace DataUploader.Controllers
{
    public class DataUploadController : Controller
    {
        //
        // GET: /DataUpload/


        public ActionResult Index()
        {
            ViewBag.Message = "Default data upload controller";

            return View();
        }

        public ActionResult UploadHistory()
        {
            ViewBag.Message = "Upload History / Status";

            return View("UploadHistory", getTestUploadHistory());
        }

        public ActionResult FileUpload()
        {
            ViewBag.Message = "New Data Upload";

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Arbin", Value = "0", Selected = true });
            items.Add(new SelectListItem { Text = "Cycler #2", Value = "1" });
            items.Add(new SelectListItem { Text = "Cycler #3", Value = "2" });
            items.Add(new SelectListItem { Text = "Cycler #4", Value = "3" });

            ViewBag.Cycler = items;



            return View("FileUpload");
        }

        [HttpPost]
        public ActionResult FileUpload(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine("C:\\qa_data\\drop\\", fileName);
                        file.SaveAs(path);                        
                    }
                }
            }
            return RedirectToAction("Index");
        }



        private IList<UploadHistory> getTestUploadHistory()
        {
            IList<UploadHistory> data = new List<UploadHistory>();

            data.Add(new UploadHistory("EPS_MOd095_Formation_06Aug12", DateTime.Parse("9/21/2012"), "Loading"));
            data.Add(new UploadHistory("EPS_Mod095_C2_80_08Sep12", DateTime.Parse("9/21/2012"), "Pending"));
            data.Add(new UploadHistory("EPS_Mod095_C2_27Aug12", DateTime.Parse("9/21/2012"), "Pending"));

            return data;
        }
    }
}
