using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using System.Text;

namespace 夜雀食堂_Beta2
{

    public class Area
    {
        public Area(string info, List<CmCus> cus)
        {
            var infos = info.Split('\t', 2);
            Name = new(infos[0]);

            CmSet = new(infos[1].Split('\t').Select(info => cus.First(c => c.Name == info)));
            if (IsQuanzou)
                CmSet.Add(cus.First(c => c.Name == "白狼天狗"));
            minDishScore = CmNum - DishReverseScore;
            minDrinkScore = CmNum - DrinkReverseScore;

            CmDishList = CmSet.First().DishForm.Select(dish => new CmSale(this, dish.Key, true)).ToList();
            CmDrinkList = CmSet.First().DrinkForm.Select(drink => new CmSale(this, drink.Key, false)).ToList();
            CmDishList = CmDishList.Where(dish => dish.Score >= minDishScore).OrderBy(dish => dish).ToList();
            CmDrinkList = CmDrinkList.Where(dish => dish.Score >= minDrinkScore).OrderBy(dish => dish).ToList();
        }

        public Area(string name, int ix, int rx, params Area[] areas)
        {
            Name = new(name);
            CmSet = new();
            foreach (var area in areas)
                CmSet = CmSet.Concat(area.CmSet).ToHashSet();
            if (areas.All(x => x.Name != "妖怪之山") && IsQuanzou)
                CmSet.RemoveWhere(x => x.Name == "白狼天狗");
            CmSet.RemoveWhere(x => x.Name == "地精");
            minDishScore = CmNum - ix;
            minDrinkScore = CmNum - rx;

            CmDishList = CmSet.First().DishForm.Select(dish => new CmSale(this, dish.Key, true)).ToList();
            CmDrinkList = CmSet.First().DrinkForm.Select(drink => new CmSale(this, drink.Key, false)).ToList();
            CmDishList = CmDishList.Where(dish => dish.Score >= minDishScore).OrderBy(dish => dish).ToList();
            CmDrinkList = CmDrinkList.Where(dish => dish.Score >= minDrinkScore).OrderBy(dish => dish).ToList();
        }

        public Area(List<CmCus> cus, string AliceName)
        {
            Name = new(AliceName);
            CmSet = cus.ToHashSet();

            minDishScore = CmNum - DishReverseScore;
            minDrinkScore = CmNum - DrinkReverseScore;

            CmDishList = CmSet.First().DishForm.Select(dish => new CmSale(this, dish.Key, true)).ToList();
            CmDrinkList = CmSet.First().DrinkForm.Select(drink => new CmSale(this, drink.Key, false)).ToList();
            CmDishList = CmDishList.Where(dish => dish.Score >= minDishScore).OrderBy(dish => dish).ToList();
            CmDrinkList = CmDrinkList.Where(dish => dish.Score >= minDrinkScore).OrderBy(dish => dish).ToList();
        }

        public override string ToString()
        {
            StringBuilder info = new();
            info.AppendLine(Name.Split(' ').Last());
            info.AppendLine();
            info.AppendLine();
            info.Append("普客:");
            foreach (var nm in CmSet) info.Append(nm.Name + ' ');

            if (CmDishList.Count == 0)
            {
                info.Append("\n\n无料理满足要求(威震天警告)\n\n");
                return info.ToString();
            }
            info.AppendLine();
            info.AppendLine();



            var str1 = CmDishList.Select(a => a.ToString()).ToList();
            var str2 = CmDrinkList.Select(a => a.ToString()).ToList();
            info.AppendLine("料理:".Pad( str1[0].RealLen(), true) + "\t酒水:\n");
            int rlen = str1[0].RealLen();
            while (str1.Count < str2.Count) str1.Add(new(' ', rlen));
            while (str1.Count > str2.Count) str2.Add(new(""));
            foreach (var str in str1.Zip(str2, (a, b) => a + '\t' + b + "\n\n"))
                info.Append(str);

            return info.ToString();
        }

        public string Name { get; }
        public int CmNum { get => CmSet.Count; }
        public HashSet<CmCus> CmSet { get; }
        public List<CmSale> CmDishList { get; set; }
        public List<CmSale> CmDrinkList { get; set; }


        public static bool IsQuanzou { get; set; } = false;
        public static bool IsAlice { get; set; } = false;


        public static int DishReverseScore { get; set; }
        public static int DrinkReverseScore { get; set; }

        private readonly int minDishScore;
        private readonly int minDrinkScore;

    }
}
