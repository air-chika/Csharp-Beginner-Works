using BoomPlane.Map;
using SMPConsole;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomPlane.Build
{
    internal static class ReadBuild
    {
        static int TranRead(char a) => a == '0' ? a + 10 : a;
        static public Position GetPos()
        {
            char fir = Console.ReadKey().KeyChar;
            char sec = Console.ReadKey().KeyChar;
            if (fir.IsNumber() && sec.IsNumber())
                return new(TranRead(fir) - '1', TranRead(sec) - '1');
            if (fir > sec) { char tmp = sec; sec = fir; fir = tmp; } //3A
            if (sec >= 'A' && sec <= 'Z') sec = (char)(sec - 'A' + 'a');
            return new(TranRead(fir) - '1', sec - 'a');
        }


        public static DesignMap map = new();
        static public void DesignPlanes()
        {
            PageBase pageBase = new((_, _) => 3, (_) => 1);

            Line[] help = new Line[] {
                new(3),
                new(new ColorString[]{new("控制方向:  ", ConsoleColor.DarkYellow), new("w s a d  ", ConsoleColor.Yellow) }, 2),
                new(new ColorString[]{new("控制位置:  ", ConsoleColor.DarkYellow), new("↑←↓→  ", ConsoleColor.Yellow) }, 2),
                new(new ColorString[]{ new("放下飞机:  ", ConsoleColor.DarkYellow), new("空格/回车 ", ConsoleColor.Yellow) }, 2),
                new(new ColorString[]{ new("撤回飞机:  ", ConsoleColor.DarkYellow), new("回退      ", ConsoleColor.Yellow)}, 2),
            };

            KeyAction[] keyActions = new KeyAction[] {
                new(() => map.MoveXY(0, 1), ConsoleKey.DownArrow),
                new(() => map.MoveXY(0, -1), ConsoleKey.UpArrow),
                new(() => map.MoveXY(-1, 0), ConsoleKey.LeftArrow),
                new(() => map.MoveXY(1, 0), ConsoleKey.RightArrow),
                new(() => map.SetDirection(Direction.down), ConsoleKey.S),
                new(() => map.SetDirection(Direction.up), ConsoleKey.W),
                new(() => map.SetDirection(Direction.left), ConsoleKey.A),
                new(() => map.SetDirection(Direction.right), ConsoleKey.D),
                new(() => map.AddPattern(), ConsoleKey.Enter,ConsoleKey.Spacebar),
                new(() => map.RemovePattern(), ConsoleKey.Backspace)
            };

            Line[] help2 = new Line[] {
                new(3),
                new(new ColorString[]{new("确认:      ", ConsoleColor.DarkYellow), new("回车", ConsoleColor.Yellow) }, 2),
                new(new ColorString[]{new("撤回:      ", ConsoleColor.DarkYellow), new("回退", ConsoleColor.Yellow) }, 2)
            };

            bool end = true;
            KeyAction[] keyActions2 = new KeyAction[] {
                new(() => end=true, ConsoleKey.Enter),
                new(() => end=false, ConsoleKey.Backspace),
            };


            while (true)
            {
                var lines = map.ToLines().Concat(help);
                ActionPage actionPage = new(lines, keyActions, pageBase);
                actionPage.ShowReadOnce();
                if (map.IsFull)
                {
                    var lines2 = map.ToLines().Concat(help2);
                    ActionPage actionPage2 = new(lines2, keyActions2, pageBase);
                    actionPage2.ShowReadOnce();
                    if (end)
                        return;
                    else
                        map.RemovePattern();
                }
            }

        }
    }
}
