using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadService
{
    class Configuration
    {
        public static String QADataConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["qadataDb"].ConnectionString;
            }
        }

        public static String DropDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DropDirectory"];
            }
        }

        public static String PendingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PendingDirectory"];
            }
        }

        public static String CompletedDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CompletedDirectory"];
            }
        }

        public static String ProcessingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProcessingDirectory"];
            }
        }



        public static String GenealogyThicknessDropDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GenealogyThicknessDropDirectory"];
            }
        }

        public static String GenealogyThicknessCompletedDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GenealogyThicknessCompletedDirectory"];
            }
        }

        public static String GenealogyThicknessProcessingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GenealogyThicknessProcessingDirectory"];
            }
        }


        public static String GenealogyWeightDropDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GenealogyWeightDropDirectory"];
            }
        }

        public static String GenealogyWeightCompletedDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GenealogyWeightCompletedDirectory"];
            }
        }

        public static String GenealogyWeightProcessingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GenealogyWeightProcessingDirectory"];
            }
        }


    }
}
