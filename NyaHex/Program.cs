using NyaHexgen.Compiler;
using NyaHexgen.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace NyaHexgen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Intel HEX and binary file generator v1.1 [dev.]");

            if (args.Length == 0) 
                ShowHelp();
            else
                ParseArgs(args);
        }
        static void ShowHelp()
        {
            Console.WriteLine("Help:");
            Console.WriteLine("Intel HEX and binary file generator");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("-m<file> - modify data of xml file:");
            Console.WriteLine("  <field name>=<new value> - replace default value of field by specified value");
            Console.WriteLine("");
            Console.WriteLine("-d - dump structure");
            Console.WriteLine("");
            Console.WriteLine("-s<file> - save result to specified file:");
            Console.WriteLine("  format=[bin|hex] - file format (binary or Intel HEX)U;");
            Console.WriteLine("  offset=0xXXXXXXXX - data offset;");
        }

        static void ParseArgs(string[] args)
        {
            Argument.ArgumentParser Parser = new Argument.ArgumentParser(args);
            List<Argument.Argument> ArgList = Parser.Arguments;
            Memory? Mem = null;

            for (int i = 0; i < ArgList.Count; i++)
            {
                switch (ArgList[i].Name)
                {
                    case "m":
                        Mem = Modify(ArgList[i].Value, ArgList[i].Arguments);
                        break;
                    case "d":
                        if(Mem != null)
                            Dump(Mem);
                        else
                            Console.WriteLine("No data for dump");
                        break;
                    case "s":
                        if(Mem != null)
                            Save(ArgList[i].Value, ArgList[i].Arguments, Mem.Data);
                        else
                            Console.WriteLine("No data for saving");
                        break;
                }
            }
        }

        static int GetOffset(string Value)
        {
            try
            {
                if ("0x".CompareTo(Value.Substring(0, 2)) == 0)
                    return Convert.ToInt32(Value.Substring(2), 16);
                else
                    return Convert.ToInt32(Value);
            }
            catch (Exception E)
            {
                Console.WriteLine("Error: Invalid offset value (" + E.Message + ")");

                return 0;
            }
        }

        static void Dump(Memory Mem)
        {
            foreach(var E in Mem.Elements)
            {
                var Text = $"{E.Address:X04} {E.Type:6} ";
                if (E.Name.Length > 0)
                    Text += $"name={E.Name} ";

                Text += $"value={E.Dump()}";
                Console.WriteLine(Text);
            }
        }

        static void Save(string Argument, List<Argument.Argument> ArgList, byte[] Data)
        {
            string FileName = Argument;
            string FileType = "bin";
            string Offset = "0x00000000";

            if (Data == null) return;

            foreach (var A in ArgList)
            {
                string Value = A.Value;
                switch (A.Name)
                {
                    case "format": FileType = Value; break;
                    case "offset": Offset = Value.Trim(); break;
                }
            }

            switch (FileType)
            {
                case "hex":
                case "ihex":
                    Exporter.SaveHex(FileName, Data, GetOffset(Offset));
                    break;
                case "bin":
                default:
                    Exporter.SaveBin(FileName, Data, GetOffset(Offset));
                    break;
            }
        }

        static Memory? Modify(string Argument, List<Argument.Argument> ArgList)
        {
            var Mem = Loader.LoadXml(Argument);

            if (Mem != null)
            {
                foreach (var A in ArgList)
                {
                    foreach (var E in Mem.Elements)
                    {
                        if (E.Name.Length == 0) continue;
                        if (E.Name.CompareTo(A.Name) == 0) E.Override(A.Value);
                    }
                }

                Mem.FillMemory();

                return Mem;
            }

            return null;
        }

    }
}
