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

namespace DataUploadClient.Controllers
{

    public class DataViewController : Controller
    {

        private String DropDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DropDirectory"];
            }
        }

        private String PendingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PendingDirectory"];
            }
        }

        private String CompletedDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CompletedDirectory"];
            }
        }

        private String ProcessingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProcessingDirectory"];
            }
        }

         

        //
        // GET: /DataView/

        public ActionResult Index()
        {
            ViewBag.Message = "Data View";

            return View("Index", getTestUploadHistory());
        }

        public ActionResult CompletedSummary(string fileName, string testType)
        {
            SummaryModel summaryModel = new SummaryModel();

            ArbinExcelDataSource dataSource = new ArbinExcelDataSource(Path.Combine(CompletedDirectory, fileName));
            ArbinTest test = dataSource.getTestResults();

            var Results = from t in test.TestResults
                          where t.StepIndex == 15
                          select t;

            summaryModel.fileName = fileName;
            summaryModel.test = test;
            summaryModel.testType = testType != null ? testType : getTestType(fileName);

            return View("CompletedSummary", summaryModel);
        }


        public ActionResult CompletedDownload(string fileName)
        {
            if (System.IO.File.Exists(Path.Combine(CompletedDirectory, fileName)))
                return File(Path.Combine(CompletedDirectory, fileName), "application/octet-stream", fileName);
            else
                return RedirectToAction("Index");
        }

        public ActionResult buildConditioningChargeChart(ArbinTest test)
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
            chart.Series.Add(ChargeCapacitySeries(test, "Charge & Discharge Capacity", "Charge"));
            chart.Series.Add(DischargeCapacitySeries(test, "Charge & Discharge Capacity", "Discharge"));
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
            chart.Series.Add(ChannelingImpedanceChart(test, "Impedance", "Impedance"));

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
                var p1 = seriesDetail.Points.Add(r.ChargeCapacity);
            }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }

        public Series ChannelingImpedanceChart(ArbinTest test, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;

            var filteredResults = from t in test.TestResults
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

        public Series ChargeCapacitySeries(ArbinTest test, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;

            var Results = from t in test.TestResults
                          where t.StepIndex == 15
                          select new { t.ChargeCapacity, t.DischargeCapacity };

            foreach (var r in Results)
            {
                var p1 = seriesDetail.Points.Add(r.ChargeCapacity);
            }

            seriesDetail.ChartArea = chartArea;
            return seriesDetail;
        }

        public Series DischargeCapacitySeries(ArbinTest test, string chartArea, string chartSeries)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = chartSeries;
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(99, 198, 99);
            seriesDetail.ChartType = SeriesChartType.Line;
            seriesDetail.BorderWidth = 2;


            var Results = from t in test.TestResults
                          where t.StepIndex == 15
                          select new { t.ChargeCapacity, t.DischargeCapacity };

            foreach (var r in Results)
            {
                var p2 = seriesDetail.Points.Add(r.DischargeCapacity);
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

            data = data.Concat<UploadHistory>(UploadRepository.getFiles(ProcessingDirectory, "PROCESSING", "Arbin"));
            data = data.Concat<UploadHistory>(UploadRepository.getFiles(CompletedDirectory, "COMPLETED", "Arbin"));

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
