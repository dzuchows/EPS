using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataUploadApi
{
    public class Configuration
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


        public static String FiringCircuitsDropDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FiringCircuitsDropDirectory"];
            }
        }

        public static String FiringCircuitsPendingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FiringCircuitsPendingDirectory"];
            }
        }

        public static String FiringCircuitsCompletedDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FiringCircuitsCompletedDirectory"];
            }
        }

        public static String FiringCircuitsProcessingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FiringCircuitsProcessingDirectory"];
            }
        }


        public static String PECDropDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PECDropDirectory"];
            }
        }

        public static String PECPendingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PECPendingDirectory"];
            }
        }

        public static String PECCompletedDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PECCompletedDirectory"];
            }
        }

        public static String PECProcessingDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PECProcessingDirectory"];
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
