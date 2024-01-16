using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LapTopStore_Common
{
    public static class Securiry
    {
        public static string MD5Hash(string text)
        {
            MD5 md5H = MD5.Create();
            //convert the input string to a byte array and compute its hash
            byte[] data = md5H.ComputeHash(Encoding.UTF8.GetBytes(text));
            // create a new stringbuilder to collect the bytes and create a string
            StringBuilder sB = new StringBuilder();
            //loop through each byte of hashed data and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sB.Append(data[i].ToString("x2"));
            }
            //return hexadecimal string
            return sB.ToString();
        }

        private static bool IsNumeric(object value)
        {
            return value is sbyte
                       || value is byte
                       || value is short
                       || value is ushort
                       || value is int
                       || value is uint
                       || value is long
                       || value is ulong
                       || value is float
                       || value is double
                       || value is decimal;
        }

        public static string FormatNumber(object value, int SoSauDauPhay = 2)
        {
            bool isNumber = IsNumeric(value);
            decimal GT = 0;
            if (isNumber)
            {
                GT = Convert.ToDecimal(value);
            }
            string str = "";
            string thapPhan = "";
            for (int i = 0; i < SoSauDauPhay; i++)
            {
                thapPhan += "#";
            }
            if (thapPhan.Length > 0) thapPhan = "." + thapPhan;
            string snumformat = string.Format("0:#,##0{0}", thapPhan);
            str = String.Format("{" + snumformat + "}", GT);

            return str;
        }
    }
}
