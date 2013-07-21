using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataUploadApi;
using DataUploadApi.model;
using DataUploadApi.repository;

namespace DataUploadService
{

    public partial class EPSDataUploadService : ServiceBase
    {
        public static string SERVICE_NAME = "DEV - EPS Data Upload Service";

        public EPSDataUploadService()
        {
            InitializeComponent();
            EventLog.Log = SERVICE_NAME;
        }

        protected override void OnStart(string[] args)
        {
            try
            {

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher arbinTestDropWatcher = new FileSystemWatcher();
            FileSystemWatcher arbinTestProcessWatcher = new FileSystemWatcher();

            FileSystemWatcher firingcircuitsTestDropWatcher = new FileSystemWatcher();
            FileSystemWatcher firingcircuitsTestProcessWatcher = new FileSystemWatcher();

            FileSystemWatcher pecTestDropWatcher = new FileSystemWatcher();
            FileSystemWatcher pecTestProcessWatcher = new FileSystemWatcher();

            FileSystemWatcher genealogyThicknessProcessWatcher = new FileSystemWatcher();
            FileSystemWatcher genealogyThicknessDropWatcher = new FileSystemWatcher();

            FileSystemWatcher genealogyWeightProcessWatcher = new FileSystemWatcher();
            FileSystemWatcher genealogyWeightDropWatcher = new FileSystemWatcher();


            arbinTestDropWatcher.Path = Configuration.DropDirectory;
            arbinTestDropWatcher.Filter = "*.*";

            arbinTestProcessWatcher.Path = Configuration.ProcessingDirectory;
            arbinTestProcessWatcher.Filter = "*.*";

            firingcircuitsTestDropWatcher.Path = Configuration.FiringCircuitsDropDirectory;
            firingcircuitsTestDropWatcher.Filter = "*.*";

            firingcircuitsTestProcessWatcher.Path = Configuration.FiringCircuitsProcessingDirectory;
            firingcircuitsTestProcessWatcher.Filter = "*.*";

            pecTestDropWatcher.Path = Configuration.PECDropDirectory;
            pecTestDropWatcher.Filter = "*.*";

            pecTestProcessWatcher.Path = Configuration.PECProcessingDirectory;
            pecTestProcessWatcher.Filter = "*.*";

            genealogyThicknessProcessWatcher.Path = Configuration.GenealogyThicknessProcessingDirectory;
            genealogyThicknessProcessWatcher.Filter = "*.*";

            genealogyThicknessDropWatcher.Path = Configuration.GenealogyThicknessDropDirectory;
            genealogyThicknessDropWatcher.Filter = "*.*";

            genealogyWeightProcessWatcher.Path = Configuration.GenealogyWeightProcessingDirectory;
            genealogyWeightProcessWatcher.Filter = "*.*";

            genealogyWeightDropWatcher.Path = Configuration.GenealogyWeightDropDirectory;
            genealogyWeightDropWatcher.Filter = "*.*";


            // Add event handlers.
            arbinTestDropWatcher.Created += new FileSystemEventHandler(OnChanged);
            arbinTestDropWatcher.Renamed += new RenamedEventHandler(OnChanged);

            arbinTestProcessWatcher.Created += new FileSystemEventHandler(process);
            arbinTestProcessWatcher.Renamed += new RenamedEventHandler(process);

            firingcircuitsTestDropWatcher.Created += new FileSystemEventHandler(OnChanged_FiringCircuitsDrop);
            firingcircuitsTestDropWatcher.Renamed += new RenamedEventHandler(OnChanged_FiringCircuitsDrop);

            firingcircuitsTestProcessWatcher.Created += new FileSystemEventHandler(processFiringCircuits);
            firingcircuitsTestProcessWatcher.Renamed += new RenamedEventHandler(processFiringCircuits);

            pecTestDropWatcher.Created += new FileSystemEventHandler(OnChanged_PECDrop);
            pecTestDropWatcher.Renamed += new RenamedEventHandler(OnChanged_PECDrop);

            pecTestProcessWatcher.Created += new FileSystemEventHandler(processPEC);
            pecTestProcessWatcher.Renamed += new RenamedEventHandler(processPEC);

            genealogyThicknessProcessWatcher.Created += new FileSystemEventHandler(genealogyThicknessProcess);
            genealogyThicknessProcessWatcher.Renamed += new RenamedEventHandler(genealogyThicknessProcess);

            genealogyThicknessDropWatcher.Created += new FileSystemEventHandler(genealogyThicknessOnChanged);
            genealogyThicknessDropWatcher.Renamed += new RenamedEventHandler(genealogyThicknessOnChanged);

            genealogyWeightProcessWatcher.Created += new FileSystemEventHandler(genealogyWeightProcess);
            genealogyWeightProcessWatcher.Renamed += new RenamedEventHandler(genealogyWeightProcess);

            genealogyWeightDropWatcher.Created += new FileSystemEventHandler(genealogyWeightOnChanged);
            genealogyWeightDropWatcher.Renamed += new RenamedEventHandler(genealogyWeightOnChanged);


            // Begin watching.
            arbinTestDropWatcher.EnableRaisingEvents = true;
            arbinTestProcessWatcher.EnableRaisingEvents = true;

            firingcircuitsTestDropWatcher.EnableRaisingEvents = true;
            firingcircuitsTestProcessWatcher.EnableRaisingEvents = true;

            pecTestDropWatcher.EnableRaisingEvents = true;
            pecTestProcessWatcher.EnableRaisingEvents = true;
            
            genealogyThicknessDropWatcher.EnableRaisingEvents = true;
            genealogyThicknessProcessWatcher.EnableRaisingEvents = true;

            genealogyWeightDropWatcher.EnableRaisingEvents = true;
            genealogyWeightProcessWatcher.EnableRaisingEvents = true;

            }
            catch (Exception e)
            {

                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Message);
            }

        }

        protected override void OnStop()
        {
        }


        private static void genealogyWeightOnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                if (!validFileExtension(e.FullPath))
                {
                    return;
                }

                WaitReady(e.FullPath);
                copyFile(e.Name, Configuration.GenealogyWeightDropDirectory, Configuration.GenealogyWeightProcessingDirectory);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);

            }
        }

        private static void genealogyWeightProcess(object source, FileSystemEventArgs e)
        {
            try
            {
                WaitReady(e.FullPath);
                ElectrodeWeightGenealogyExcelDataSource dataSource = new ElectrodeWeightGenealogyExcelDataSource(e.FullPath);

                BielectrodeGenealogyRepository repository = new BielectrodeGenealogyRepository();

                IList<ElectrodeWeight> data = dataSource.getWeightData();

                repository.saveWeightData(data);

                WaitReady(e.FullPath);
                completeFile(e.Name, Configuration.GenealogyWeightProcessingDirectory, Configuration.GenealogyWeightCompletedDirectory);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
            }
        }


        private static void genealogyThicknessOnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                if (!validFileExtension(e.FullPath))
                {
                    return;
                }
                WaitReady(e.FullPath);
                copyFile(e.Name, Configuration.GenealogyThicknessDropDirectory, Configuration.GenealogyThicknessProcessingDirectory);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
            }
        }

        private static void genealogyThicknessProcess(object source, FileSystemEventArgs e)
        {
            try
            {
                WaitReady(e.FullPath);
                ElectrodeThicknessGenealogyExcelDataSource dataSource = new ElectrodeThicknessGenealogyExcelDataSource(e.FullPath);

                BielectrodeGenealogyRepository repository = new BielectrodeGenealogyRepository();

                IList<ElectrodeThickness> data = dataSource.getThicknessData();
                repository.saveThicknessData(data);


                WaitReady(e.FullPath);

                completeFile(e.Name, Configuration.GenealogyThicknessProcessingDirectory, Configuration.GenealogyThicknessCompletedDirectory);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
               
            }
        }


        private static void process(object source, FileSystemEventArgs e)
        {
            try
            {
                WaitReady(e.FullPath);
                ArbinExcelDataSource dataSource = new ArbinExcelDataSource(e.FullPath);

                ArbinTestDataRepository repository = new ArbinTestDataRepository();

                ArbinTest results = dataSource.getTestResults();

                repository.save(dataSource.getTestResults());

                WaitReady(e.FullPath);

                completeFile(e.Name, Configuration.ProcessingDirectory, Configuration.CompletedDirectory);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
               
            }
        }

        private static void processFiringCircuits(object source, FileSystemEventArgs e)
        {
            try
            {
                WaitReady(e.FullPath);
                FiringCircuitsCSVDataSource dataSource = new FiringCircuitsCSVDataSource(e.FullPath);

                FiringCircuitsTestDataRepository repository = new FiringCircuitsTestDataRepository();

                FiringCircuitsTest results = dataSource.getTestResults();

                repository.save(dataSource.getTestResults());

                WaitReady(e.FullPath);

                completeFile(e.Name, Configuration.FiringCircuitsProcessingDirectory, Configuration.FiringCircuitsCompletedDirectory);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
                
            }
        }

        private static void processPEC(object source, FileSystemEventArgs e)
        {
            try
            {
                WaitReady(e.FullPath);
                PECCSVDataSource dataSource = new PECCSVDataSource(e.FullPath);

                PECTestDataRepository repository = new PECTestDataRepository();

                PECTest results = dataSource.getTestResults();

                repository.save(dataSource.getTestResults());

                WaitReady(e.FullPath);

                completeFile(e.Name, Configuration.PECProcessingDirectory, Configuration.PECCompletedDirectory);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
               
            }
        }

        // Define the event handlers. 
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                if (!validFileExtension(e.FullPath))
                {
                    return;
                }

                WaitReady(e.FullPath);
                copyFile(e.Name, Configuration.DropDirectory, Configuration.ProcessingDirectory);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
                
            }
        }

        // Define the event handlers. 
        private static void OnChanged_FiringCircuitsDrop(object source, FileSystemEventArgs e)
        {
            try
            {
                if (!validCSVFileExtension(e.FullPath))
                {
                    return;
                }

                WaitReady(e.FullPath);
                copyFile(e.Name, Configuration.FiringCircuitsDropDirectory, Configuration.FiringCircuitsProcessingDirectory);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
                
            }
        }

        // Define the event handlers. 
        private static void OnChanged_PECDrop(object source, FileSystemEventArgs e)
        {
            try
            {
                if (!validCSVFileExtension(e.FullPath))
                {
                    return;
                }

                WaitReady(e.FullPath);
                copyFile(e.Name, Configuration.PECDropDirectory, Configuration.PECProcessingDirectory);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EPSDataUploadService.SERVICE_NAME, e.Name + ":" + ex.Message);
                
            }
        }


        private static Boolean validFileExtension(string fullPath)
        {
            // get the file's extension 
            string strFileExt = System.IO.Path.GetExtension(fullPath);

            // filter file types 
            if (!Regex.IsMatch(strFileExt, @"\.xls|\.xlsx", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }

        private static Boolean validCSVFileExtension(string fullPath)
        {
            // get the file's extension 
            string strFileExt = System.IO.Path.GetExtension(fullPath);

            // filter file types 
            if (!Regex.IsMatch(strFileExt, @"\.csv", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static void WaitReady(string fileName)
        {
            while (true)
            {
                try
                {
                    using (Stream stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        if (stream != null)
                        {
                            break;
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                }
                catch (IOException ex)
                {
                }
                catch (UnauthorizedAccessException ex)
                {
                }
                System.Threading.Thread.Sleep(500);
            }
        }

        private static void completeFile(string fileName, string fromDirectory, string toDirectory)
        {
            File.Copy(Path.Combine(fromDirectory, fileName), Path.Combine(toDirectory, fileName), true);
            File.Delete(Path.Combine(fromDirectory, fileName));
        }

        private static void copyFile(string fileName, string fromDirectory, string toDirectory)
        {
            File.Copy(Path.Combine(fromDirectory, fileName), Path.Combine(toDirectory, fileName), true);
            File.Delete(Path.Combine(fromDirectory, fileName));
        }

        private void dataUploadServiceInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }

        private void dataUploadServiceProcessInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }

    }
}
