using NyaHexgen.Elements.Base;
using NyaHexgen.Elements.Numeric;
using NyaHexgen.Parser;
using NyaIO.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Elements.Net
{
    public class IP4 : Element
    {
        byte[] InternalData = new byte[4];

        public IP4() : base("IP4")
        {

        }

        /// <summary>
        /// Перезаписать
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override bool Override(string Value)
        {
            IPAddress val;
            if (IPAddress.TryParse(Value, out val) && (val.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
                var Temp = val.GetAddressBytes();

                if (Temp.Length == 4)
                {
                    for (int i = 0; i < 4; i++)
                        InternalData[i] = Temp[i];
                }
            }

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
            data.WriteArray(offset, InternalData, 4);
        }
    }

    public class IP4Factory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public IP4Factory() : base("IP4")
        {

        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override Element? Get(ParserElement Element, Compiler.Environment Env)
        {
            var Res = new IP4();

            if (Element.HasParam("default"))
                Res.Override(Env.Preprocess(Element.GetStringValue("default", "0.0.0.0")));

            return Res;
        }
    }
}
