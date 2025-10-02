using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    public static class SMPTool
    {
        static public void Swap<T>(T x, T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }
        static public void Swap<T>(ref T x,ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        static public T[][] GetNew<T>(int len1, int len2, T defalt)
        {
            T[][] ret = new T[len1][];
            for (int i = 0 ; i < len1 ; i++)
            {
                ret[i] = new T[len2];
                for (int j = 0 ; j < len2 ; j++)
                    ret[i][j] = defalt;
            }
            return ret;
        }

        static public T[][] GetNew<T>(int len1, int len2, T defalt, params T[] para)
        {
            T[][] ret = new T[len1][];
            int top = 0;
            for (int i = 0 ; i < len1 ; i++)
            {
                ret[i] = new T[len2];
                for (int j = 0 ; j < len2 ; j++)
                    if (top == para.Length)
                        ret[i][j] = defalt;
                    else
                        ret[i][j] = para[top++];
            }
            return ret;
        }
        static public T[] GetNew<T>(int len) where T : new()
        {
            T[] ret = new T[len];
            for (int i = 0 ; i < len ; i++)
                ret[i] = new T();
            return ret;
        }
        static public T[] GetNew<T>(int len, Func<T> newDefault)
        {
            T[] ret = new T[len];
            for (int i = 0 ; i < len ; i++)
                ret[i] = newDefault();
            return ret;
        }

        public static IEnumerator<int> GetEnumerator(this Range range)
        {
            if (range.End.IsFromEnd || range.Start.IsFromEnd)
                throw new ArgumentException(nameof(range));

            for (int i = range.Start.Value ; i <= range.End.Value ; i++)
                yield return i;
        }

    }
}
