using NyaHexgen.Compiler;
using NyaHexgen.Elements.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Environment = NyaHexgen.Compiler.Environment;

namespace NyaHexgen.Parser
{
    public class Parser
    {
        /// <summary>
        /// Список фабрик поддерживаемых элементов
        /// </summary>
        private List<ElementFactory> Factories = new List<ElementFactory>();

        /// <summary>
        /// Список элементов
        /// </summary>
        private List<Element> Elements = new List<Element>();

        /// <summary>
        /// Блок данных
        /// </summary>
        private Memory Mem = new Memory();

        /// <summary>
        /// Есть ли ошибки в разборе
        /// </summary>
        private bool Error = false;

        public Parser()
        { 
            
        }

        /// <summary>
        /// Получить результат
        /// </summary>
        /// <returns></returns>
        public Memory GetResult()
        {
            if (Error)
                return null;
            else
                return Mem;
        }

        /// <summary>
        /// Начать обработку
        /// </summary>
        public void StartParse()
        {
            Elements.Clear();
            Error = false;
        }

        /// <summary>
        /// В случае ошибки сообщение
        /// </summary>
        /// <param name="Text"></param>
        protected void OnError(string Text)
        {
            Error = true;

            Debug.WriteLine(Text);
        }

        /// <summary>
        /// Обработать элемент...
        /// </summary>
        /// <param name="element"></param>
        protected void ProcessElement(ParserElement element)
        {
            var factory = GetFactory(element.GetName());

            if (factory != null)
            {
                var E = factory.Get(element, Mem.Environment);

                if (E != null)
                {
                    if (element.HasParam("name"))
                        E.Name = element.GetStringValue("name", "");
                    if (element.HasParam("description"))
                        E.Description = element.GetStringValue("description", "");

                    Elements.Add(E);
                }
                else
                {
                    OnError($"Cannot create element type {element.GetName()}");
                }
            }
            else
            {
                OnError($"Cannot detect element type {element.GetName()}");
            }
        }

        /// <summary>
        /// Обработать файл
        /// </summary>
        public virtual void ProcessFile()
        {

        }

        /// <summary>
        /// Обработать
        /// </summary>
        public void Process()
        {
            ProcessFile();
            if (!Error)
            {
                Mem.ProcessElements(Elements.ToArray());
                Elements.Clear();
            }
        }

        /// <summary>
        /// Добавить обрабатываемый тип поля
        /// </summary>
        /// <param name="Factory"></param>
        public void AddFactory(ElementFactory Factory)
        {
            Factories.Add(Factory);
        }

        /// <summary>
        /// Добавить обрабатываемый тип поля
        /// </summary>
        /// <param name="Factories"></param>
        public void AddFactories(ElementFactory[] Factories)
        {
            foreach (var Factory in Factories)
                this.Factories.Add(Factory);
        }

        /// <summary>
        /// Получить фабрику по названию элемента
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ElementFactory? GetFactory(string Name)
        {
            foreach (var Factory in Factories)
            {
                if(Factory.IsMyName(Name))
                    return Factory;
            }

            return null;
        }
    }
}
