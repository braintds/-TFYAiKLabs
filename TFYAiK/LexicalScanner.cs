using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using static TFYAiK.LexicalScanner;

namespace TFYAiK
{
    public struct LexicalItem
    {
        public Codes lexicalCode;
        public char item;
        public int Position;

        public LexicalItem(Codes code, char item, int Position)
        {
            this.lexicalCode = code;
            this.item = item;
            this.Position = Position;
        }

        public override string ToString()
        {
            return $"{Position}: [{item}] {lexicalCode}: {Convert.ToInt16(lexicalCode)}";
        }
    }

    public static class LexicalScanner
    {
        public enum Codes
        {
            Error = -1,
            LeftParen = 1,
            RightParen,
            Asterisk,
            CurveLeftParen,
            CurveRightParen,
            LetterOrDigit
        }

        public static List<LexicalItem> GetTokens(string inputString)
        {
            int i = 0;
            var parts = new List<LexicalItem>();

            while (i < inputString.Length)
            {
                char c = inputString[i];

                if (!Char.IsDigit(c) && !Char.IsLetter(c))
                {
                    Codes code;

                    switch (c)
                    {
                        case '(':
                            code = Codes.LeftParen;
                            break;
                        case ')':
                            code = Codes.RightParen;
                            break;
                        case '*':
                            code = Codes.Asterisk;
                            break;
                        case '{':
                            code = Codes.CurveLeftParen;
                            break;
                        case '}':
                            code = Codes.CurveRightParen;
                            break;
                        default:
                            code = Codes.Error;
                            break;
                    }

                    i++;
                    parts.Add(new LexicalItem(code, c, i));
                    continue;
                }
                else
                {
                    i++;
                    parts.Add(new LexicalItem(Codes.LetterOrDigit, c, i));
                }
            }
            return parts;
        }
    }
}