using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    public enum LineStyle
    {
        Head,
        Mid
    }

    public partial class Line : List<ColorString>, IDeepCopy<Line>
    {
        public Line(int lineNum, LineStyle lineStyle = LineStyle.Head)
        {
            LineNum = lineNum;
            this.IneStyle = lineStyle;
        }

        public Line(IEnumerable<ColorString> strs, int lineNum, LineStyle lineStyle = LineStyle.Head)
            :base(strs)
        {
            LineNum = lineNum;
            this.IneStyle = lineStyle;
        }

        public Line GetDeepCopy()
        {
            Line line = new(LineNum, IneStyle);
            line.AddRange(SmpIE.GetDeepCopy(this));
            return line;
        }

        public virtual Line DeepCopy { get => GetDeepCopy(); }

        public Line CopyStyle() => new(LineNum, IneStyle);

        public LineStyle IneStyle { get; }

        public int LineNum { get; internal set; }

        public int RealLen { get => this.Aggregate(0, (a, b) => a + b.Str.RealLen()); }

        public int SetChoiceKey(int index)
        {
            foreach (var str in this)
                str.SetKey(ref index);
            return index;
        }


    }
    public static partial class IELine
    {
        static public int GetLineNum(this IEnumerable<Line> lines) =>
            lines.Sum(line => line.LineNum);
        static public void SuitLineNum(this IEnumerable<Line> lines, int num)
        {
            lines.Last().LineNum += num - lines.GetLineNum();
        }

    }
}
