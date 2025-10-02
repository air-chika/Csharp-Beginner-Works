using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace 夜雀食堂_Beta2
{
    public class DishBase : ISale, ISaleTag
    {
        static public string? PopLike { get; set; } = null;
        static public string? PopHate { get; set; } = null;
        static public string? FiltPop(string tag)
        {
            return tag switch
            {
                "流行·喜爱" => PopLike,
                "流行·厌恶" => PopHate,
                _ => tag,
            };
        }
        static public int CmMinPrice { get; set; }
        static public int SpMinPrice { get; set; }

        public DishBase(string info)
        {
            var infos = info.Split('\t', StringSplitOptions.RemoveEmptyEntries);
            name = new(infos[0]);
            price = int.Parse(infos[1]);
            infos[2].Split("、").ToList().ForEach(x => DishStuffs.Add(new(x)));
            infos[3].Split("、").ToList().ForEach(x => Tags.Add(new(x)));
            if (infos.Length > 4)
                infos[4].Split("、").ToList().ForEach(x => NegTags.Add(new(x)));
            if (DishStuffs.Count == 5)
            {
                Tags.Add(new("大份"));
            }
        }
        DishBase(string nm, int pr) { name = new(nm); price = pr; }
        public static DishBase GetNull(string name) => new DishBase(name, 0);


        string name;
        int price;
        public string Name { get => name; }
        public int Price { get => price; }
        public List<string> Tags { get; } = new List<string>();
        public List<string> NegTags { get; } = new List<string>();
        public List<string> DishStuffs { get; } = new List<string>();

        public override string ToString()
        {
            return Name.Pad(16, true) + Price.ToString().Pad(4, false) + "円    ";
        }
        public string ToString(int bud1, int bud2, int score)
        {
            return Name.Pad(16, true) + $" S{score}" + $"{(double)(Price * 10) / (bud1 + bud2),5:0.00}   ";
        }

        public bool CmLove(CmCus customer)
        {
            if (PopHate != null && Tags.Contains(PopHate)) return false;
            if (PopLike != null && Tags.Contains(PopLike)) return true;
            return Tags.Any(tag => customer.DishTags.Contains(tag));
        }

    }


}
