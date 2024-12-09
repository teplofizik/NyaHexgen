using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Parser
{
    public abstract class ParserElement
    {
        public ParserElement() { 
        
        }

        /// <summary>
        /// Название блока
        /// </summary>
        /// <returns></returns>
        public abstract string GetName();

        /// <summary>
        /// Есть ли параметры
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public abstract bool HasParam(string Name);

        /// <summary>
        /// Получить строковое значение
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public abstract string GetStringValue(string Name, string Default);
    }
}
