using NyaHexgen.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Elements.Base
{
    /// <summary>
    /// Базовый элемент файла
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Тип элемента
        /// </summary>
        public readonly string Type;

        /// <summary>
        /// Имя элемента
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Описание элемента
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Приоритет генерации
        /// </summary>
        public int Priority { get; set; } = 0;

        /// <summary>
        /// Размер поля
        /// </summary>
        public virtual uint Size { get; } = 0;

        /// <summary>
        /// Адрес поля
        /// </summary>
        public long Address { get; set; } = 0;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Type"></param>
        protected Element(string Type)
        {
            this.Type = Type;
        }

        /// <summary>
        /// Запись значения поля в данные
        /// </summary>
        /// <param name="data"></param>
        public virtual void Write(ref byte[] data, long offset)
        {

        }

        public virtual string Dump()
        {
            var Temp = new byte[Size];
            Write(ref Temp, 0);

            return String.Join("", Array.ConvertAll(Temp, B => $"{B:X02}"));
        }

        /// <summary>
        /// Перезаписать
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public virtual bool Override(string Value)
        {
            return false;
        }

        /// <summary>
        /// Попытаться преобразовать знаковое число
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Res"></param>
        /// <returns></returns>
        protected bool ParseSigned(string Value, out long Res, long Min, long Max) => Data.ParseSigned(Value, out Res, Min, Max);

        /// <summary>
        /// Попытаться преобразовать знаковое число
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Res"></param>
        /// <returns></returns>
        protected bool ParseUnsigned(string Value, out ulong Res, ulong Min, ulong Max) => Data.ParseUnsigned(Value, out Res, Min, Max);
    }
}
