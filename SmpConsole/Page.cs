using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    public class Page
    {
        //lines中应该已经初始化好了表
        public Page(IEnumerable<Line> lines, PageBase pagebase)
        {
            Lines = lines;
            Base = pagebase;
        }
        public IEnumerable<Line> Lines { get; protected init; }
        public PageBase Base { get; protected set; }

        public virtual void Show()
        {
            Base.Write(Lines);
        }

        static public void Show(IEnumerable<Line> lines, PageBase pagebase)
        {
            Page p = new(lines, pagebase);
            p.Show();
        }

    }

}
