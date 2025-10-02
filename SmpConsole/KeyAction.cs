using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    public class KeyAction
    {
        public KeyAction(Action action, params ConsoleKey[]? keys)
        {
            Action = action;
            Keys = keys != null ? keys.ToList() : new();
        }

        public bool RunKey(ConsoleKey ck)
        {
            
            if (Keys.Contains(ck))
            {
                Action();
                return true;
            }
            return false;
        }

        public List<ConsoleKey> Keys { get; }
        public Action Action { get; }

    }
}
