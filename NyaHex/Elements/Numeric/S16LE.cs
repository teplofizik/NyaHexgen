using NyaHexgen.Elements.Base;
using NyaHexgen.Parser;
using NyaIO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Elements.Numeric
{

    public class S16LE : Element
    {
        Int16 Value = 0;

        public S16LE() : base("s16")
        {

        }

        /// <summary>
        /// Перезаписать
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override bool Override(string Value)
        {
            long Res = 0;
            if (ParseSigned(Value, out Res, Int16.MinValue, Int16.MaxValue))
            {
                this.Value = Convert.ToInt16(Res);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Размер поля
        /// </summary>
        public override uint Size { get; } = 2;

        /// <summary>
        /// Запись значения поля в данные
        /// </summary>
        /// <param name="data"></param>
        public override void Write(ref byte[] data, long offset)
        {
            data.WriteUInt16(offset, (ushort)Value);
        }
    }

    public class S16LEFactory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public S16LEFactory() : base("s16be")
        {

        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override Element? Get(ParserElement Element, Compiler.Environment Env)
        {
            var Res = new S16LE();

            if (Element.HasParam("default"))
                Res.Override(Env.Preprocess(Element.GetStringValue("default", "0")));

            return Res;
        }
    }
}
