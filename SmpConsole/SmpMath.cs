using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    public static class SmpMath
    {
        static public int DivideUp(int x , int y) => x / y + ((x % y) == 0 ? 0 : 1);

    }
}
