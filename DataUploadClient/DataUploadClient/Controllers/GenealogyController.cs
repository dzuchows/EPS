using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataUploadApi;

namespace DataUploadClient.Controllers
{

    public class GenealogySummaryModel
    {
        public string bielectrodeId { get; set; }

        public ElectrodeThickness thickness { get; set; }

        public ElectrodeWeight weight { get; set; }
    }

    public class GenealogyUploadModel
    {
        public SelectList genealogyTypeList { get; set; }

        public string selectedType { get; set; }

        public HttpPostedFileBase file { get; set; }
    }

    public class GenealogyController : Controller
    {
        //
        // GET: /Genealogy/

        public ActionResult Index()
        {
            return View("Index", getGenealogyUploads());
        }


        public ActionResult Summary(string bielectrodeId)
        {
            BielectrodeGenealogyRepository repository = new BielectrodeGenealogyRepository();
            GenealogySummaryModel model = new GenealogySummaryModel();

            model.thickness = repository.getThickness(bielectrodeId);
            model.weight = repository.getWeight(bielectrodeId);
            model.bielectrodeId = bielectrodeId;

            return View("Summary", model);
        }

        public ActionResult GenealogyUpload()
        {
            GenealogyUploadModel model = new GenealogyUploadModel();


            Dictionary<string, string> items1 = new Dictionary<string, string>();
            items1.Add("WEIGHT", "WEIGHT");
            items1.Add("THICKNESS", "THICKNESS");
            model.genealogyTypeList = new SelectList(items1, "Key", "Value");

            return View("GenealogyUpload", model);

        }

        [HttpPost]
        public ActionResult GenealogyUpload(GenealogyUploadModel model)
        {
            var file = model.file;

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var path = model.selectedType == "WEIGHT" ? Path.Combine(Configuration.GenealogyWeightDropDirectory, Path.GetFileName(model.file.FileName)) : Path.Combine(Configuration.GenealogyThicknessDropDirectory, Path.GetFileName(model.file.FileName));
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("UploadIndex");
        }

        public ActionResult UploadIndex()
        {
            return View("UploadIndex", getGenealogyUploads());
        }

        private IEnumerable<ElectrodeGenealogySummary> getGenealogyUploads()
        {
            BielectrodeGenealogyRepository repository = new BielectrodeGenealogyRepository();

            return repository.getElectrodeGenealogySummary();
        }

    }
}
