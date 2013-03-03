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

namespace DataUploadService
{
    public partial class EPSDataUploadService : ServiceBase
    {
        public EPSDataUploadService()
        {
            InitializeComponent();
            EventLog.Log = "EPS Data Upload Service";
        }

        protected override void OnStart(string[] args)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            FileSystemWatcher dropWatcher = new FileSystemWatcher();
            FileSystemWatcher genealogyThicknessProcessWatcher = new FileSystemWatcher();
            FileSystemWatcher genealogyThicknessDropWatcher = new FileSystemWatcher();
            FileSystemWatcher genealogyWeightProcessWatcher = new FileSystemWatcher();
            FileSystemWatcher genealogyWeightDropWatcher = new FileSystemWatcher();


            watcher.Path = Configuration.DropDirectory;
            watcher.Filter = "*.*";

            dropWatcher.Path = Configuration.ProcessingDirectory;
            dropWatcher.Filter = "*.*";

            genealogyThicknessProcessWatcher.Path = Configuration.GenealogyThicknessProcessingDirectory;
            genealogyThicknessProcessWatcher.Filter = "*.*";

            genealogyThicknessDropWatcher.Path = Configuration.GenealogyThicknessDropDirectory;
            genealogyThicknessDropWatcher.Filter = "*.*";

            genealogyWeightProcessWatcher.Path = Configuration.GenealogyWeightProcessingDirectory;
            genealogyWeightProcessWatcher.Filter = "*.*";

            genealogyWeightDropWatcher.Path = Configuration.GenealogyWeightDropDirectory;
            genealogyWeightDropWatcher.Filter = "*.*";


            // Add event handlers.
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnChanged);
            dropWatcher.Created += new FileSystemEventHandler(process);
            dropWatcher.Renamed += new RenamedEventHandler(process);

            genealogyThicknessProcessWatcher.Created += new FileSystemEventHandler(genealogyThicknessProcess);
            genealogyThicknessProcessWatcher.Renamed += new RenamedEventHandler(genealogyThicknessProcess);

            genealogyThicknessDropWatcher.Created += new FileSystemEventHandler(genealogyThicknessOnChanged);
            genealogyThicknessDropWatcher.Renamed += new RenamedEventHandler(genealogyThicknessOnChanged);

            genealogyWeightProcessWatcher.Created += new FileSystemEventHandler(genealogyWeightProcess);
            genealogyWeightProcessWatcher.Renamed += new RenamedEventHandler(genealogyWeightProcess);

            genealogyWeightDropWatcher.Created += new FileSystemEventHandler(genealogyWeightOnChanged);
            genealogyWeightDropWatcher.Renamed += new RenamedEventHandler(genealogyWeightOnChanged);


            // Begin watching.
            watcher.EnableRaisingEvents = true;
            dropWatcher.EnableRaisingEvents = true;
            genealogyThicknessDropWatcher.EnableRaisingEvents = true;
            genealogyThicknessProcessWatcher.EnableRaisingEvents = true;
            genealogyWeightDropWatcher.EnableRaisingEvents = true;
            genealogyWeightProcessWatcher.EnableRaisingEvents = true;
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
                Console.WriteLine("genealogyThicknessOnChanged Error: {0}", ex.Message);
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
            File.Copy(fromDirectory + "\\" + fileName, toDirectory + "\\" + fileName, true);
            File.Delete(fromDirectory + "\\" + fileName);
        }

        private static void copyFile(string fileName, string fromDirectory, string toDirectory)
        {
            File.Copy(fromDirectory + "\\" + fileName, toDirectory + "\\" + fileName, true);
            File.Delete(fromDirectory + "\\" + fileName);
        }

        private void dataUploadServiceInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }

        private void dataUploadServiceProcessInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }

    }
}
