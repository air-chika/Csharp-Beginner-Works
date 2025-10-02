using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BoomPlane.AI;
using BoomPlane.Group;
using SMPConsole;

namespace BoomPlane.Map
{

    enum GuessState
    {
        nil,
        empty,
        body,
        head
    }

    internal class GuessMap : IMap
    {

        public mapType[][] ToAImap()
        {
            mapType[][] ints = SMPTool.GetNew(mapLength + 1, mapWidth + 1, mapType.unknown);
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapLength; j++)
                    ints[j + 1][i + 1] = (mapType)map[i][j];
            return ints;
        }

        public GuessMap(PatternGroup planes)
        {
            this.planes = planes;
            map = new int[mapWidth][];
            for (int i = 0; i < mapWidth; i++)
                map[i] = new int[mapLength];
        }

        public Position GuessPositon(Func<Position> FGetPositon)
        {
            var p = FGetPositon();
            while (!p.IsAdapt || GetState(p) != GuessState.nil) p = FGetPositon();
            if (planes.IsBodies(p))
            {
                SetState(p, GuessState.body);
                return p;
            }
            if (planes.IsHeads(p))
            {
                SetState(p, GuessState.head);
                deadPlaneNum++;
                return p;
            }
            SetState(p, GuessState.empty);
            return p;
        }

        readonly int[][] map; //[宽][长]
        public GuessState GetState(Position point) => (GuessState)map[point.y][point.x];
        void SetState(Position point, GuessState situation) => map[point.y][point.x] = (int)situation;

        PatternGroup planes;
        int deadPlaneNum = 0;
        public bool Win { get => deadPlaneNum == 3; }

        protected override int[][] Map => map;

        protected override string[] SitStr => sitStr;

        protected override ConsoleColor[] SitClr => sitClr;

        readonly static string[] sitStr = new string[] { "·", "×", "□", "■" };
        readonly static ConsoleColor[] sitClr = new ConsoleColor[] { ConsoleColor.DarkGray, ConsoleColor.DarkGray, ConsoleColor.Red, ConsoleColor.DarkRed };

    }
}
