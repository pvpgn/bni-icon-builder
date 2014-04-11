using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BniIconBuilder
{
    static class Helper
    {
        public static string ProgramName = "BNI Icon Builder";
        public const int MaxWidth = 28;
        public const int MaxHeight = 14;

        
        /// <summary>
        /// Remove null bytes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] RemoveNullBytes(byte[] data, bool addOneNullByteAtTheEnd = false)
        {
            // get position without null bytes
            int pos = 0;
            for (int i = data.Length - 1; i >= 0; i--)
                if (data[i] != 0)
                {
                    pos = i;
                    break;
                }

            pos++;

            // add one null byte at the end
            if (addOneNullByteAtTheEnd)
                pos ++;

            // copy bytes without null bytes
            var newData = new byte[pos];
            Array.Copy(data, newData, pos);

            return newData;
        }

        /// <summary>
        /// Convert text hex (like 00000800) to int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns>if convert fails return false</returns>
        public static bool ConvertToInt32(string str, out int value)
        {
            value = 0;
            try
            {
                value = Convert.ToInt32(str, 16);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Convert text hex (like 00000800) to int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns>if convert fails return false</returns>
        public static int ConvertToInt32(string str)
        {
            int value;
            ConvertToInt32(str, out value);
            return value;
        }

    }
}
