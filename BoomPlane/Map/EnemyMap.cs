using BoomPlane.AI;
using BoomPlane.Group;
using SMPConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomPlane.Map
{
    enum EnemyState
    {
        nil,
        body,
        head,
        empty,
        hitBody,
        hitHead
    }
    internal class EnemyMap : IMap
    {
        public mapType[][] ToAImap()
        {
            mapType[][] ints = SMPTool.GetNew(mapLength + 1, mapWidth + 1, mapType.unknown);
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapLength; j++)
                    ints[j + 1][i + 1] = TranAiType(map[i][j]);
            return ints;
        }

         public mapType TranAiType(int x)
        {
            return x switch
            {
                0 => 0,
                1 => 0,
                2 => 0,
                3 => (mapType)1,
                4 => (mapType)2,
                5 => (mapType)3,
                _ => 0,
            };
        }

        public EnemyMap(PatternGroup planes)
        {
            this.planes = planes;
            map = new int[mapWidth][];
            for (int i = 0; i < mapWidth; i++)
            {
                map[i] = new int[mapLength];
                for (int j = 0; j < mapLength; j++)
                {
                    if (planes.IsBodies(new(j, i)))
                    {
                        map[i][j] = (int)EnemyState.body;
                        continue;
                    }
                    if (planes.IsHeads(new(j, i))) map[i][j] = (int)EnemyState.head;
                }
            }
        }

        public Position GuessPositon(Func<Position> FGetPositon)
        {
            var p = FGetPositon();
            while (!p.IsAdapt || !Unknown(p)) p = FGetPositon();
            if (planes.IsBodies(p))
            {
                SetState(p, EnemyState.hitBody);
                return p;
            }
            if (planes.IsHeads(p))
            {
                SetState(p, EnemyState.hitHead);
                deadPlaneNum++;
                return p;
            }
            SetState(p, EnemyState.empty);
            return p;
        }

        int[][] map; //[宽][长]
        public EnemyState GetState(Position point) => (EnemyState)map[point.y][point.x];
        void SetState(Position point, EnemyState situation) => map[point.y][point.x] = (int)situation;

        bool Unknown(Position point) => GetState(point) == EnemyState.nil || GetState(point) == EnemyState.body || GetState(point) == EnemyState.head;

        PatternGroup planes;
        int deadPlaneNum = 0;
        public bool Win { get => deadPlaneNum == 3; }

        protected override int[][] Map => map;

        protected override string[] SitStr => sitStr;

        protected override ConsoleColor[] SitClr => sitClr;

        readonly static string[] sitStr = new string[] { "·", "□", "■", "×", "□", "■" };
        readonly static ConsoleColor[] sitClr = new ConsoleColor[] { ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Red, ConsoleColor.DarkRed };
    }
}
