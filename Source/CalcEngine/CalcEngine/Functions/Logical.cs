using System;
using System.Collections.Generic;

namespace CalcEngine
{
    public static class Logical
    {
        public static void Register(CalcEngine ce)
        {
            ce.RegisterFunction("AND", 1, int.MaxValue, And);
            ce.RegisterFunction("OR", 1, int.MaxValue, Or);
            ce.RegisterFunction("NOT", 1, Not);
            ce.RegisterFunction("IF", 3, If);
            ce.RegisterFunction("TRUE", 0, True);
            ce.RegisterFunction("FALSE", 0, False);
            ce.RegisterFunction("ISNUM", 1, 1,IsNumber);
        }

#if DEBUG
        public static void Test(CalcEngine ce)
        {
            ce.Test("AND(true, true)", true);
            ce.Test("AND(true, false)", false);
            ce.Test("AND(false, true)", false);
            ce.Test("AND(false, false)", false);
            ce.Test("OR(true, true)", true);
            ce.Test("OR(true, false)", true);
            ce.Test("OR(false, true)", true);
            ce.Test("OR(false, false)", false);
            ce.Test("NOT(false)", true);
            ce.Test("NOT(true)", false);
            ce.Test("IF(5 > 4, true, false)", true);
            ce.Test("IF(5 > 14, true, false)", false);
            ce.Test("TRUE()", true);
            ce.Test("FALSE()", false);
            ce.Test("ISNUM(1234)", true);
            ce.Test("ISNUM(12.34)", true);
            ce.Test("ISNUM(121.34)", true);
        }
#endif
        static object And(List<Expression> p)
        {
            var b = true;
            foreach (var v in p)
            {
                b = b && (bool)v;
            }
            return b;
        }
        static object Or(List<Expression> p)
        {
            var b = false;
            foreach (var v in p)
            {
                b = b || (bool)v;
            }
            return b;
        }
        static object Not(List<Expression> p)
        {
            return !(bool)p[0];
        }
        static object If(List<Expression> p)
        {
            return (bool)p[0] 
                ? p[1].Evaluate() 
                : p[2].Evaluate();
        }
        static object True(List<Expression> p)
        {
            return true;
        }
        static object False(List<Expression> p)
        {
            return false;
        }

        static object IsNumber(List<Expression> p)
        {
            var s = (string)p[0];
            bool isNum = Double.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out double retNum);
            if (isNum) return isNum;                        
            isNum = long.TryParse(s, System.Globalization.NumberStyles.HexNumber, null, out long output);
            return isNum;
        }

        static object IsHexNumber(List<Expression> p)
        {
            var input = (string)p[0];
            return Int32.TryParse(input, System.Globalization.NumberStyles.HexNumber, null, out int myInt);
        }
    }
}
