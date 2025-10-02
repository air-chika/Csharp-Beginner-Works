using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    partial class Line
    {
        internal virtual void Write()
        {
            foreach (var l in this)
                l.Write();
            SmpStr.CwLine(LineNum);
        }
    }
    partial class EndLine
    {
        internal override void Write()
        {
            foreach (var l in this)
                l.Write();
        }
    }

    public class PageBase
    {
        public PageBase(Func<Line[], int, int> Lines_Width_Blank, Func<Line[], int> Lines_Hei_Line, ConsoleColor AllBack=ConsoleColor.Black, ConsoleColor? Back = null)
        {
            headBlankFunc = Lines_Width_Blank;
            headLineFunc = Lines_Hei_Line;
            this.AllBack = AllBack;
            this.Back = Back;
        }

        Func<Line[], int, int> headBlankFunc;
        Func<Line[], int> headLineFunc;

        public ConsoleColor AllBack { get; set; }
        public ConsoleColor? Back { get; set; }
        void Clear()
        {
            Console.BackgroundColor = AllBack;
            Console.Clear();
            Console.BackgroundColor = Back ?? AllBack;
        }

        public void Write(IEnumerable<Line> lines)
        {
            Clear();
            var ls = lines.ToArray();
            SmpStr.CwLine(headLineFunc(ls));
            for (int i = 0; i < ls.Length; i++)
            {
                SmpStr.CwBlank(headBlankFunc(ls, i));
                ls[i].Write();
            }
        }

    }


}
