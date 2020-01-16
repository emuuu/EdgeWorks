using Colorful;
using EdgeWorks.Tools.Extensions;
using EdgeWorks.Tools.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace EdgeWorks.Tools.App
{
    public class Application
    {
        public readonly AnalyseService _analyseService;
        public readonly DataProcessService _dataProcessService;
        private List<KeyValuePair<int, MethodInfo>> _methods;

        public Application(AnalyseService analyseService, DataProcessService dataProcessService)
        {
            _analyseService = analyseService;
            _dataProcessService = dataProcessService;
            _methods = new List<KeyValuePair<int, MethodInfo>>();
        }

        public void Run()
        {
            InitMethodlist();
            PrintMethodList();
            Console.WriteLine("");
            do
            {
                Console.WriteLine("Select a method:", "00CC66".ToColor());
                var input = ReadLineOrEsc();
                if (input == null)
                {
                    Environment.Exit(0);
                }
                int methodID;
                if (!int.TryParse(input, out methodID))
                {
                    Console.WriteLine("Invalid input", "FF0000".ToColor());
                    Console.WriteLine("");
                }
                else
                {
                    if (!_methods.Any(x => x.Key == methodID))
                    {
                        Console.WriteLine("MethodID not found");
                    }
                    else
                    {
                        var method = _methods.First(x => x.Key == methodID).Value;
                        Console.WriteLine("");
                        ExecuteMethod(method);
                        Console.WriteLine("");
                    }
                }
            }
            while (true);
        }

        private void InitMethodlist()
        {
            var i = 1;
            GetTypeMethods(i, this, out i);
            foreach (var field in this.GetType().GetFields())
            {
                GetTypeMethods(i, field.GetValue(this), out i);
            }
        }

        private void GetTypeMethods(int currentNumber, Object input, out int finishNumber)
        {
            var type = input.GetType();
            var methods = type.GetMethods().Where(x => x.DeclaringType == type && x.Name != "Run");

            foreach (MethodInfo method in methods)
            {
                {
                    _methods.Add(new KeyValuePair<int, MethodInfo>(currentNumber, method));
                    currentNumber++;
                }
            }
            finishNumber = currentNumber;
        }

        public void PrintMethodList()
        {
            Console.WriteLine("Available methods:", "00CC66".ToColor());
            foreach (var declaringType in _methods.Select(x => x.Value.DeclaringType).Distinct())
            {
                Console.WriteLine("{0}:", declaringType.Name, "33FF99".ToColor());
                foreach (var method in _methods.Where(x => x.Value.DeclaringType == declaringType))
                {
                    Console.WriteFormatted("{0}", Color.White, TypeDictionary.FormatInput(method.Key));
                    Console.WriteLineFormatted(" - {0}", Color.White, TypeDictionary.FormatInput(GetMethodString(method.Value)));
                }
                Console.WriteLine("");
            }
            Console.WriteLineFormatted("Press {0} to exit", Color.White, new Formatter("ESC", "CC0066".ToColor()));
        }

        private void ExecuteMethod(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var values = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var foundParam = false;
                Console.WriteFormatted("Insert the {0} variable", Color.White, TypeDictionary.FormatInput(parameters[i].GetTypeName()));
                Console.WriteFormatted(" '{0}':", Color.White, TypeDictionary.FormatInput(parameters[i].Name));
                while (!foundParam)
                {
                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        values[i] = parameters[i].ParameterType.IsValueType ? Activator.CreateInstance(parameters[i].ParameterType) : null;
                        foundParam = true;
                    }
                    else if (parameters[i].GetType() == typeof(bool) && (input == "0" || input == "1"))
                    {
                        values[i] = input == "1";
                        foundParam = true;
                    }
                    else if (parameters[i].GetType() == typeof(string))
                    {
                        values[i] = input;
                        foundParam = true;
                    }
                    else
                    {
                        try
                        {
                            values[i] = Convert.ChangeType(input, parameters[i].ParameterType);
                            foundParam = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Input is invalid for {0}", parameters[i].GetTypeName());
                        }
                    }
                }
            }

            object target = this;
            foreach (var field in this.GetType().GetFields())
            {
                if (field.FieldType == method.DeclaringType)
                {
                    target = field.GetValue(this);
                }
            }
            if (method.ReturnType == typeof(Task))
            {
                ((Task)method.Invoke(target, values)).Wait();
            }
            else
            {
                method.Invoke(target, values);
            }

            Console.WriteLine("");
        }


        private string GetMethodString(MethodInfo method)
        {
            var text = method.Name;
            var parameters = method.GetParameters();
            text += "(";

            foreach (var param in parameters)
            {
                text += param.GetTypeName() + " " + param.Name + ", ";
            }
            if (parameters.Length > 0)
            {
                text = text.Remove(text.Length - 2);
            }
            text += ")";
            return text;
        }

        private static string ReadLineOrEsc()
        {
            string retString = "";

            int curIndex = 0;
            do
            {
                ConsoleKeyInfo readKeyResult = Console.ReadKey(true);

                // handle Esc
                if (readKeyResult.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    return null;
                }

                // handle Enter
                if (readKeyResult.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return retString;
                }

                // handle backspace
                if (readKeyResult.Key == ConsoleKey.Backspace)
                {
                    if (curIndex > 0)
                    {
                        retString = retString.Remove(retString.Length - 1);
                        Console.Write(readKeyResult.KeyChar);
                        Console.Write(' ');
                        Console.Write(readKeyResult.KeyChar);
                        curIndex--;
                    }
                }
                else
                // handle all other keypresses
                {
                    retString += readKeyResult.KeyChar;
                    Console.Write(readKeyResult.KeyChar);
                    curIndex++;
                }
            }
            while (true);
        }
    }
}