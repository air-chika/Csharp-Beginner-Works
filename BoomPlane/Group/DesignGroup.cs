using BoomPlane.Pattern;
using SMPConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BoomPlane.Group
{
    internal class DesignGroup
    {
        public DesignGroup()
        {
            nowPattern = new Plane(new(0, 2), Direction.right);
        }
        public bool IsFull => patterns.Count == 3;

        public void MoveXY(int relaX, int relaY)
        {
            nowPattern.MoveXY(relaX, relaY);
        }

        public void SetDirection(Direction dir)
        {
            nowPattern.SetDirection(dir);
        }

        public bool AddPattern()
        {
            if (nowPattern.IsOverFlow || patterns.Any(x => x.IsConflict(nowPattern))) return false;
            patterns.Add(nowPattern);
            nowPattern = new Plane(new(0, 2), Direction.right);
            return true;
        }

        public void RemovePattern()
        {
            nowPattern = patterns.Last();
            patterns = patterns.SkipLast(1).ToList();
        }

        public bool IsOldHeads(Position positon) => patterns.Any(p => p.IsHead(positon));
        public bool IsOldBodies(Position positon) => patterns.Any(p => p.IsBody(positon));
        public bool IsNewHead(Position positon) => !IsFull && nowPattern.IsHead(positon);
        public bool IsNewBody(Position positon) => !IsFull && nowPattern.IsBody(positon);

        public List<IPattern> patterns = new();
        IPattern nowPattern;

    }
}
