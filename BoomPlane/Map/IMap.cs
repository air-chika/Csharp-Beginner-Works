using SMPConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomPlane.Map
{
    internal abstract class IMap
    {
        protected abstract int[][] Map { get; } //YX
        protected abstract string[] SitStr { get; }
        protected abstract ConsoleColor[] SitClr { get; }

        ColorString GetStr(int situation)
        {
            return new(SitStr[situation], SitClr[situation]);
        }
        Line GetHeadLine()
        {
            Line line = new(1);
            line.Add(new(" ", ConsoleColor.Green));
            for (int i = 1; i < 10; i++)
                line.Add(new(i.ToString().PadRight(2), ConsoleColor.Green));
            line.Add(new(0.ToString().PadRight(2), ConsoleColor.Green));
            return line;
        }

        virtual public IEnumerable<Line> ToLines()
        {
            return
                Map.Select(
                    (arr, i) => new Line(arr.Select(a => GetStr(a))
                    .Prepend(new(((char)(i + 'A')).ToString(), ConsoleColor.Green)), 1)
                    )
                .Prepend(GetHeadLine());
        }

        public static int mapLength = 10;
        public static int mapWidth = 10;

    }
}
