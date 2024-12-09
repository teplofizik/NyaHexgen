using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Compiler
{
    public class Loader
    {
        static Elements.Base.ElementFactory[] Factories = new Elements.Base.ElementFactory[]
        {
            new Elements.Other.StringFactory(),
            new Elements.Other.SpareFactory(),

            new Elements.Numeric.U8Factory(),
            new Elements.Numeric.U16LEFactory(),
            new Elements.Numeric.U16BEFactory(),
            new Elements.Numeric.U32LEFactory(),
            new Elements.Numeric.U32BEFactory(),
            new Elements.Numeric.S8Factory(),
            new Elements.Numeric.S16LEFactory(),
            new Elements.Numeric.S16BEFactory(),
            new Elements.Numeric.S32LEFactory(),
            new Elements.Numeric.S32BEFactory(),

            new Elements.Net.MACFactory(),
            new Elements.Net.IP4Factory(),

            new Elements.Checksum.CRC32Factory(),
            new Elements.Checksum.CRC32BEFactory()
        };

        static public Memory? LoadXml(string Filename)
        {
            var Parser = new Parser.Xml.XmlParser(Filename);
            Parser.AddFactories(Factories);
            Parser.Process();

            return Parser.GetResult();
        }
    }
}
