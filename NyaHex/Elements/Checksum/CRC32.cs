﻿using CrcSharp;
using NyaHexgen.Elements.Base;
using NyaHexgen.Helper;
using NyaHexgen.Parser;
using NyaIO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Elements.Checksum
{
    public class CRC32 : Element
    {
        readonly long Offset;
        readonly long Count;

        public CRC32(ulong Offset, ulong Count) : base("CRC32")
        {
            this.Offset = Convert.ToInt64(Offset);
            this.Count = Convert.ToInt64(Count);
        }

        /// <summary>
        /// Перезаписать
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override bool Override(string Value)
        {
            // Algo?
            return true;
        }

        /// <summary>
        /// Размер поля
        /// </summary>
        public override uint Size { get; } = 4;

        /// <summary>
        /// Начало диапазона
        /// </summary>
        private long Start => Offset;

        /// <summary>
        /// Конец диапазона
        /// </summary>
        private long End => Offset + Count - 1;

        /// <summary>
        /// Запись значения поля в данные
        /// </summary>
        /// <param name="data"></param>
        public override void Write(ref byte[] data, long offset)
        {
            if(Start > data.Length)
            {
                Log.Error($"Cannot calc CRC32: start address is too large ({Start:X04} > size {data.Length:X04})");
                return;
            }
            if (End > data.Length)
            {
                Log.Error($"Cannot calc CRC32: end address is too large ({End:X04} > size {data.Length:X04})");
                return;
            }
            if((offset > Start - 4) && (offset < End))
            {
                Log.Error($"Cannot calc CRC32: crc32 field is in calculated range ({offset:X04} : range {Start:X04}-{End:X04})");
                return;
            }

            var CRC = CalcCrc(data.ReadArray(Offset, Count));

            data.WriteUInt32(offset, CRC);
        }

        private uint CalcCrc(byte[] Data)
        {
            var crc32 = new Crc(new CrcParameters(32, 0x04c11db7, 0xffffffff, 0xffffffff, true, true));

            return Convert.ToUInt32(crc32.CalculateAsNumeric(Data));
        }
        public override string Dump()
        {
            return $"CRC32 (from {Offset:X04} size {Count:X04})";
        }

    }

    public class CRC32Factory : ElementFactory
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public CRC32Factory() : base("CRC32")
        {
            AddName("CRC32LE");
        }

        /// <summary>
        /// Получить объект элемента
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public override Element? Get(ParserElement Element, Compiler.Environment Env)
        {
            string Address = Element.GetStringValue("address", "0");
            string Size    = Element.GetStringValue("size", "0");

            ulong UAddress, USize;
            if (Data.ParseUnsigned(Address, out UAddress, 0, 0x80000000u))
            {
                if (Data.ParseUnsigned(Size, out USize, 0, 0x80000000u))
                {
                    var Res = new CRC32(UAddress, USize);

                    return Res;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
