using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public static class DataConversor
    {

        public static string[] DateSeparator(string stringValue)
        {
            if (stringValue != null)
            {
                var strings = stringValue.Split('/');
                return strings;
            }
            else
            {
                return null;
            }

        }

        public static string ConvertStringToDateFormat(string stringValue)
        {
            try
            {
                string newString = "";
                var stringArray = DateSeparator(stringValue);
                if (stringArray.Length > 0 && stringArray.Length<=3)
                {
                    newString = stringArray[1] + "/" + stringArray[0] + "/" + stringArray[2];
                }
                return newString;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static int ConvertStringToInt(string stringValue)
        {
            try
            {
                var value = int.Parse(stringValue);
                return value;
            }
            catch (Exception)
            {

                return -1;
            }
        }

    }
}