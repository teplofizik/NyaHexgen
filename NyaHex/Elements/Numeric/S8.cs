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
    public class S8 : Element
    {
        private SByte Value = 0;

        public S8() : base("s8")
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
            if (ParseSigned(Value, out Res, SByte.MinValue, SByte.MaxValue))
            {
                this.Value = Convert.ToSByte(Res);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Размер поля
        /// </summary>
        public override uint Size { get; } = 1;

        /// <summary>
        /// Запись значения поля в данные
        /// </summary>
        /// <param name="data"></param>
        public override void Write(ref byte[] data, long offset)
        {
            data.WriteByte(offset, (byte)Value);
        }
    }

    public class S8Factory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public S8Factory() : base("s8")
        {

        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override Element? Get(ParserElement Element, Compiler.Environment Env)
        {
            var Res = new S8();

            if (Element.HasParam("default"))
                Res.Override(Env.Preprocess(Element.GetStringValue("default", "0")));

            return Res;
        }
    }
}
