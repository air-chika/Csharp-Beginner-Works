
using System.Diagnostics;
using System.Text;

namespace 夜雀食堂_Beta2
{

    static class Program
    {
        public static string Pad(this string str, int total, bool isLeft)
        {
            int doubleNum = 0;
            foreach (char c in str)
                if (c < 0 || c > 0x007f) doubleNum++;
            return isLeft ? str.PadRight(total - doubleNum) : str.PadLeft(total - doubleNum);
        }
        public static string PadMid(this string str, int total)
        {
            int doubleNum = 0;
            foreach (char c in str)
                if (c < 0 || c > 0x007f) doubleNum++;
            return str.PadLeft((total + str.Length - doubleNum) / 2).PadRight(total - doubleNum);
        }
        public static int RealLen(this string str)
        {
            int doubleNum = 0;
            foreach (char c in str)
                if (c < 0 || c > 0x007f) doubleNum++;
            return str.Length + doubleNum;
        }
        static List<string> GetCmStrings(string[] all, string file2)
        {
            var got = File.ReadAllLines(file2).Where(x => !x.Contains('x')).ToArray();
            if (got[0].StartsWith("全部")) return all.ToList();

/*            var alls = all.Select(al => al.Split('\t', 2, StringSplitOptions.RemoveEmptyEntries)[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries).Last());
            var errs = got.Where(x => !alls.Contains(x));
            foreach (var i in errs)
            {
                Console.WriteLine(i);
            }
            Console.ReadLine();
            Process.GetCurrentProcess().Kill();*/

            return got.Select(g => all.First(al =>
            {
                if (al.StartsWith("果味High Ball")) return "果味High Ball".IndexOf(g, StringComparison.CurrentCultureIgnoreCase) >= 0;
                return al.Split('\t', 2, StringSplitOptions.RemoveEmptyEntries)[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries).Last().IndexOf(g, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
            )).ToList();
        }
        static List<string> GetSpStrings(string[] all, string file2)
        {

            var got = File.ReadAllLines(file2).Select(s => s.EndsWith('x') ? s.Remove(s.Length - 1) : s).ToArray();
            if (got[0].StartsWith("全部")) return all.ToList();

            return got.Select(g => all.First(al =>
            {
                if (al.StartsWith("果味High Ball")) return "果味High Ball".IndexOf(g, StringComparison.CurrentCultureIgnoreCase) >= 0;
                return al.Split('\t', 2, StringSplitOptions.RemoveEmptyEntries)[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries).Last().IndexOf(g, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
            )).ToList();
        }
        static T Filt<T>(this string chose, T tu, T fs)
        {
            if (chose == "是") return tu;
            if (chose == "否" || chose == "无") return fs;
            return tu;
        }
        static bool Filt(this string chose)
        {
            if (chose == "是") return true;
            if (chose == "否" || chose == "无") return false;
            throw new Exception();
        }
        static void FirstCm(string[] firs, string[] dishdata, string[] drinkdata)
        {
            firs = firs.Select(f => f.Split(':')[1]).ToArray();
            int top = 0;
            DishBase.PopLike = firs[top].Filt(firs[top++], null);
            DishBase.PopHate = firs[top].Filt(firs[top++], null);
            Area.IsQuanzou = firs[top++].Filt();
            Area.IsAlice = firs[top++].Filt();
            DishBase.CmMinPrice = int.Parse(firs[top++]);
            DrinkBase.CmMinPrice = int.Parse(firs[top++]);
            Area.DishReverseScore = int.Parse(firs[top++]);
            Area.DrinkReverseScore = int.Parse(firs[top++]);
            CmSale.Icon = new(firs[top++]);

            GetCmStrings(dishdata, "MSQ\\★已获得料理.txt")
               .ForEach(info => CmDishBases.Add(new DishBase(info)));
            GetCmStrings(drinkdata, "MSQ\\★已获得酒水.txt")
                .ForEach(info => CmDrinkBases.Add(new DrinkBase(info)));
            CmDishBases = CmDishBases.Where(dish => dish.Price >= DishBase.CmMinPrice).ToList();
            CmDrinkBases = CmDrinkBases.Where(dish => dish.Price >= DrinkBase.CmMinPrice).ToList();
        }

        static void FirstYM(string[] firs)
        {
            if (!firs[0].Split(":")[1].Equals("是")) return;
            int xi = int.Parse(firs[1].Split(":")[1]);
            int xr = int.Parse(firs[2].Split(":")[1]);
            var infos = firs.Skip(3);
            foreach (var info in infos)
            {
                if (info.StartsWith("全部"))
                {
                    Areas.Add(new("试炼:所有人,都过来", xi, xr, Areas.ToArray()));
                    continue;
                }
                var infs = info.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Areas.Add(new("试炼:" + infs.Aggregate((re, i) => re + '+' + i)
                    , xi, xr,
               infs.ToList().Select(
                    name => Areas.Last(a => a.Name.Contains(name))).ToArray()
                    ));
            }
        }

        static void FirstVIP(string[] firs,string[] dishdata, string[] drinkdata, string[] stuffdata, string[] vipdata)
        {
            firs = firs.Select(f => f.Split(':')[1]).ToArray();
            int top = 0;
            //DishBase.SpMinPrice = int.Parse(firs[top++]);
            //DrinkBase.SpMinPrice = int.Parse(firs[top++]);
            GetSpStrings(dishdata, "MSQ\\★已获得料理.txt")
                .ForEach(info => VIPDishBases.Add(new(info)));
            GetSpStrings(drinkdata, "MSQ\\★已获得酒水.txt")
                .ForEach(info => VIPDrinkBases.Add(new(info)));

            SpForm.DishMinScore = int.Parse(firs[top++]);
            SpForm.StuffMinScore = int.Parse(firs[top++]);
            SpForm.DrinkMinScore = int.Parse(firs[top++]);
            SpCus.Icon[1] = new(firs[top++]);
            SpCus.Icon[2] = new(firs[top++]);
            SpCus.Icon[3] = new(firs[top++]);
            SpCus.Icon[4] = new(firs[top++]);
            SpCus.Icon[5] = new(firs[top++]);
            SpCus.ShowSPNeg = firs[top++].Equals("是");
            SpForm.Acsend = firs[top++].Equals("是") ? 1 : -1;

            //VIPDishBases = VIPDishBases.Where(dish => dish.Price >= DishBase.SpMinPrice).ToList();
            //VIPDrinkBases = VIPDrinkBases.Where(dish => dish.Price >= DrinkBase.SpMinPrice).ToList();

            stuffdata.ToList().ForEach(info => DishStuffs.Add(new(info)));
            vipdata.ToList().ForEach(info => VIPs.Add(new(info, VIPDishBases, VIPDrinkBases, DishStuffs)));

        }
       
        static void Main()
        {
            var data = File.ReadAllText("data.dat").Split('@').Select(data1 => data1.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)).ToArray();
            //dish,drink,cmcus,area,stuff,sp
            var firs = File.ReadAllText("MSQ\\★首选项.txt").Split("\r\n\r\n", 3).Select(fir => fir.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)).ToArray();
            //cm,ym,sp

            //处理普客,试炼
            FirstCm(firs[0], data[0], data[1]);
            data[2].ToList().ForEach(info => CmCustomers.Add(new(info, CmDishBases, CmDrinkBases)));
            data[3].ToList().ForEach(info => Areas.Add(new(info, CmCustomers)));
            FirstYM(firs[1]);
            if (Area.IsAlice) Areas.Add(new(CmCustomers, "z 爱丽丝符卡-全部客人"));

            File.WriteAllText("MSQ\\" + "普客" + ".txt", Areas.OrderBy(v => v.Name).Aggregate("", (all, v) => all + v.ToString() + new string('\n', 5)));

            //处理稀客
            FirstVIP(firs[2],data[0], data[1], data[4], data[5]);
            File.WriteAllText("MSQ\\" + "稀客" + ".txt", VIPs.OrderBy(v => v.Name).Aggregate("", (all, v) => all + v.GetString() + new string('\n', 5)));

        }
        

        static List<DishBase> CmDishBases = new();
        static List<DrinkBase> CmDrinkBases = new();
        static List<CmCus> CmCustomers = new();
        static List<Area> Areas = new();
        static List<DishBase> VIPDishBases = new();
        static List<DrinkBase> VIPDrinkBases = new();
        static List<DishStuff> DishStuffs = new();
        static List<SpCus> VIPs = new();
    }
}

