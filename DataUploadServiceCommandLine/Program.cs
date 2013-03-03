using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataUploadApi;


namespace DataUploadService
{
    class Program
    {        
        static void Main(string[] args)
        {
            Run();
        }


        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run()
        {
            string[] args = System.Environment.GetCommandLineArgs();

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

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }


        private static void genealogyWeightOnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                if (!validFileExtension(e.FullPath))
                {
                    return;
                }

                Console.WriteLine("Begin genealogyWeightOnChanged File: " + e.FullPath + " " + e.ChangeType);
                WaitReady(e.FullPath);
                copyFile(e.Name, Configuration.GenealogyWeightDropDirectory, Configuration.GenealogyWeightProcessingDirectory);
                Console.WriteLine("End genealogyWeightOnChanged File: " + e.FullPath + " " + e.ChangeType);
            }
            catch (Exception ex)
            {
                Console.WriteLine("genealogyWeightOnChanged Error: {0}", ex.Message);
            }
        }

        private static void genealogyWeightProcess(object source, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine("genealogyWeightProcess Processing Step 1a: " + e.FullPath + " " + e.ChangeType);
                WaitReady(e.FullPath);
                Console.WriteLine("genealogyWeightProcess Processing Step 1b: " + e.FullPath + " " + e.ChangeType);
                ElectrodeWeightGenealogyExcelDataSource dataSource = new ElectrodeWeightGenealogyExcelDataSource(e.FullPath);
                Console.WriteLine("genealogyWeightProcess Processing Step 2: " + e.FullPath + " " + e.ChangeType);

                BielectrodeGenealogyRepository repository = new BielectrodeGenealogyRepository();
                Console.WriteLine("genealogyWeightProcess Processing Step 3: " + e.FullPath + " " + e.ChangeType);

                IList<ElectrodeWeight> data = dataSource.getWeightData();

                Console.WriteLine("genealogyWeightProcess Processing Step 4: " + e.FullPath + " " + e.ChangeType);

                repository.saveWeightData(data);

                Console.WriteLine("genealogyWeightProcess Processing Step 5: " + e.FullPath + " " + e.ChangeType);

                WaitReady(e.FullPath);
                Console.WriteLine("genealogyWeightProcess Processing Step 6: " + e.FullPath + " " + e.ChangeType);

                completeFile(e.Name, Configuration.GenealogyWeightProcessingDirectory, Configuration.GenealogyWeightCompletedDirectory);

            }
            catch (Exception ex)
            {
                Console.WriteLine("genealogyThicknessProcess Error: {0}", ex.Message);
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

                Console.WriteLine("Begin genealogyThicknessOnChanged File: " + e.FullPath + " " + e.ChangeType);
                WaitReady(e.FullPath);
                copyFile(e.Name, Configuration.GenealogyThicknessDropDirectory, Configuration.GenealogyThicknessProcessingDirectory);
                Console.WriteLine("End genealogyThicknessOnChanged File: " + e.FullPath + " " + e.ChangeType);
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
                Console.WriteLine("genealogyThicknessProcess Processing Step 1a: " + e.FullPath + " " + e.ChangeType);
                WaitReady(e.FullPath);
                Console.WriteLine("genealogyThicknessProcess Processing Step 1b: " + e.FullPath + " " + e.ChangeType);
                ElectrodeThicknessGenealogyExcelDataSource dataSource = new ElectrodeThicknessGenealogyExcelDataSource(e.FullPath);
                Console.WriteLine("genealogyThicknessProcess Processing Step 2: " + e.FullPath + " " + e.ChangeType);

                BielectrodeGenealogyRepository repository = new BielectrodeGenealogyRepository();
                Console.WriteLine("genealogyThicknessProcess Processing Step 3: " + e.FullPath + " " + e.ChangeType);

                IList<ElectrodeThickness> data = dataSource.getThicknessData();

                Console.WriteLine("genealogyThicknessProcess Processing Step 4: " + e.FullPath + " " + e.ChangeType);

                repository.saveThicknessData(data);

                Console.WriteLine("genealogyThicknessProcess Processing Step 5: " + e.FullPath + " " + e.ChangeType);

                WaitReady(e.FullPath);
                Console.WriteLine("genealogyThicknessProcess Processing Step 6: " + e.FullPath + " " + e.ChangeType);

                completeFile(e.Name, Configuration.GenealogyThicknessProcessingDirectory, Configuration.GenealogyThicknessCompletedDirectory);

            }
            catch (Exception ex)
            {
                Console.WriteLine("genealogyThicknessProcess Error: {0}", ex.Message);
            }
        }


        private static void process(object source, FileSystemEventArgs e)        
        {
            try
            {
                Console.WriteLine("Processing Step 1a: " + e.FullPath + " " + e.ChangeType);
                WaitReady(e.FullPath);
                Console.WriteLine("Processing Step 1b: " + e.FullPath + " " + e.ChangeType);
                ArbinExcelDataSource dataSource = new ArbinExcelDataSource(e.FullPath);
                Console.WriteLine("Processing Step 2: " + e.FullPath + " " + e.ChangeType);

                ArbinTestDataRepository repository = new ArbinTestDataRepository();
                Console.WriteLine("Processing Step 3: " + e.FullPath + " " + e.ChangeType);

                ArbinTest results = dataSource.getTestResults();
                Console.WriteLine("Processing Step 4: " + e.FullPath + " " + e.ChangeType);

                repository.save(dataSource.getTestResults());
                Console.WriteLine("Processing Step 5: " + e.FullPath + " " + e.ChangeType);

                WaitReady(e.FullPath);
                Console.WriteLine("Processing Step 6: " + e.FullPath + " " + e.ChangeType);

                completeFile(e.Name, Configuration.ProcessingDirectory, Configuration.CompletedDirectory);

            }
            catch (Exception ex)
            {
                Console.WriteLine("process Error: {0}", ex.Message);
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

                Console.WriteLine("Begin onChanged File: " + e.FullPath + " " + e.ChangeType);
                WaitReady(e.FullPath);
                copyFile(e.Name, Configuration.DropDirectory, Configuration.ProcessingDirectory);
                Console.WriteLine("End onChanged File: " + e.FullPath + " " + e.ChangeType);
            }
            catch (Exception ex)
            {
                Console.WriteLine("OnChanged Error: {0}", ex.Message);
            }
        }


        private static Boolean validFileExtension(string fullPath)
        {
            // get the file's extension 
            string strFileExt = System.IO.Path.GetExtension(fullPath);

            // filter file types 
            if (!Regex.IsMatch(strFileExt, @"\.xls|\.xlsx", RegexOptions.IgnoreCase))
            {
                Console.WriteLine("wrong file type: {0}", strFileExt);
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
                            Console.WriteLine(string.Format("Output file {0} ready.", fileName));
                            break;
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(string.Format("Output file {0} not yet ready ({1})", fileName, ex.Message));
                }
                catch (IOException ex)
                {
                    Console.WriteLine(string.Format("Output file {0} not yet ready ({1})", fileName, ex.Message));
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(string.Format("Output file {0} not yet ready ({1})", fileName, ex.Message));
                }
                System.Threading.Thread.Sleep(500);
            }
        }

        private static void completeFile(string fileName, string fromDirectory, string toDirectory)
        {
            Console.WriteLine("File: {0} copied", fileName);
            File.Copy(fromDirectory + "\\" + fileName, toDirectory + "\\" + fileName, true);
            File.Delete(fromDirectory + "\\" + fileName);
        }

        private static void copyFile(string fileName, string fromDirectory, string toDirectory)
        {
            Console.WriteLine("File: {0} copied", fileName);
            File.Copy(fromDirectory + "\\" + fileName, toDirectory + "\\" + fileName, true);
            File.Delete(fromDirectory + "\\" + fileName);
        }

    }
}
