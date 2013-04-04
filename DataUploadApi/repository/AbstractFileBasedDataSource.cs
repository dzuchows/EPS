using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi
{
    public abstract class AbstractFileBasedDataSource<T, R> : FileBasedDataSource<T, R>
    {
        public T getTestResults()
        {
            throw new InvalidOperationException("Must override");
        }

        
        protected string returnFirstNonEmptyLine(StreamReader reader)
        {
            string s;
            while ((s = reader.ReadLine()).Length == 0);
            return s;
        }

        protected int extractIntValue(String line)
        {
            string[] s;
            s = line.Split(',');
            return getIntValue(s[1]);
        }

        protected string extractStringValue(String line)
        {
            string[] s;
            s = line.Split(',');
            return s[1];
        }

        protected DateTime? extractDateTimeValue(String line)
        {
            string[] s;
            s = line.Split(',');
            return getDateTimeValue(s[1]);
        }

        protected float extractFloatValue(String line)
        {
            string[] s;
            s = line.Split(',');
            return getFloatValue(s[1]);
        }

        protected int getIntValue(String value)
        {
            if (!String.IsNullOrEmpty(value)) return Convert.ToInt32(value);
            return 0;
        }

        protected DateTime? getDateTimeValue(String value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        protected float getFloatValue(string s)
        {
            float value;
            if (float.TryParse(s, out value))
            {
                return value;
            }
            else
            {
                return 0.0F;
            }
        }

    }
}
