using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFYAiK
{
    class FiniteStateMachine
    {
        private List<LexicalItem> tokens;
        private int currentTokenIndex;
        public string result { get; private set; }

        private bool Compare(LexicalScanner.Codes code)
        {
            if (tokens[currentTokenIndex].lexicalCode == code)
            {
                return true;
            }
            return false;
        }

        private void Error(string message)
        {
            string errorMsg = $"\n{message} Получено '{tokens[currentTokenIndex]}'";
        }

        public string InitFSM(List<LexicalItem> tokens)
        {
            this.currentTokenIndex = 0;
            this.tokens = tokens;
            this.result = null;

            try
            {
                result = "I -> ";
                StateI();
            }
            catch
            {
                throw;
            }

            return result;
        }

        private void StateI()
        {
            if (Compare(LexicalScanner.Codes.LeftParen) || Compare(LexicalScanner.Codes.CurveLeftParen))
            {
                switch (tokens[currentTokenIndex].lexicalCode)
                {
                    case LexicalScanner.Codes.LeftParen:
                        result += "D -> ";
                        this.currentTokenIndex++;
                        StateD();
                        break;
                    case LexicalScanner.Codes.CurveLeftParen:
                        result += "K -> ";
                        this.currentTokenIndex++;
                        StateK();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Error("Ожидалось '(' или '{'");
            }
        }

        private void StateK()
        {

            if (Compare(LexicalScanner.Codes.LetterOrDigit))
            {
                result += "K -> ";
                this.currentTokenIndex++;
                StateK();
            }
            else
            {
                if (!Compare(LexicalScanner.Codes.Error) && !Compare(LexicalScanner.Codes.LetterOrDigit) && !Compare(LexicalScanner.Codes.CurveRightParen))
                {
                    result += "K -> ";
                    this.currentTokenIndex++;
                    StateK();
                }
                else
                if (Compare(LexicalScanner.Codes.CurveRightParen))
                {
                    result += "F";
                    StateF();
                }
            }
        }

        private void StateD()
        {
            if (Compare(LexicalScanner.Codes.Asterisk))
            {
                result += "C -> ";
                this.currentTokenIndex++;
                StateC();
            }
            else
            {
                Error("Ожидалось '*'");
            }
        }

        private void StateC()
        {
            if (!Compare(LexicalScanner.Codes.Error) && !Compare(LexicalScanner.Codes.Asterisk))
            {
                result += "C -> ";
                this.currentTokenIndex++;
                StateC();
            }
            else
            {
                if (Compare(LexicalScanner.Codes.Asterisk))
                {
                    result += "E -> ";
                    this.currentTokenIndex++;
                    StateE();
                }
                else
                {
                    Error("Ожидалось '*'");
                }
            }
        }

        private void StateE()
        {
            if (Compare(LexicalScanner.Codes.RightParen))
            {
                result += "F";
                this.currentTokenIndex++;
                StateF();
            }
            else
            {
                Error("Ожидалось ')'");
            }
        }

        private void StateF()
        {
            if (currentTokenIndex == tokens.Count)
            {
                return;
            }
            else
            {
                Error("Ожидался конец выражения");
            }
        }
    }
}
