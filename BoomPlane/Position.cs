using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoomPlane.Map;

namespace BoomPlane
{
    public enum Direction
    {
        up,
        down,
        left,
        right
    }
    internal record Position(int x, int y)
    {
        public bool IsAdapt => x >= 0 && x < IMap.mapLength && y >= 0 && y < IMap.mapWidth;

        static public Position operator +(Position positon1, Position positon2) =>
            new Position(positon1.x + positon2.x, positon1.y + positon2.y);
    }
}
