using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace TFYAiK
{
    public static class LexicalScanner
    {
        public struct LexicalItem
        {
            public Codes lexicalCode;
            public string item;
            public int startPosition;
            public int endPosition;

            public LexicalItem(Codes code, string item, int startPosition, int endPosition)
            {
                this.lexicalCode = code;
                this.item = item;
                this.startPosition = startPosition;
                this.endPosition = endPosition;
            }

            public override string ToString()
            {
                return $"{startPosition}:{endPosition}  {item}: {lexicalCode}: {Convert.ToInt16(lexicalCode)}";
            }
        }

        public enum Codes
        {
            ErrorCode = -1,
            WhileCode = 1,
            ForCode,
            IfCode,
            ElseCode,
            BreakCode,
            IdentifierCode,
            EqualCode,
            DoubleDotCode,
            SpaceConstCode,
            StringConstCode,
            IntegerConstCode,
            DoubleConstCode,
        }

        private static Codes IsNumber(string text)
        {
            if (text == "")
            {
                return Codes.ErrorCode;
            }

            int integerConst = 0;

            if (int.TryParse(text, out integerConst))
            {
                return Codes.IntegerConstCode;
            }
            // Проверяет первое число на то что это цифра. \ Checks if the first number is a digit
            if (Char.IsDigit(text[0]))
            {
                int i = 0;
                int countDot = 0;

                while (i < text.Length)
                {
                    if (text[i] == '.')
                    {
                        countDot++;         // Точка должна быть только одна \ There must be only one point
                    }
                    i++;
                }

                if (countDot == 1)
                {
                    return Codes.DoubleConstCode;
                }
            }
            // Если TryParse вернул false или в строке больше одной точки.
            // \ If TryParse returned false or there is more than one dot in the string.
            return Codes.ErrorCode;
        }

        private static Codes IsIdentifier(string text)
        {
            if (text == "")
            {
                return Codes.ErrorCode;
            }

            if (!Char.IsLetter(text[0]))
            {
                return Codes.ErrorCode;
            }
            else
            {
                foreach (char c in text)
                {
                    if (c != '_' && !Char.IsDigit(c) && !Char.IsLetter(c))
                    {
                        return Codes.ErrorCode;
                    }
                }
            }
            return Codes.IdentifierCode;
        }
        public static Codes IsKeyWord(string text)
        {
            if (text == "")
            {
                return Codes.ErrorCode;
            }
            switch (text)
            {
                case "while":
                    return Codes.WhileCode;
                case "for":
                    return Codes.ForCode;
                case "if":
                    return Codes.IfCode;
                case "else":
                    return Codes.ElseCode;
                case "break":
                    return Codes.BreakCode;
                default:
                    return Codes.ErrorCode;

            }
        }

        private static char GetNext(string text, int currentPosition)
        {
            return text[currentPosition + 1];
        }

        public static List<LexicalItem> GetTokens(string inputString)
        {
            int i = 0;
            string answer = "";
            var parts = new List<LexicalItem>();
            string subString = "";

            while (i < inputString.Length)
            {
                char c = inputString[i];


                // Может быть оператором =. \ Can be an operator.
                if (c == '=')
                {
                    int start = i + 1;
                    i++;
                    parts.Add(new LexicalItem(Codes.EqualCode, c.ToString(), start, i + 1));
                    continue;
                }
                if (c == ':')
                {
                    parts.Add(new LexicalItem(Codes.DoubleDotCode, c.ToString(), i + 1, i + 1));
                    i++;
                    continue;
                }
                // Может быть идентификатором или ключевым словом. \ Can be an identifier.
                if (Char.IsLetter(c))
                {
                    subString = "";
                    int start = i + 1;
                    while (i < inputString.Length - 1 && Char.IsLetter(inputString[i]))
                    {
                        if (IsKeyWord(subString) != Codes.ErrorCode)
                        {
                            i++;
                            parts.Add(new LexicalItem(IsKeyWord(subString), subString, start, i));

                            continue;
                        }
                        subString += inputString[i];
                        i++;
                    }

                    while ((i < inputString.Length) && (Char.IsLetter(inputString[i]) || Char.IsDigit(inputString[i])))
                    {
                        subString += inputString[i];
                        i++;
                    }

                    parts.Add(new LexicalItem(IsIdentifier(subString), subString, start, i));
                    subString = "";
                    continue;
                }

                // Может быть числом. \ Can be a number.
                if (Char.IsDigit(c))
                {
                    subString = "";
                    int start = i + 1;

                    while ((i < inputString.Length) && (Char.IsDigit(inputString[i]) || inputString[i] == '.'))
                    {
                        subString += inputString[i];
                        i++;
                    }

                    if (subString.EndsWith("."))
                    {
                        //parts.Add($"{start}:{i}", Codes.ErrorCode);
                        parts.Add(new LexicalItem(Codes.ErrorCode, subString, start, i));
                    }
                    else
                    {
                        //parts.Add($"{start}:{i}", IsNumber(subString));
                        parts.Add(new LexicalItem(IsNumber(subString), subString, start, i));
                    }
                    subString = "";
                }

                if (Char.IsWhiteSpace(c))
                {
                    parts.Add(new LexicalItem(Codes.SpaceConstCode, c.ToString(), i + 1, i + 1));
                    i++;
                }
            }

            return parts;
        }
    }
}
