using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Parser.Xml
{
    public class XmlParser : Parser
    {
        string Filename;

        public XmlParser(string Filename)
        {
            this.Filename = Filename;
        }

        /// <summary>
        /// Обработать
        /// </summary>
        public override void ProcessFile()
        {
            NyaHexgen.Xml.XmlLoad X = new NyaHexgen.Xml.XmlLoad();
            if (!X.Load(Filename)) return;

            while (X.Read())
            {
                var Element = new XmlParserElement(X);

                ProcessElement(Element);
            }
            X.Close();
        }
    }
}
