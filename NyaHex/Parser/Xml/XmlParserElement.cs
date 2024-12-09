using NyaHexgen.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Parser.Xml
{
    public class XmlParserElement : ParserElement
    {
        XmlLoad Loader;

        public XmlParserElement(XmlLoad L) 
        {
            Loader = L;
        }

        /// <summary>
        /// Название блока
        /// </summary>
        /// <returns></returns>
        public override string GetName()
        {
            return Loader.ElementName;
        }

        /// <summary>
        /// Есть ли параметры
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public override bool HasParam(string Name)
        {
            return Loader.HasAttribute(Name);
        }

        /// <summary>
        /// Получить строковое значение
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public override string GetStringValue(string Name, string Default)
        {
            var Res = Loader.GetAttribute(Name);

            return Res == null ? Default : Res;
        }
    }
}
