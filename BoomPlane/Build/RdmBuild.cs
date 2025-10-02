using BoomPlane.Map;
using BoomPlane.Pattern;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomPlane.Build
{
    static internal class RdmBuild
    {
        public static Random random = new(((int)DateAndTime.Now.Ticks));
        //public static Random random = new(333);

        public static Position GetRandomPos()
        {
            return new(random.Next(IMap.mapLength), random.Next(IMap.mapWidth));
        }

        public static Plane GetRandomPlane()
        {
            return new(GetRandomPos(), (Direction)random.Next(4));
        }

    }
}
