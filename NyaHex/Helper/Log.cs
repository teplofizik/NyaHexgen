using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Helper
{
    public static class Log
    {
        public static void Error(string Message)
        {
            Console.Error.WriteLine(Message);
        }
    }
}
