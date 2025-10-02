using BoomPlane.AI;
using BoomPlane.Build;
using BoomPlane.Group;
using BoomPlane.Map;
using BoomPlane.Pattern;
using SMPConsole;
using System.Runtime.InteropServices;

namespace BoomPlane
{
    internal class Program
    {
        static PageBase pageBase = new((_, _) => 3, (_) => 1);


        static void Main(string[] args)
        {
            Line l1 = new(new ColorString[] { new("1.进入网络对战(1,2可互连)", ConsoleColor.Green) }, 2);
            Line l2 = new(new ColorString[] { new("2.进入带提示的网络对战", ConsoleColor.Green) }, 2);
            Line l3 = new(new ColorString[] { new("3.进入AI自动对战", ConsoleColor.Green) }, 2);
            Line[] lines = new Line[] { l1, l2, l3 };
            Page.Show(lines, pageBase);
            Console.Write("   ");
            switch (Console.ReadLine())
            {
                case "1":
                    NetPlay();
                    break;

                case "2":
                    HelpNetPlay();
                    break;

                case "3":
                    AIPlay();
                    break;
                default:
                    break;
            }
        }

        static void AIPlay()
        {
            Console.CursorVisible = false;
            Console.WriteLine("\n   AI模型构建中...");
            BestPosition guessai = new();
            BestPosition enemyai = new();
            Console.Clear();
            Console.WriteLine("\n   AI模型构建完成,按任意键继续");
            Console.ReadKey();
            /*            ReadBuild.DesignPlanes();
                        NetBuild.LoadQueue(ReadBuild.map.ToBytes());*///手动选机
            PatternGroup group1 = new(RdmBuild.GetRandomPlane);
            GuessMap guessMap1 = new(group1);
            //PatternGroup group2 = new(NetBuild.GetBytePlane);//手动选机
            PatternGroup group2 = new(RdmBuild.GetRandomPlane);
            EnemyMap guessMap2 = new(group2);

            Line own = new(new ColorString[] { new("我方已选择轰炸位置,按任意键继续", ConsoleColor.Green) }, 2);
            Line enemy = new(new ColorString[] { new("敌方已选择轰炸位置,按任意键继续", ConsoleColor.Red) }, 1);

            while (!guessMap1.Win && !guessMap2.Win)
            {
                var lines = guessMap2.ToLines().Append(new Line(3)).Concat(guessMap1.ToLines()).Append(new Line(1));

                Position q1 = guessMap1.GuessPositon(() => guessai.getNextStep());
                guessai.Input(q1, (mapType)guessMap1.GetState(q1));
                Page.Show(lines.Append(own), pageBase);
                Console.ReadKey();

                Position q2 = guessMap2.GuessPositon(() => enemyai.getNextStep());
                enemyai.Input(q2, guessMap2.TranAiType((int)guessMap2.GetState(q2)));
                Page.Show(lines.Append(enemy), pageBase);
                Console.ReadKey();
            }
            var lines2 = guessMap2.ToLines().Append(new Line(3)).Concat(guessMap1.ToLines());
            Page page2 = new(lines2, pageBase);

            page2.Show();
            if (guessMap1.Win)
                if (guessMap2.Win)
                    Console.WriteLine("   游戏结束,平局");
                else
                    Console.WriteLine("   游戏结束,我方胜利");
            else
                Console.WriteLine("   游戏结束,敌方胜利");
            Console.ReadLine();
        }


        static void HelpNetPlay()
        {
            BestPosition? guessai = null;
            var tas = Task.Run(() => guessai = new());
            Console.CursorVisible = true;
            string[] strings = { "1 建立主机   2 连接主机" };
            Line line = new(strings.Select(x => new ColorString(x, ConsoleColor.Yellow)), 1);
            Page.Show(line.ToIE(), pageBase);
            Console.Write("   ");
            int Hoster = int.Parse(Console.ReadLine() ?? "0");

            string[] iphelp = { "输入IP:" };
            Line line2 = new(iphelp.Select(x => new ColorString(x, ConsoleColor.Yellow)), 1);
            Page.Show(line2.ToIE(), pageBase);
            Console.Write("   ");
            string ip = Console.ReadLine() ?? "10.20.30.23:46318";

            Console.CursorVisible = false;

            if (Hoster == 1)
                NetBuild.Host(ip);
            else
                NetBuild.Join(ip);

            ReadBuild.DesignPlanes();
            NetBuild.LoadQueue(ReadBuild.map.ToBytes());
            PatternGroup group2 = new(NetBuild.GetBytePlane);
            EnemyMap enemyMap = new(group2);

            NetBuild.SendOwnGroup();
            NetBuild.ReceiveEnemyGroup();
            PatternGroup group1 = new(NetBuild.GetBytePlane);
            GuessMap guessMap = new(group1);

            Console.WriteLine("\n   AI模型构建中...");
            tas.Wait();
            Console.Clear();
            Console.WriteLine("\n   AI模型构建完成,按任意键继续");
            Console.ReadKey();

            Line own = new(new ColorString[] { new("我方正在选择轰炸位置——", ConsoleColor.Green) }, 2);
            Line own2 = new(new ColorString[] { new("输入(数字,字母)选择位置", ConsoleColor.Green) }, 2);
            Line enemy = new(new ColorString[] { new("对方正在选择轰炸位置", ConsoleColor.Red) }, 2);

            while (!guessMap.Win && !enemyMap.Win)
            {
                var lines = enemyMap.ToLines().Append(new Line(3)).Concat(guessMap.ToLines()).Append(new Line(1));

                if (Hoster == 1)
                {
                    var pos = guessai.getNextStep();
                    Line own3 = new(new ColorString[] { new($"AI建议下一步选择:({pos.x + 1},{(char)(pos.y + 'a')})", ConsoleColor.Green) }, 1);
                    Page.Show(lines.Append(own).Append(own2).Append(own3), pageBase);
                    var p = guessMap.GuessPositon(ReadBuild.GetPos);
                    guessai.Input(p, (mapType)guessMap.GetState(p));
                    NetBuild.SendOwnPos(p);

                    Page.Show(lines.Append(enemy), pageBase);
                    enemyMap.GuessPositon(NetBuild.ReceiveEnemyPos);
                }
                else
                {
                    Page.Show(lines.Append(enemy), pageBase);
                    enemyMap.GuessPositon(NetBuild.ReceiveEnemyPos);

                    var pos = guessai.getNextStep();
                    Line own3 = new(new ColorString[] { new($"AI建议下一步选择:({pos.x + 1},{(char)(pos.y + 'a')})", ConsoleColor.Green) }, 1);
                    Page.Show(lines.Append(own).Append(own2).Append(own3), pageBase);
                    var p = guessMap.GuessPositon(ReadBuild.GetPos);
                    guessai.Input(p, (mapType)guessMap.GetState(p));
                    NetBuild.SendOwnPos(p);
                }
            }
            var lines2 = enemyMap.ToLines().Append(new Line(3)).Concat(guessMap.ToLines());
            Page page2 = new(lines2, pageBase);
            page2.Show();
            if (guessMap.Win)
                if (enemyMap.Win)
                    Console.WriteLine("   游戏结束,平局");
                else
                    Console.WriteLine("   游戏结束,我方胜利");
            else
                Console.WriteLine("   游戏结束,敌方胜利");
            Console.ReadLine();
        }

        static void NetPlay()
        {
            string[] strings = { "1 建立主机   2 连接主机" };
            Line line = new(strings.Select(x => new ColorString(x, ConsoleColor.Yellow)), 1);
            Page.Show(line.ToIE(), pageBase);
            Console.Write("   ");
            int Hoster = int.Parse(Console.ReadLine() ?? "0");

            string[] iphelp = { "输入IP:" };
            Line line2 = new(iphelp.Select(x => new ColorString(x, ConsoleColor.Yellow)), 1);
            Page.Show(line2.ToIE(), pageBase);
            Console.Write("   ");
            string ip = Console.ReadLine() ?? "10.20.30.23:46318";

            Console.CursorVisible = false;

            if (Hoster == 1)
                NetBuild.Host(ip);
            else
                NetBuild.Join(ip);

            ReadBuild.DesignPlanes();
            NetBuild.LoadQueue(ReadBuild.map.ToBytes());
            PatternGroup group2 = new(NetBuild.GetBytePlane);
            EnemyMap enemyMap = new(group2);

            NetBuild.SendOwnGroup();
            NetBuild.ReceiveEnemyGroup();
            PatternGroup group1 = new(NetBuild.GetBytePlane);
            GuessMap guessMap = new(group1);

            Line own = new(new ColorString[] { new("我方正在选择轰炸位置——", ConsoleColor.Green) }, 2);
            Line own2 = new(new ColorString[] { new("输入(数字,字母)选择位置", ConsoleColor.Green) }, 1);
            Line enemy = new(new ColorString[] { new("对方正在选择轰炸位置", ConsoleColor.Red) }, 1);

            while (!guessMap.Win && !enemyMap.Win)
            {
                var lines = enemyMap.ToLines().Append(new Line(3)).Concat(guessMap.ToLines()).Append(new Line(1));

                if (Hoster == 1)
                {
                    Page.Show(lines.Append(own).Append(own2), pageBase);
                    var p = guessMap.GuessPositon(ReadBuild.GetPos);
                    NetBuild.SendOwnPos(p);

                    Page.Show(lines.Append(enemy), pageBase);
                    enemyMap.GuessPositon(NetBuild.ReceiveEnemyPos);
                }
                else
                {
                    Page.Show(lines.Append(enemy), pageBase);
                    enemyMap.GuessPositon(NetBuild.ReceiveEnemyPos);

                    Page.Show(lines.Append(own).Append(own2), pageBase);
                    var p = guessMap.GuessPositon(ReadBuild.GetPos);
                    NetBuild.SendOwnPos(p);
                }
            }
            var lines2 = enemyMap.ToLines().Append(new Line(3)).Concat(guessMap.ToLines());
            Page page2 = new(lines2, pageBase);
            page2.Show();
            if (guessMap.Win)
                if (enemyMap.Win)
                    Console.WriteLine("   游戏结束,平局");
                else
                    Console.WriteLine("   游戏结束,我方胜利");
            else
                Console.WriteLine("   游戏结束,敌方胜利");
            Console.ReadLine();
        }
    }
}