using BoomPlane.Group;
using BoomPlane.Pattern;
using SMPConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomPlane.Map
{
    enum DesignState
    {
        nil,
        oldBody,
        oldHead,
        newBody,
        newHead
    }

    internal class DesignMap : IMap
    {
        readonly int[][] map; //[宽][长]
        DesignGroup designGroup = new();
        public DesignMap()
        {
            map = new int[mapWidth][];
            for (int i = 0; i < mapWidth; i++)
                map[i] = new int[mapLength];
            LoadMap();
        }

        void LoadMap()
        {
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapLength; j++)
                {
                    DesignState state = DesignState.nil;
                    Position p = new(j, i);
                    if (designGroup.IsOldHeads(p)) state = DesignState.oldHead;
                    else
                        if (designGroup.IsOldBodies(p)) state = DesignState.oldBody;
                    if (designGroup.IsNewHead(p)) state = DesignState.newHead;
                    else
                        if (designGroup.IsNewBody(p)) state = DesignState.newBody;
                    map[i][j] = (int)state;
                }
        }

        public void MoveXY(int relaX, int relaY)
        {
            designGroup.MoveXY(relaX, relaY);
            LoadMap();
        }
        public void SetDirection(Direction dir)
        {
            designGroup.SetDirection(dir);
            LoadMap();
        }
        public bool AddPattern()
        {
            if (!designGroup.AddPattern()) return false;
            LoadMap();
            return true;
        }
        public void RemovePattern()
        {
            designGroup.RemovePattern();
            LoadMap();
        }

        public bool IsFull => designGroup.patterns.Count == 3;

        public byte[] ToBytes()
        {
            return designGroup.patterns.SelectMany(p => p.ToBytes()).ToArray();
        }

        protected override int[][] Map => map;

        protected override string[] SitStr => sitStr;

        protected override ConsoleColor[] SitClr => sitClr;

        readonly static string[] sitStr = new string[] { "·", "□", "■", "□", "■" };
        readonly static ConsoleColor[] sitClr = new ConsoleColor[] { ConsoleColor.DarkGray, ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Blue, ConsoleColor.DarkBlue };

    }
}
