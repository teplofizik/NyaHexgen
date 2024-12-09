using NyaHexgen.Elements.Base;
using NyaHexgen.Elements.Net;
using NyaHexgen.Parser;
using NyaIO.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Elements.Other
{
    public class String : Element
    {
        byte[] InternalData;

        public String(int Length) : base($"S{Length}")
        {
            InternalData = new byte[Length];
        }

        /// <summary>
        /// Перезаписать
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override bool Override(string Value)
        {
            var Raw = UTF8Encoding.UTF8.GetBytes(Value);
            InternalData.WriteArray(0, Raw, InternalData.Length);

            return true;
        }

        /// <summary>
        /// Размер поля
        /// </summary>
        public override uint Size => Convert.ToUInt32(InternalData.Length);

        /// <summary>
        /// Запись значения поля в данные
        /// </summary>
        /// <param name="data"></param>
        public override void Write(ref byte[] data, long offset)
        {
            data.WriteArray(offset, InternalData, InternalData.Length);
        }
    }

    public class StringFactory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public StringFactory() : base("S")
        {

        }

        public override bool IsMyName(string Name)
        {
            if(Name.StartsWith('S'))
            {
                int Length = 0;
                var Part = Name.Substring(1);
                if(Int32.TryParse(Part, out Length))
                {
                    if((Length > 0) && (Length < 2048))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override Element? Get(ParserElement Element, Compiler.Environment Env)
        {
            int Length;
            var Name = Element.GetName();
            var Part = Name.Substring(1);
            if (Int32.TryParse(Part, out Length))
            {
                var Res = new String(Length);

                if (Element.HasParam("default"))
                    Res.Override(Env.Preprocess(Element.GetStringValue("default", "")));

                return Res;
            }

            throw new InvalidOperationException("Invalid element name");
        }
    }
}
