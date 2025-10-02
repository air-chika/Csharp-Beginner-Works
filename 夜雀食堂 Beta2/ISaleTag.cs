using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 夜雀食堂_Beta2
{
    public interface ISaleTag
    {
        public bool CmLove(CmCus CusTag);
        List<string> Tags { get; }
    }
}
