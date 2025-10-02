using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace 夜雀食堂_Beta2
{


    public class CmSale : IComparable<CmSale>
    {
        public CmSale(Area area, ISale dishBase,  bool isDi)
        {
            Sale = dishBase;
            EveScore = area.CmSet.Select(cus => (isDi ? cus.DishForm : cus.DrinkForm)[dishBase]).ToArray();
            Score = EveScore.Count(x => x == true);
        }

        public ISale Sale { get; }
        public int Score { get; }
        public bool[] EveScore;

        public static int ScoreAscend { get; set; } = -1;
        public static int PriceAscend { get; set; } = -1;
        public static bool UseIcon { set; get; }
        public static string Icon { set; get; } = "■";
        // public static string NegIcon { set; get; } = "■";

        public override string ToString() =>
            Sale.ToString() + EveScore.Aggregate("", (s, i) => s + (i ? Icon : "").Pad(3, false));

        public int CompareTo(CmSale? other)
        {
            if (other == null) return ScoreAscend;
            if (Score != other.Score) return ScoreAscend * (Score - other.Score);
            if (Sale.Price != other.Sale.Price)
                return PriceAscend * (Sale.Price - other.Sale.Price);
            for (int i = 0; i < EveScore.Length; i++)
                if (EveScore[i] != other.EveScore[i])
                    return -1 * (EveScore[i] ? 1 : -1);
            return 0;
        }
    }
}
