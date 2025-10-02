using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    public static class SmpError
    {
        public static void Exit(string info)
        {
            Console.WriteLine(info);
            Console.ReadLine();
            Process.GetCurrentProcess().Kill();
        }
    }
}
