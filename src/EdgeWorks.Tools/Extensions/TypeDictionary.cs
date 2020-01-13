using Colorful;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EdgeWorks.Tools.Extensions
{
    public static class TypeDictionary
    {
        public static readonly Dictionary<Type, string> Aliases =
            new Dictionary<Type, string>()
                {
                    { typeof(byte), "byte" },
                    { typeof(sbyte), "sbyte" },
                    { typeof(short), "short" },
                    { typeof(ushort), "ushort" },
                    { typeof(int), "int" },
                    { typeof(uint), "uint" },
                    { typeof(long), "long" },
                    { typeof(ulong), "ulong" },
                    { typeof(float), "float" },
                    { typeof(double), "double" },
                    { typeof(decimal), "decimal" },
                    { typeof(object), "object" },
                    { typeof(bool), "bool" },
                    { typeof(char), "char" },
                    { typeof(string), "string" },
                    { typeof(void), "void" }
                };

        public static readonly Dictionary<Type, Color> Colors =
            new Dictionary<Type, Color>()
            {
                        { typeof(byte), ColorTranslator.FromHtml("#FFFFFF") },
                        { typeof(sbyte), ColorTranslator.FromHtml("#FFFFFF") },
                        { typeof(short), ColorTranslator.FromHtml("#CC0066") },
                        { typeof(ushort), ColorTranslator.FromHtml("#CC0066") },
                        { typeof(int), ColorTranslator.FromHtml("#CC0066") },
                        { typeof(uint), ColorTranslator.FromHtml("#CC0066") },
                        { typeof(long), ColorTranslator.FromHtml("#CC0066") },
                        { typeof(ulong), ColorTranslator.FromHtml("#CC0066") },
                        { typeof(float), ColorTranslator.FromHtml("#FF3399") },
                        { typeof(double), ColorTranslator.FromHtml("#FF3399") },
                        { typeof(decimal), ColorTranslator.FromHtml("#FF3399") },
                        { typeof(object), ColorTranslator.FromHtml("#FFFFFF") },
                        { typeof(bool), ColorTranslator.FromHtml("#FFFF99") },
                        { typeof(char), ColorTranslator.FromHtml("#66FFFF") },
                        { typeof(string), ColorTranslator.FromHtml("#00CCCC") },
                        { typeof(void), ColorTranslator.FromHtml("#FFCCCC") }
            };

        public static Formatter FormatInput(dynamic input)
        {
            var color = Color.White;
            Colors.TryGetValue(input.GetType(), out color);
            return new Formatter(input, color);
        }
    }
}
