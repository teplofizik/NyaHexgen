using NyaHexgen.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Elements.Base
{
    public abstract class ElementFactory
    {
        /// <summary>
        /// Имя поля
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Список псевдонимов
        /// </summary>
        private List<string> NameList = new List<string>();

        public ElementFactory(string Name)
        {
            this.Name = Name;

            NameList.Add(Name);
        }

        /// <summary>
        /// Добавить имя дополнительное
        /// </summary>
        /// <param name="Name"></param>
        protected void AddName(string Name)
        {
            NameList.Add(Name);
        }

        /// <summary>
        /// Это ли название блока
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public virtual bool IsMyName(string Name)
        {
            return NameList.Contains(Name);
        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public abstract Element? Get(ParserElement Element, Compiler.Environment Env);
    }
}
