using NyaHexgen.Elements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaHexgen.Compiler
{
    public class Memory
    {
        /// <summary>
        /// Окружение
        /// </summary>
        public Environment Environment { get; } = new Environment();

        /// <summary>
        /// Предварительный список элементов на обработку
        /// </summary>
        public Element[] Elements = new Element[] { };

        /// <summary>
        /// Список приоритетов
        /// </summary>
        public int[] Priorities = new int[] { };

        /// <summary>
        /// Данные
        /// </summary>
        public byte[] Data = new byte[] { };

        public void ProcessElements(Element[] Elements)
        {
            var Priorities = new List<int>();
            long Size = 0;

            foreach (var E in Elements)
            {
                E.Address = Size;
                Size += E.Size;

                if(!Priorities.Contains(E.Priority))
                    Priorities.Add(E.Priority);
            }

            this.Elements = Elements;
            this.Data = new byte[Size];
            this.Priorities = Priorities.ToArray();
        }

        private void FillByPriority(int Priority)
        {
            foreach (var E in Elements)
            {
                if(E.Priority == Priority)
                {
                    E.Write(ref Data, E.Address);
                }
            }
        }

        /// <summary>
        /// Заполнить память значениями...
        /// </summary>
        public void FillMemory()
        {
            foreach(var Priority in Priorities)
            {
                FillByPriority(Priority);
            }
        }

    }
}
