using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Argument
{
    class Argument
    {
        /// <summary>
        /// Имя аргумента
        /// </summary>
        public string Name;

        /// <summary>
        /// Значение аргумента
        /// </summary>
        public string Value;

        /// <summary>
        /// Список данных
        /// </summary>
        public List<Argument> Arguments = new List<Argument>();
    }
}
