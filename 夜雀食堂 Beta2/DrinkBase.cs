using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 夜雀食堂_Beta2
{

    public class DrinkBase : ISale, ISaleTag
    {
        static public int CmMinPrice { get; set; }
        static public int SpMinPrice { get; set; }

        public DrinkBase(string info)
        {
            var infos = info.Split('\t', StringSplitOptions.RemoveEmptyEntries);
            Name = new(infos[0]);
            Price = int.Parse(infos[1]);
            infos[2].Split("、").ToList().ForEach(x => Tags.Add(new(x)));
        }
        public string Name { get; }
        public int Price { get; }
        public List<string> Tags { get; } = new List<string>();

        public bool CmLove(CmCus customer)
        {
            return Tags.Any(tag => customer.DrinkTags.Contains(tag));
        }
        public override string ToString()
        {
            return Name.Pad(16, true) + Price.ToString().Pad(4, false) + "円    ";
        }

        public string ToString(int bud1, int bud2, int score)
        {
            return Name.Pad(16, true)+$" S{score}" + $"{(double)(Price * 10) / (bud1 + bud2),5:0.00}   ";
        }

    }
}
