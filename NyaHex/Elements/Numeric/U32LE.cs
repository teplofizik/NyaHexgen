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
    public class U32LE : Element
    {
        UInt32 Value = 0;

        public U32LE() : base("u32")
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
            if (ParseUnsigned(Value, out Res, UInt32.MinValue, UInt32.MaxValue))
            {
                this.Value = Convert.ToUInt32(Res);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Размер поля
        /// </summary>
        public override uint Size { get; } = 4;

        /// <summary>
        /// Запись значения поля в данные
        /// </summary>
        /// <param name="data"></param>
        public override void Write(ref byte[] data, long offset)
        {
            data.WriteUInt32(offset, Value);
        }
    }

    public class U32LEFactory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public U32LEFactory() : base("u32")
        {
            AddName("u32le");
        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override Element? Get(ParserElement Element, Compiler.Environment Env)
        {
            var Res = new U32LE();

            if (Element.HasParam("default"))
                Res.Override(Env.Preprocess(Element.GetStringValue("default", "0")));

            return Res;
        }
    }
}
