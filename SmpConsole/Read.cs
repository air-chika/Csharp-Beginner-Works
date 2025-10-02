using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    static public class Read
    {
        //最多61个选项
        static public int GetChoice(int range)
        {
            while (true)
            {
                var c = TransChoice(Console.ReadKey(true).KeyChar);
                if (c > 0 && c <= range) return c;
            }
        }

        //最多61个选项
        static public int TransChoice(char a)
        {
            if (a > '0' && a <= '9') return (int)(a - '0');
            if (a >= 'a' && a <= 'z') return (int)(a - 'a' + 10);
            if (a >= 'A' && a <= 'Z') return (int)(a - 'A' + 36);
            return 0;
        }
        //最多61个选项
        static public char TransChoice(int c)
        {
            if (c > 0 && c <= 9) return (char)(c + '0');
            if (c >= 10 && c <= 35) return (char)(c - 10 + 'a');
            return (char)(c - 36 + 'A');
        }

       
    }
}
