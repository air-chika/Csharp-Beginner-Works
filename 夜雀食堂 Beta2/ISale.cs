using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace 夜雀食堂_Beta2
{
    public interface ISale
    {
        string Name { get; }
        int Price { get; }
        string ToString();
        string ToString(int bud1,int bud2, int score); 
        static public int StrLen { get => 26; }
        static public int StrLen2 { get => 27; } 
    }
}
