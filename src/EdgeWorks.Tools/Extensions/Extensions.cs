using Colorful;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Console = Colorful.Console;


namespace EdgeWorks.Tools.Extensions
{
    public static class Extensions
    {
        public static Color ToColor(this string hexValue)
        {
            if (!hexValue.StartsWith('#'))hexValue = hexValue.Insert(0, "#");
            try { return ColorTranslator.FromHtml(hexValue); }
            catch (Exception) { return Color.White; }
        }

        public static string GetTypeName(this ParameterInfo param)
        {
            var typeName = param.ParameterType.Name;
            TypeDictionary.Aliases.TryGetValue(param.ParameterType, out typeName);
            return typeName;
        }

        public static void PrintException(this Exception ex)
        {
            Console.WriteLine("Error occured while execution:");
            Console.WriteLine(ex.Message);

            foreach (var line in ex.StackTrace.Split("\r\n"))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
        }
    }
}
