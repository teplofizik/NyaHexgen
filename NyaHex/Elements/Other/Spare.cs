using NyaHexgen.Elements.Base;
using NyaHexgen.Elements.Net;
using NyaHexgen.Parser;
using NyaIO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Elements.Other
{
    public class Spare : Element
    {
        byte[] InternalData;

        public Spare(int Length) : base($"P{Length}")
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
            ulong Res = 0;
            if (ParseUnsigned(Value, out Res, Byte.MinValue, Byte.MaxValue))
            {
                var V = Convert.ToByte(Res);
                InternalData.Fill(V);
                return true;
            }
            else
                return false;
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

    public class SpareFactory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public SpareFactory() : base("P")
        {

        }

        public override bool IsMyName(string Name)
        {
            if(Name.StartsWith('P'))
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
                var Res = new Spare(Length);

                if (Element.HasParam("default"))
                    Res.Override(Env.Preprocess(Element.GetStringValue("default", "0")));

                return Res;
            }

            throw new InvalidOperationException("Invalid element name");
        }
    }
}
