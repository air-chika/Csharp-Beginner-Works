using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 夜雀食堂_Beta2
{
    public class CmCus
    {
        public CmCus(string info, List<DishBase> dishes, List<DrinkBase> drinks)
        {
            var infos = info.Split('\t', StringSplitOptions.RemoveEmptyEntries);
            Name = new(infos[0]);
            infos[1].Split("、").ToList().ForEach(x => DishTags.Add(new(x)));
            infos[2].Split("、").ToList().ForEach(x => DrinkTags.Add(new(x)));

            foreach (DishBase dish in dishes)
                DishForm.Add(dish, dish.CmLove(this));
            foreach (DrinkBase drink in drinks)
                DrinkForm.Add(drink, drink.CmLove(this));

        }


        public string Name { get; }
        public List<string> DishTags { get; } = new List<string>();
        public List<string> DrinkTags { get; } = new List<string>();
        public Dictionary<ISale, bool> DishForm { get; } = new ();
        public Dictionary<ISale, bool> DrinkForm { get; } = new ();

    }
}
