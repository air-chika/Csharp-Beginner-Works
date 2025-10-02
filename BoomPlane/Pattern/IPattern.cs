using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomPlane.Pattern
{
    internal interface IPattern
    {
        bool IsHead(Position positon);
        bool IsBody(Position positon);
        bool IsHit(Position positon);
        bool IsConflict(IPattern positon);
        bool IsOverFlow { get; }
        void MoveXY(int relaX, int relaY);
        void SetDirection(Direction dir);
        byte[] ToBytes();
    }
}
