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
    public class U16BE : Element
    {
        UInt16 Value = 0;

        public U16BE() : base("u16be")
        {

        }

        /// <summary>
        /// Перезаписать
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override bool Override(string Value)
        {
            ulong Res = 0;
            if (ParseUnsigned(Value, out Res, UInt16.MinValue, UInt16.MaxValue))
            {
                this.Value = Convert.ToUInt16(Res);
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
            data.WriteUInt16BE(offset, Value);
        }
    }

    public class U16BEFactory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public U16BEFactory() : base("u16be")
        {

        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override Element? Get(ParserElement Element, Compiler.Environment Env)
        {
            var Res = new U16BE();

            if (Element.HasParam("default"))
                Res.Override(Env.Preprocess(Element.GetStringValue("default", "0")));

            return Res;
        }
    }
}
