using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static 夜雀食堂_Beta2.SpCus;

namespace 夜雀食堂_Beta2
{
    public class DishStuff
    {
        public DishStuff(string info)
        {
            var infos = info.Split('\t', StringSplitOptions.RemoveEmptyEntries);
            Name = new(infos[0]);
            Tags = infos[1].Split("、").ToList();
        }
        public string Name { get; }
        public List<string> Tags { get; } 

        public string ToString(int score)
        {
            return (Name.Pad(16, true) + $" S{score}").Pad(ISale.StrLen2, true);
        }

    }
}
