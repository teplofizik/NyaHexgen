using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Compiler
{
    public class Environment
    {
        /// <summary>
        /// Словарь замен
        /// </summary>
        public Dictionary<string, string> Values = new Dictionary<string, string>();

        public Environment()
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                Values.Add("user", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            }

            Values.Add("day", $"{DateTime.Now.Day}");
            Values.Add("month", $"{DateTime.Now.Month}");
            Values.Add("year", $"{DateTime.Now.Year}");
            Values.Add("yearshort", $"{DateTime.Now.Year % 100}");
        }

        /// <summary>
        /// Предобработать данные
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public string Preprocess(string Text)
        {
            foreach (var Key in Values.Keys)
            {
                Text = Text.Replace($"{{{Key}}}", Values[Key]);
            }

            return Text;
        }
    }
}
