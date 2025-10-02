using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoomPlane.Pattern;

namespace BoomPlane.Group
{
    internal class PatternGroup
    {
        public PatternGroup(Func<IPattern> FGetPlane)
        {
            while (!IsFull)
                TryAddPlane(FGetPlane());
        }

        bool TryAddPlane(IPattern plane)
        {
            if (plane.IsOverFlow || patterns.Any(p => p.IsConflict(plane)))
                return false;
            patterns.Add(plane);
            return true;
        }
        bool IsFull { get => patterns.Count == 3; }

        public bool IsHeads(Position positon) => patterns.Any(p => p.IsHead(positon));
        public bool IsBodies(Position positon) => patterns.Any(p => p.IsBody(positon));

        public List<IPattern> patterns { get; } = new();

    }
}
