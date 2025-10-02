using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoomPlane.Pattern
{


    internal class Plane : IPattern
    {
        public Plane(Position positon, Direction dir)
        {
            head = new(positon.x, positon.y);
            foreach (var p in relatives)
                body.Add(TransPos(p, dir));
            direction = dir;
        }
        Position head;
        List<Position> body = new();
        Direction direction;

        public bool IsConflict(IPattern plane) => plane.IsHit(head) || body.Any(x => plane.IsHit(x));
        public bool IsOverFlow { get => body.Any(p => !p.IsAdapt); }

        public bool IsHead(Position positon) => head == positon;
        public bool IsBody(Position positon) => body.Contains(positon);
        public bool IsHit(Position positon) => IsHead(positon) || IsBody(positon);

        Position TransPos(Position relative, Direction direction)
        {
            switch (direction)
            {
                case Direction.up:
                    return head + relative;

                case Direction.down:
                    return head + new Position(-relative.x, -relative.y);

                case Direction.left:
                    return head + new Position(relative.y, -relative.x);

                case Direction.right:
                    return head + new Position(-relative.y, relative.x);

                default:
                    return head + relative;
            }
        }

        public void MoveXY(int relaX, int relaY)
        {
            Position rela = new(relaX, relaY);
            head += rela;
            for (int i = 0; i < body.Count; i++)
                body[i] += rela;
        }

        public void SetDirection(Direction dir)
        {
            body.Clear();
            foreach (var p in relatives)
                body.Add(TransPos(p, dir));
            direction = dir;
        }

        readonly static Position[] relatives = { new(0, -1), new(0, -2), new(0, -3),
            new(1, -1), new(2, -1), new(-1, -1), new(-2, -1),
            new(1, -3), new(-1, -3) };
        public byte[] ToBytes()
        {
            return new byte[3] { (byte)head.x, (byte)head.y, (byte)direction };
        }
    }

}
