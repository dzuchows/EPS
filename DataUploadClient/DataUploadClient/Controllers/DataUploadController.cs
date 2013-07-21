using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using DataUploadApi;
using DataUploadApi.model;
using DataUploadApi.repository;

namespace DataUploadClient.Controllers
{

    public class FileUploadModel
    {
        public SelectList cyclerList { get; set; }

        public string selectedCycler { get; set; }

        public SelectList testTypeList { get; set; }

        public string selectedTestType { get; set; }

        public HttpPostedFileBase file { get; set; }
    }

    public class SummaryModel
    {
        public ModuleTest test { get; set; }

        public string fileName { get; set; }

        public string testType { get; set; }

        public string cycler { get; set; }
    }

    public class DataUploadController : Controller
    {

        //
        // GET: /DataUpload/
        private FileUploadModel fileUploadModel = new FileUploadModel();


        public ActionResult Index()
        {
            ViewBag.Message = "Upload History / Status";

            return View("Index", getTestUploadHistory());
        }

        public ActionResult UploadHistory()
        {
            ViewBag.Message = "Upload History / Status";

            return View("UploadHistory", getTestUploadHistory());
        }

        public ActionResult FileUpload()
        {
            ViewBag.Message = "New Data Upload";

            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("Arbin", "Arbin");
            items.Add("PEC", "PEC");
            items.Add("Firing Circuits", "Firing Circuits");
            fileUploadModel.cyclerList = new SelectList(items, "Key", "Value");


            Dictionary<string, string> items1 = new Dictionary<string, string>();
            items1.Add("CONDITIONING", "CONDITIONING");
            items1.Add("FORMATION", "FORMATION");

            fileUploadModel.testTypeList = new SelectList(items1, "Key", "Value");

            return View("FileUpload", fileUploadModel);
        }

        [HttpPost]
        public ActionResult FileUpload(FileUploadModel fileUploadModel)
        {   
            int index=1;
            var file = fileUploadModel.file;
            string fileName = generateNewFileName(fileUploadModel);

                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var path = Path.Combine(getCyclerPendingDirectory(fileUploadModel.selectedCycler), fileName);
                        file.SaveAs(path);
                        index++;
                    }
                }

                return RedirectToAction("Summary", new { fileName = fileName, testType = fileUploadModel.selectedTestType, cycler = fileUploadModel.selectedCycler });
        }

        private string generateNewFileName(FileUploadModel fileUploadModel)
        {
            string newFileName = System.IO.Path.GetFileNameWithoutExtension(fileUploadModel.file.FileName);
            string extension = System.IO.Path.GetExtension(fileUploadModel.file.FileName);
            newFileName += "_" + fileUploadModel.selectedTestType + extension;

            return newFileName;
        }

        public ActionResult Summary(string fileName, string testType, string cycler)
        {
            SummaryModel summaryModel = new SummaryModel();
            summaryModel.cycler = cycler;
            ImpedanceTest impedanceTest = null;

            if (cycler == "Arbin")
            {
                ArbinExcelDataSource dataSource = new ArbinExcelDataSource(Path.Combine(getCyclerPendingDirectory(cycler), fileName));
                ArbinTest arbinTest = dataSource.getTestResults();
                summaryModel.test = arbinTest;
                impedanceTest = arbinTest;
            }

            if (cycler == "PEC")
            {
                PECCSVDataSource dataSource = new PECCSVDataSource(Path.Combine(getCyclerPendingDirectory(cycler), fileName));
                PECTest pecTest = dataSource.getTestResults();
                summaryModel.test = pecTest;
                impedanceTest = pecTest;
            }

            if (cycler == "Firing Circuits")
            {
                FiringCircuitsCSVDataSource dataSource = new FiringCircuitsCSVDataSource(Path.Combine(getCyclerPendingDirectory(cycler), fileName));
                FiringCircuitsTest firingCircuitsTest = dataSource.getTestResults();
                summaryModel.test = firingCircuitsTest;

                impedanceTest = firingCircuitsTest;
            }

            if (impedanceTest != null)
            {
                summaryModel.fileName = fileName;
                summaryModel.testType = testType != null ? testType : getTestType(fileName);

                return View("Summary", summaryModel);
            }
            else
            {
                return null;
            }
        }


        public ActionResult CompletedSummary(string fileName, string testType, string cycler)
        {
            SummaryModel summaryModel = new SummaryModel();

            if (cycler == "Arbin")
            {
                ArbinExcelDataSource dataSource = new ArbinExcelDataSource(Path.Combine(getCyclerCompletedDirectory(cycler), fileName));
                summaryModel.test = dataSource.getTestResults();
            }
            else if (cycler == "Firing Circuits")
            {
                FiringCircuitsCSVDataSource dataSource = new FiringCircuitsCSVDataSource(Path.Combine(getCyclerCompletedDirectory(cycler), fileName));
                summaryModel.test = dataSource.getTestResults();
            }
            else if (cycler == "PEC")
            {
                PECCSVDataSource dataSource = new PECCSVDataSource(Path.Combine(getCyclerCompletedDirectory(cycler), fileName));
                summaryModel.test = dataSource.getTestResults();
            }
            else
            {
                throw new ArgumentException("Unknown test type");
            }

            summaryModel.cycler = cycler;
            summaryModel.fileName = fileName;
            summaryModel.testType = testType != null ? testType : getTestType(fileName);

            return View("CompletedSummary", summaryModel);
        }

        private static string getCyclerCompletedDirectory(string cycler)
        {
            if (cycler.Equals(("Arbin")))
            {
                return Configuration.CompletedDirectory;
            }
            else if (cycler.Equals("Firing Circuits"))
            {
                return Configuration.FiringCircuitsCompletedDirectory;
            }
            else if (cycler.Equals(("PEC")))
            {
                return Configuration.PECCompletedDirectory;
            }
            else
            {
                throw new ArgumentException("Unnown test type");
            }
        }

        private static string getCyclerPendingDirectory(string cycler)
        {
            if (cycler.Equals(("Arbin")))
            {
                return Configuration.PendingDirectory;
            }
            else if (cycler.Equals("Firing Circuits"))
            {
                return Configuration.FiringCircuitsPendingDirectory;
            }
            else if (cycler.Equals(("PEC")))
            {
                return Configuration.PECPendingDirectory;
            }
            else
            {
                throw new ArgumentException("Unnown test type");
            }
        }

        
        private static string getCyclerDropDirectory(string cycler)
        {
            if (cycler.Equals(("Arbin")))
            {
                return Configuration.DropDirectory;
            }
            else if (cycler.Equals("Firing Circuits"))
            {
                return Configuration.FiringCircuitsDropDirectory;
            }
            else if (cycler.Equals(("PEC")))
            {
                return Configuration.PECDropDirectory;
            }
            else
            {
                throw new ArgumentException("Unnown test type");
            }
        }

        public ActionResult CompletedDownload(string fileName, string cycler)
        {
            if (System.IO.File.Exists(Path.Combine(getCyclerCompletedDirectory(cycler), fileName)))
                return File(Path.Combine(getCyclerCompletedDirectory(cycler), fileName), "application/octet-stream", fileName);
            else
                return RedirectToAction("Index");
        }

        public ActionResult Download(string fileName, string cycler)
        {
            if (System.IO.File.Exists(Path.Combine(getCyclerPendingDirectory(cycler), fileName)))
                return File(Path.Combine(getCyclerPendingDirectory(cycler), fileName), "application/octet-stream", fileName);
            else
                return RedirectToAction("Index"); 
        }

        public ActionResult ProcessUpload(string fileName, string cycler)
        {
            if (System.IO.File.Exists(Path.Combine(getCyclerPendingDirectory(cycler), fileName)))
            {
                System.IO.File.Copy(Path.Combine(getCyclerPendingDirectory(cycler), fileName), Path.Combine(getCyclerDropDirectory(cycler), fileName), true);
                System.IO.File.Delete(Path.Combine(getCyclerPendingDirectory(cycler), fileName));                
            }
            return RedirectToAction("Index");
        }

        public ActionResult CancelUpload(string fileName, string cycler)
        {
            System.IO.File.Delete(Path.Combine(getCyclerPendingDirectory(cycler), fileName));
            return RedirectToAction("Index");
        }


        public ActionResult buildFiringCircuitsConditioningChargeChart(FiringCircuitsTest test)
        {
            return buildConditioningChargeChartInternal(test.TestResults);
        }

        public ActionResult buildConditioningChargeChart(ArbinTest test)
        {
            return buildConditioningChargeChartInternal(test.TestResults);
        }

        public ActionResult buildPECConditioningChargeChart(PECTest test, string key)
        {
            return buildConditioningChargeChartInternal(test.TestResults[key]);
        }


        private ActionResult buildConditioningChargeChartInternal(IEnumerable<ImpedanceTestData> testData )
        {
            // Build Chart
            var chart = new Chart();

            // Create chart here
            chart.Width = 400;
            chart.Height = 300;

            Title title = new Title();
            title.Text = "Charge & Discharge Capacity";
            title.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 10F, FontStyle.Bold);
            chart.Titles.Add(title);


            chart.ChartAreas.Add(CreateChartArea("Charge & Discharge Capacity"));
            chart.Series.Add(ChargeCapacitySeries(testData, "Charge & Discharge Capacity", "Charge"));
            chart.Series.Add(DischargeCapacitySeries(testData, "Charge & Discharge Capacity", "Discharge"));
            chart.ChartAreas[0].AxisY.Title = "Ah";

            StringBuilder result = new StringBuilder();
            result.Append(getChartImage(chart));
            result.Append(chart.GetHtmlImageMap("ImageMap"));
            return Content(result.ToString());
        }

        public ActionResult buildFormationChargeChart(ArbinTest test)
        {
             // Build Chart
            var chart = new Chart();

            // Create chart here
            chart.Width = 400;
            chart.Height = 300;
            Title title = new Title();
            title.Text = "Charge & Discharge Capacity";
            title.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 10F, FontStyle.Bold);
            chart.Titles.Add(title);
            chart.ChartAreas.Add(CreateChartArea("Charge & Discharge Capacity"));
            chart.Series.Add(FormationChargeCapacitySeries(test, "Charge & Discharge Capacity", "Charge"));
            chart.Series.Add(FormationDischargeCapacitySeries(test, "Charge & Discharge Capacity", "Discharge"));

            chart.ChartAreas[0].AxisY.Title = "Ah";

            StringBuilder result = new StringBuilder();
            result.Append(getChartImage(chart));
            result.Append(chart.GetHtmlImageMap("ImageMap"));
            return Content(result.ToString());
        }

        public ActionResult buildConditioningImpedanceChart(ArbinTest test)
        {
            return buildConditioningImpedanceChartInternal(test.TestResults);
        }

        public ActionResult buildFiringCircuitsConditioningImpedanceChart(FiringCircuitsTest test)
        {
            return buildConditioningImpedanceChartInternal(test.TestResults);
        }

        public ActionResult buildPECConditioningImpedanceChart(PECTest test, string key)
        {
            return buildConditioningImpedanceChartInternal(test.TestResults[key]);
        }

        private ActionResult buildConditioningImpedanceChartInternal(IEnumerable<ImpedanceTestData> testData)
        {
            // Build Chart
            var chart = new Chart();

            // Create chart here
            chart.Width = 400;
            chart.Height = 300;

            Title title = new Title();
            title.Text = "Impedance";
            title.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 10F, FontStyle.Bold);
            chart.Titles.Add(title);


            chart.ChartAreas.Add(CreateChartArea("Impedance"));
            chart.Series.Add(ChannelingImpedanceChart(testData, "Impedance", "Impedance"));

            chart.ChartAreas[0].AxisY.Title = "R";

            StringBuilder result = new StringBuilder();
            result.Append(getChartImage(chart));
            result.Append(chart.GetHtmlImageMap("ImageMap"));
            return Content(result.ToString());
        }



        private ChartArea CreateChartArea(string name)
        {
            ChartArea chartArea = new ChartArea();
            chartArea.Name = name;
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
           //chartArea.AxisX.TextOrientation = TextOrientation.
            
            return chartArea;
        }


        public Series FormationChargeCapacitySeries(ArbinTest test, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;

            var filterIndicies = new int[] {16, 23, 31, 37, 44};
            var filteredResults = from t in test.TestResults
                          where t.StepIndex == 16 || t.StepIndex == 23
                            || t.StepIndex == 31 || t.StepIndex == 37
                            || t.StepIndex == 44
                          select t;

            var Results = from t in filteredResults
                      where t.StepTime < 125                              
                      select new { t.ChargeCapacity, t.DischargeCapacity };

            foreach (var r in Results)
            {
                var p1 = seriesDetail.Points.Add(r.ChargeCapacity);
            }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }

        public Series ChannelingImpedanceChart(IEnumerable<ImpedanceTestData> testData, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;

            var filteredResults = from t in testData
                                  where t.StepIndex == 7
                                  group t by t.CycleIndex into r
                                  select new { R = Math.Abs(r.Average(c => c.Current)) / r.Average(c => c.Voltage) };

            foreach (var r in filteredResults)
            {
                var p1 = seriesDetail.Points.Add(r.R);
            }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }


        public Series FormationDischargeCapacitySeries(ArbinTest test, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;

            var filterIndicies = new int[] { 16, 23, 31, 37, 44 };
            var filteredResults = from t in test.TestResults
                                  where t.StepIndex == 16 || t.StepIndex == 23
                                    || t.StepIndex == 31 || t.StepIndex == 37
                                    || t.StepIndex == 44
                                  select t;

            var Results = from t in filteredResults
                          where t.StepTime < 125
                          select new { t.ChargeCapacity, t.DischargeCapacity };

            foreach (var r in Results)
            {
                var p1 = seriesDetail.Points.Add(r.DischargeCapacity);
            }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }

        public Series ChargeCapacitySeries(IEnumerable<ImpedanceTestData> testData, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;

            var Results = from t in testData
                          where t.StepIndex == 15
                          select new { t.ChargeCapacity, t.DischargeCapacity };

            foreach (var r in Results)
            {
                var p1 = seriesDetail.Points.Add(r.ChargeCapacity);
             }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }

        public Series DischargeCapacitySeries(IEnumerable<ImpedanceTestData> testData, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(99, 198, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;

            var Results = from t in testData
                          where t.StepIndex == 15
                          select new { t.ChargeCapacity, t.DischargeCapacity };

            foreach (var r in Results)
            {
                var p2 = seriesDetail.Points.Add(r.DischargeCapacity);
            }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }


        public Series FiringCircuitsChargeCapacitySeries(FiringCircuitsTest test, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;

            var Results = from t in test.TestResults
                          where t.Step == 15
                          select new { t.AhCha, t.AhDch };

            foreach (var r in Results)
            {
                var p1 = seriesDetail.Points.Add(r.AhCha);
            }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }

        public Series FiringCircuitsDischargeCapacitySeries(FiringCircuitsTest test, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(99, 198, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;


            var Results = from t in test.TestResults
                          where t.Step == 15
                          select new { t.AhCha, t.AhDch };

            foreach (var r in Results)
            {
                var p2 = seriesDetail.Points.Add(r.AhDch);
            }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }




        private string getChartImage(Chart chart)
        {
            using (var stream = new MemoryStream())
            {
                string img = "<img src='data:image/png;base64,{0}' alt='' usemap='#ImageMap'>";
                chart.SaveImage(stream, ChartImageFormat.Png);
                string encoded = Convert.ToBase64String(stream.ToArray());
                return String.Format(img, encoded);
            }
        }


        private IEnumerable<UploadHistory> getTestUploadHistory()
        {
            IEnumerable<UploadHistory> data = new List<UploadHistory>();

            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.PendingDirectory, "PENDING APPROVAL", "Arbin"));
            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.PECPendingDirectory, "PENDING APPROVAL", "PEC"));
            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.FiringCircuitsPendingDirectory, "PENDING APPROVAL", "Firing Circuits"));

            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.ProcessingDirectory, "PROCESSING", "Arbin"));
            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.PECProcessingDirectory, "PROCESSING", "PEC"));
            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.FiringCircuitsProcessingDirectory, "PROCESSING", "Firing Circuits"));

            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.CompletedDirectory, "COMPLETED", "Arbin"));
            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.PECCompletedDirectory, "COMPLETED", "PEC"));
            data = data.Concat<UploadHistory>(UploadRepository.getFiles(Configuration.FiringCircuitsCompletedDirectory, "COMPLETED", "Firing Circuits"));

            data = data.OrderByDescending(x => x.UploadTimeStamp).ToList(); 

            return data;
        }

        private string getTestType(string fileName)
        {
            if (fileName.Contains("FORMATION"))
            {
                return "FORMATION";
            }
            else if (fileName.Contains("CONDITIONING"))
            {
                return "CONDITIONING";
            }
            else
            {
                return "UNKNOWN";
            }
        }

    }
}
