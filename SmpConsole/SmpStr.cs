using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    public static class SmpStr
    {
        public static string Pad(this string str, int total, bool isLeft)
        {
            int doubleNum = 0;
            foreach (char c in str)
                if (c < 0 || c > 0x007f) doubleNum++;
            return isLeft ? str.PadRight(total - doubleNum) : str.PadLeft(total - doubleNum);
        }
        public static string PadMid(this string str, int total)
        {
            int doubleNum = 0;
            foreach (char c in str)
                if (c < 0 || c > 0x007f) doubleNum++;
            return str.PadLeft((total + str.Length - doubleNum) / 2).PadRight(total - doubleNum);
        }
        public static int RealLen(this string str)
        {
            int doubleNum = 0;
            foreach (char c in str)
                if (c < 0 || c > 0x007f) doubleNum++;
            return str.Length + doubleNum;
        }

        public static string Blank(int num) => new string(' ', num);
        public static void CwBlank(int num) => Console.Write(new string(' ', num));
        public static string Line(int num) => new string('\n', num);
        public static void CwLine(int num) => Console.Write(new string('\n', num));

        public static bool IsNumber(this char a) => a >= '0' && a <= '9';

        public static IEnumerable<T> ToIE<T>(this T obj) => new T[] { obj };


    }

    public static class SmpIE
    {
        static public IEnumerable<T[]> Chunk<T>(this IEnumerable<T> ie, int size, T Default)
         => ie.Concat(Enumerable.Repeat(Default, size - ie.Count() % size)).Chunk(size);

        static public IEnumerable<T> GetDeepCopy<T>(this IEnumerable<T> ie) where T : IDeepCopy<T>
        {
            return ie.Select(i => i.GetDeepCopy());
        }

    }
}
