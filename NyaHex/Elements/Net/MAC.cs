using NyaHexgen.Elements.Base;
using NyaHexgen.Elements.Numeric;
using NyaHexgen.Parser;
using NyaIO.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Elements.Net
{
    public class MAC : Element
    {
        byte[] InternalData = new byte[6];

        public MAC() : base("MAC")
        {

        }

        /// <summary>
        /// Перезаписать
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override bool Override(string Value)
        {
            var Parts = Value.Split(new char[] { ':' });
            if (Parts.Length != 6) return false;

            int[] P = new int[6];
            for (int i = 0; i < 6; i++)
            {
                if (!Int32.TryParse(Parts[i], NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat, out P[i])) return false;
                if ((P[i] < 0) || (P[i] > 255)) return false;
            }

            for (int i = 0; i < 6; i++)
                InternalData[i] = Convert.ToByte(P[i]);

            return true;
        }

        /// <summary>
        /// Размер поля
        /// </summary>
        public override uint Size { get; } = 6;

        /// <summary>
        /// Запись значения поля в данные
        /// </summary>
        /// <param name="data"></param>
        public override void Write(ref byte[] data, long offset)
        {
            data.WriteArray(offset, InternalData, 6);
        }
    }
    public class MACFactory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public MACFactory() : base("MAC")
        {

        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override Element? Get(ParserElement Element, Compiler.Environment Env)
        {
            var Res = new MAC();

            if (Element.HasParam("default"))
                Res.Override(Env.Preprocess(Element.GetStringValue("default", "00:00:00:00:00:00")));

            return Res;
        }
    }
}
