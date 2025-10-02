using System.Diagnostics;
using static SMPConsole.PagesReader;

namespace SMPConsole
{

    public partial class EndLine : Line
    {
        public EndLine(ColorString preStr, ColorString nextStr, LineStyle style, ConsoleKey[]? pre = null, ConsoleKey[]? next = null) : base(1, style)
        {
            headStr[0] = preStr;
            headStr[1] = nextStr;
            Pre = pre;
            Next = next;
        }

        internal Line[] GetPageCopy(PagesReader reader)
        {
            int len = reader.pages.Length;
            Line[] result = new Line[len];
            for (int i = 0; i < len; i++)
            {
                result[i] = this.DeepCopy;
                result[i].Insert(0, new ChoiceString(headStr[0], () => reader.pages[reader.PreIndex(i)].Show(), Pre));
                result[i].Insert(0, new ChoiceString(headStr[1], () => reader.pages[reader.NextIndex(i)].Show(), Next));
            }
            return result;
        }

        ColorString[] headStr = new ColorString[2];
        ConsoleKey[]? Pre;
        ConsoleKey[]? Next;
    }
    public class PagesReader
    {
        public PagesReader(Line head, Line[][] pageSourse, EndLine end, PageBase pageBase)
        {
            pages = new ReaderPage[pageSourse.Length];
            int MaxLines = pageSourse.Max(x => x.GetLineNum());
            var endLines = end.GetPageCopy(this);
            for (int i = 0; i < pages.Length; i++)
                pages[i] = new(head, pageSourse[i], endLines[i], pageBase, this, i, MaxLines);
        }

        internal int PreIndex(int i) => i == 0 ? pages.Length - 1 : i - 1;
        internal int NextIndex(int i) => i == pages.Length - 1 ? 0 : i + 1;

        public void Show()
        {
            pages[0].Show();
        }

        internal readonly ReaderPage[] pages;

        internal class ReaderPage : Page
        {
            internal ReaderPage(Line phead, IEnumerable<Line> psrs, Line pend, PageBase pagebase, PagesReader reader, int pageIndex, int MaxLines)
                : base(psrs, pagebase)
            {
                PageNumber = pageIndex;
                var head = phead.DeepCopy;
                var end = pend.DeepCopy;
                var srs = psrs.GetDeepCopy();
                int index = end.SetChoiceKey(0);
                srs.SetChoiceKey(index);
                srs.SuitLineNum(MaxLines);
                head.Add(head[^1].SameColorCopy($@"   第{pageIndex + 1}/{reader.pages.Length}页"));
                Lines = srs.Prepend(head).Append(end);
            }
            internal int PageNumber { get; set; }

        }

    }
}