using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    public class ActionPage : Page
    {
        public ActionPage(IEnumerable<Line> lines, IEnumerable<KeyAction> keyActions,PageBase pagebase) : base(lines, pagebase)
        {
            this.keyActions = keyActions.ToList();
        }
        List<KeyAction> keyActions;

        public void ShowReadOnce()
        {
            Show();
            ReadThenRun();
        }
        void ReadThenRun()
        {
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                foreach (var i in keyActions)
                    if (i.RunKey(key))
                        return;
            }
        }
    }
}
