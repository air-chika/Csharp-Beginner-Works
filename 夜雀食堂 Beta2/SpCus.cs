using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace 夜雀食堂_Beta2
{
    public class SpForm
    {
        internal class Cmp1 : IComparer<List<int>>
        {
            public static int Score(List<int> x)
            {
                return x.Count(x => x == 1) - x.Count(x => x == 2);
            }
            public int Compare(List<int>? x, List<int>? y)
            {
                int a = CP1(x, y);
                if (a != 0) return a;
                return CP2(x, y);
            }
            public int CP1(List<int>? x, List<int>? y)
            {
                if (x == null || y == null) return 0;
                return Score(y) - Score(x);
            }
            public int CP2(List<int>? x, List<int>? y)
            {
                if (x == null || y == null) return 0;
                //     if (x.Count(x => x == 1) != y.Count(y => y == 1))
                //       return y.Count(y => y == 1) - x.Count(x => x == 1);
                for (int i = 0; i < x.Count; i++)
                {
                    int xi = x[i] == 1 ? 1 : 0;
                    int yi = y[i] == 1 ? 1 : 0;
                    if (xi != yi) return yi - xi;
                }
                for (int i = 0; i < x.Count; i++)
                {
                    int xi = x[i] == 2 ? 1 : 0;
                    int yi = y[i] == 2 ? 1 : 0;
                    if (xi != yi) return yi - xi;
                }
                return 0;
            }
        }
        class Cmp2 : IComparer<KeyValuePair<ISale, List<int>>>
        {
            public int Compare(KeyValuePair<ISale, List<int>> x, KeyValuePair<ISale, List<int>> y)
            {
                if (x.Key.Price != y.Key.Price)
                    return Acsend * (x.Key.Price - y.Key.Price);
                Cmp1 cmp = new();
                int a1 = cmp.CP1(x.Value, y.Value);

                if (a1 != 0) return a1;
                return cmp.CP2(x.Value, y.Value);
            }
        }

        public SpForm(SpCus vip, List<DishBase> dishBases, List<DrinkBase> drinkBases, List<DishStuff> dishStuffs)
        {

            foreach (DishStuff dishStuff in dishStuffs)
            {
                var list = new List<int>();
                foreach (var tag in vip.Tags)
                    list.Add(formValue(dishStuff, tag, true));
                if (vip.NegTags.Count > 0)
                    list.Add(-1);
                foreach (var tag in vip.NegTags)
                    list.Add(formValue(dishStuff, tag, false));
                stuffForm.Add(dishStuff, list);
            }
            foreach (DishBase dishBase in dishBases)
            {
                var list = new List<int>();
                foreach (var tag in vip.Tags)
                    list.Add(formValue(dishBase, tag, true));
                if (vip.NegTags.Count > 0)
                    list.Add(-1);
                foreach (var tag in vip.NegTags)
                    list.Add(formValue(dishBase, tag, false));
                dishForm.Add(dishBase, list);
            }
            foreach (DrinkBase drinkBase in drinkBases)
            {
                var list = vip.DrinkTags.Select(tag => drinkBase.Tags.Contains(tag) ? 1 : 0);
                drinkForm.Add(drinkBase, list.ToList());
            }

            dishForm = dishForm.Where(d => Cmp1.Score(d.Value) >= DishMinScore).OrderBy(drink => drink, new Cmp2()).ToDictionary(form => form.Key, form => form.Value);
            drinkForm = drinkForm.Where(d => Cmp1.Score(d.Value) >= DrinkMinScore).OrderByDescending(drink => drink, new Cmp2()).ToDictionary(form => form.Key, form => form.Value);
            stuffForm = stuffForm.Where(d => Cmp1.Score(d.Value) >= StuffMinScore).OrderBy(drink => drink.Value, new Cmp1()).ToDictionary(form => form.Key, form => form.Value);
        }
        int formValue(DishBase dishBase, string tag, bool isPos)
        {
            if (dishBase.Tags.Contains(tag))
                return isPos ? 1 : 2;
            else if (dishBase.NegTags.Contains(tag))
                return 3;
            else if (dishBase.DishStuffs.Count == 5)
                return 3;
            else if (!fft.Contains(tag))
                return isPos ? 4 : 5;
            return 0;
        }
        int formValue(DishStuff dishBase, string tag, bool isPos)
        {
            if (dishBase.Tags.Contains(tag))
            {
                fft.Add(tag);
                return isPos ? 1 : 2;
            }
            return 0;
        }
        public Dictionary<ISale, List<int>> dishForm = new();
        public Dictionary<ISale, List<int>> drinkForm = new();
        public Dictionary<DishStuff, List<int>> stuffForm = new();
        readonly List<string> fft = new List<string>().Append("大份").ToList();

        public static int Acsend { get; set; } = -1;
        public static int DishMinScore;
        public static int DrinkMinScore;
        public static int StuffMinScore;
    }

    public class SpCus
    {
        public string Name { get; }
        public string RealName { get => Name.Split(' ').Last(); }
        readonly int MinBudget;
        readonly int MaxBudget;

        public SpCus(string info, List<DishBase> dishBases, List<DrinkBase> drinkBases, List<DishStuff> dishStuffs)
        {
            var infos = info.Split('\t', StringSplitOptions.RemoveEmptyEntries);
            Name = new(infos[0]);
            var bg = infos[1].Split('－');
            MinBudget = int.Parse(bg[0]);
            MaxBudget = int.Parse(bg[1]);
            infos[2].Split("、").ToList().ForEach(x =>
            {
                if (DishBase.FiltPop(x) != null)
                    Tags.Add(new(DishBase.FiltPop(x)));
            });
            infos[3].Split("、").ToList().ForEach(x => DrinkTags.Add(new(x)));
            if (infos.Length > 4)
                infos[4].Split("、").ToList().ForEach(x =>
                {
                    if (DishBase.FiltPop(x) != null)
                        NegTags.Add(new(DishBase.FiltPop(x)));
                });
            form = new(this, dishBases, drinkBases, dishStuffs);
        }

        public List<string> Tags { get; } = new List<string>();
        public List<string> NegTags { get; } = new List<string>();
        public List<string> DrinkTags { get; } = new List<string>();

        readonly SpForm form;
        public static string[] Icon { get; set; } = new string[] { " ", "■", "★", "×", "□", "☆" };

        public static bool ShowSPNeg = false;

        public string GetString()
        {
            if (ShowSPNeg)
            {
                PrintWithNeg printn = new(this);
                return printn.GetString();
            }
            Print print = new(this);
            return print.GetString();
        }

        public class PrintWithNeg
        {
            public PrintWithNeg(SpCus spCus)
            {
                sp = spCus;
                blankLen = ISale.StrLen2 + spCus.Tags.Count * 8 + ((spCus.NegTags.Count > 0) ? (spCus.NegTags.Count + 1) * 8 : 0);
                blank = new(' ', blankLen);
            }

            public string GetString()
            {
                var left = GetDish().Append(blank).Concat(GetStuff()).ToList();
                var right = GetDrink();
                while (left.Count > right.Count) right.Add("");
                while (left.Count < right.Count) left.Add(blank);
                return GetSpHead() + "\n\n" + left.Zip(right, (a, b) => a + "\t\t" + b + "\n\n").Aggregate((a, b) => a + b);
            }

            string GetIntArr(List<int> arr)
            {
                return arr.Aggregate("", (a, b) => a + SpCus.Icon[b == -1 ? 0 : b].PadMid(8));
            }

            List<string> GetDish()
            {
                List<string> dish = new List<string>();
                dish.Add(GetDishHead(""));
                return dish.Concat(sp.form.dishForm.Select(dish => dish.Key.ToString(sp.MinBudget, sp.MaxBudget, SpForm.Cmp1.Score(dish.Value))
                  + GetIntArr(dish.Value)
                )).ToList();
            }
            List<string> GetStuff()
            {
                List<string> dish = new List<string>();
                dish.Add(GetStuffHead(""));
                return dish.Concat(sp.form.stuffForm.Select(dish => dish.Key.ToString(SpForm.Cmp1.Score(dish.Value))
                  + GetIntArr(dish.Value)
                )).ToList();
            }
            List<string> GetDrink()
            {
                List<string> dish = new List<string>();
                dish.Add(GetDrinkHead(""));
                return dish.Concat(sp.form.drinkForm.Select(dish => dish.Key.ToString(sp.MinBudget, sp.MaxBudget, SpForm.Cmp1.Score(dish.Value))
                  + GetIntArr(dish.Value)
                )).ToList();
            }

            string GetDishHead(string Name)
            {
                return $"料理喜爱Tag:{Name}".Pad(ISale.StrLen2, true) +
                sp.Tags.Aggregate("", (a, b) => a + b.PadMid(8)) +
                ((sp.NegTags.Count > 0) ? "厌恶Tag:".PadMid(8) + sp.NegTags.Aggregate("", (a, b) => a + b.PadMid(8)) : "");
            }
            string GetStuffHead(string Name)
            {
                return $"食材喜爱Tag:{Name}".Pad(ISale.StrLen2, true) +
                sp.Tags.Aggregate("", (a, b) => a + b.PadMid(8)) +
                ((sp.NegTags.Count > 0) ? "厌恶Tag:".PadMid(8) + sp.NegTags.Aggregate("", (a, b) => a + b.PadMid(8)) : "");
            }
            string GetDrinkHead(string Name)
            {
                return $"酒水喜爱Tag:{Name}".Pad(ISale.StrLen2, true) +
                sp.DrinkTags.Aggregate("", (a, b) => a + b.PadMid(8));
            }

            string GetSpHead()
            {
                return sp.RealName + "   预算:" + sp.MinBudget + " - " + sp.MaxBudget + "円"
                + $"(平均{sp.MinBudget / 2 + sp.MaxBudget / 2}円)"
                + $"   期望浮动:{(double)(sp.MinBudget * 10) / (sp.MinBudget + sp.MaxBudget):0.00} " +
                $"- {(double)(sp.MaxBudget * 10) / (sp.MinBudget + sp.MaxBudget):0.00}"
                + $" (平均:{(double)(sp.MinBudget * 2) / (sp.MinBudget + sp.MaxBudget):0.00} - {(double)(sp.MaxBudget * 2) / (sp.MinBudget + sp.MaxBudget):0.00})";
            }

            SpCus sp;
            int blankLen; //空行的空格数
            string blank;

        }

        public class Print
        {
            public Print(SpCus spCus)
            {
                sp = spCus;
                blankLen = ISale.StrLen2 + spCus.Tags.Count * 8;
                blank = new(' ', blankLen);
            }

            public string GetString()
            {
                var left = GetDish().Append(blank).Concat(GetStuff()).ToList();
                var right = GetDrink();
                while (left.Count > right.Count) right.Add("");
                while (left.Count < right.Count) left.Add(blank);
                return GetSpHead() + "\n\n" + left.Zip(right, (a, b) => a + "\t\t" + b + "\n\n").Aggregate((a, b) => a + b);
            }

            string GetIntArr(List<int> arr)
            {
                return arr.TakeWhile(a => a != -1).Aggregate("", (a, b) => a + SpCus.Icon[b].PadMid(8));
            }

            List<string> GetDish()
            {
                List<string> dish = new List<string>();
                dish.Add(GetDishHead(""));
                return dish.Concat(sp.form.dishForm.Select(dish => dish.Key.ToString(sp.MinBudget, sp.MaxBudget, SpForm.Cmp1.Score(dish.Value))
                  + GetIntArr(dish.Value)
                )).ToList();
            }
            List<string> GetStuff()
            {
                List<string> dish = new List<string>();
                dish.Add(GetStuffHead(""));
                return dish.Concat(sp.form.stuffForm.Select(dish => dish.Key.ToString(SpForm.Cmp1.Score(dish.Value))
                  + GetIntArr(dish.Value)
                )).ToList();
            }
            List<string> GetDrink()
            {
                List<string> dish = new List<string>();
                dish.Add(GetDrinkHead(""));
                return dish.Concat(sp.form.drinkForm.Select(dish => dish.Key.ToString(sp.MinBudget, sp.MaxBudget, SpForm.Cmp1.Score(dish.Value))
                  + GetIntArr(dish.Value)
                )).ToList();
            }

            string GetDishHead(string Name)
            {
                return $"料理喜爱Tag:{Name}".Pad(ISale.StrLen2, true) +
                sp.Tags.Aggregate("", (a, b) => a + b.PadMid(8));
            }
            string GetStuffHead(string Name)
            {
                return $"食材喜爱Tag:{Name}".Pad(ISale.StrLen2, true) +
                sp.Tags.Aggregate("", (a, b) => a + b.PadMid(8));
            }
            string GetDrinkHead(string Name)
            {
                return $"酒水喜爱Tag:{Name}".Pad(ISale.StrLen2, true) +
                sp.DrinkTags.Aggregate("", (a, b) => a + b.PadMid(8));
            }

            string GetSpHead()
            {
                return sp.RealName + "   预算:" + sp.MinBudget + " - " + sp.MaxBudget + "円"
                + $"(平均{sp.MinBudget / 2 + sp.MaxBudget / 2}円)"
                + $"   期望浮动:{(double)(sp.MinBudget * 10) / (sp.MinBudget + sp.MaxBudget):0.00} " +
                $"- {(double)(sp.MaxBudget * 10) / (sp.MinBudget + sp.MaxBudget):0.00}"
                + $" (平均:{(double)(sp.MinBudget * 2) / (sp.MinBudget + sp.MaxBudget):0.00} - {(double)(sp.MaxBudget * 2) / (sp.MinBudget + sp.MaxBudget):0.00})";
            }

            SpCus sp;
            int blankLen; //空行的空格数
            string blank;

        }

        public class PrintEveLine
        {
            public PrintEveLine(SpCus spCus)
            {
                sp = spCus;
                blankLen = ISale.StrLen2 + spCus.Tags.Count * 8;
                blank = new(' ', blankLen);
            }

            public string GetString()
            {
                var left = GetDish().Append(blank).Append(blank).Concat(GetStuff()).ToList();
                var right = GetDrink();
                while (left.Count > right.Count) right.Add("");
                while (left.Count < right.Count) left.Add(blank);
                return GetSpHead() + "\n\n" + left.Zip(right, (a, b) => a + "\t\t" + b).Aggregate((a, b) => a + "\n\n" + b);
            }

            string GetIntArr(List<int> arr)
            {
                return arr.TakeWhile(a => a != -1).Aggregate("", (a, b) => a + SpCus.Icon[b].PadMid(8));
            }

            List<string> GetDish()
            {
                return sp.form.dishForm.Select(dish => dish.Key.ToString(sp.MinBudget, sp.MaxBudget, SpForm.Cmp1.Score(dish.Value)) + GetIntArr(dish.Value))
                    .Aggregate(new List<string>(), (a, b) => { a.Add(GetDishHead(false)); a.Add(b); return a; })
                    .ToList();
            }
            List<string> GetStuff()
            {
                return sp.form.stuffForm.Select(dish => dish.Key.ToString(SpForm.Cmp1.Score(dish.Value)) + GetIntArr(dish.Value))
                    .Aggregate(new List<string>(), (a, b) => { a.Add(GetStuffHead(false)); a.Add(b); return a; })
                    .ToList();

            }
            List<string> GetDrink()
            {
                return sp.form.drinkForm.Select(dish => dish.Key.ToString(sp.MinBudget, sp.MaxBudget, SpForm.Cmp1.Score(dish.Value)) + GetIntArr(dish.Value))
                    .Aggregate(new List<string>(), (a, b) => { a.Add(GetDrinkHead(false)); a.Add(b); return a; })
                    .ToList();
            }

            string GetDishHead(bool show)
            {
                return (show ? "料理:" : "").Pad(ISale.StrLen2, true) +
                sp.Tags.Aggregate("", (a, b) => a + b.PadMid(8));
            }
            string GetStuffHead(bool show)
            {
                return (show ? "食材:" : "").Pad(ISale.StrLen2, true) +
                sp.Tags.Aggregate("", (a, b) => a + b.PadMid(8));
            }
            string GetDrinkHead(bool show)
            {
                return (show ? "酒水:" : "").Pad(ISale.StrLen2, true) +
                sp.DrinkTags.Aggregate("", (a, b) => a + b.PadMid(8));
            }

            string GetSpHead()
            {
                return sp.RealName + "   预算:" + sp.MinBudget + " - " + sp.MaxBudget + "円"
                + $"(平均{sp.MinBudget / 2 + sp.MaxBudget / 2}円)"
                + $"   期望浮动:{(double)(sp.MinBudget * 10) / (sp.MinBudget + sp.MaxBudget):0.00} " +
                $"- {(double)(sp.MaxBudget * 10) / (sp.MinBudget + sp.MaxBudget):0.00}"
                + $" (平均:{(double)(sp.MinBudget * 2) / (sp.MinBudget + sp.MaxBudget):0.00} - {(double)(sp.MaxBudget * 2) / (sp.MinBudget + sp.MaxBudget):0.00})";
            }

            SpCus sp;
            int blankLen; //空行的空格数
            string blank;

        }


    }
}
