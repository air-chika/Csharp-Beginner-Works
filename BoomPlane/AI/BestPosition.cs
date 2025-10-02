using BoomPlane.AI;
using BoomPlane.Map;
using BoomPlane.Pattern;
using SMPConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BoomPlane.AI
{
    enum mapType
    {
        unknown,
        empty,
        plane,
        planeHead,
    };


    class BestPosition
    {
        static readonly int wid = IMap.mapWidth;
        static readonly int wide = wid + 1;
        static readonly int pNum = 3;

        class Node
        {
            public mapType[][] map = SMPTool.GetNew(wid + 1, wid + 1, mapType.empty);
            public long Hash()
            {
                long M = 10000000000037, p = 107, ret = 1;
                for (int i = 1; i <= wid; i++)
                    for (int j = 1; j <= wid; j++)
                        if (map[i][j] == mapType.planeHead || map[i][j] == mapType.plane)
                            ret = ret * (i * wid + j) % M * p % M;
                return ret;
            }
        }

        mapType[][] a = SMPTool.GetNew(wide, wide, mapType.empty);
        readonly int[][] upPlane = SMPTool.GetNew(10, 2, 0, +1, -2, +1, -1, +1, 0, +1, +1, +1, +2, +2, 0, +3, -1, +3, 0, +3, +1);
        readonly int[][] downPlane = SMPTool.GetNew(10, 2, 0, -1, -2, -1, -1, -1, 0, -1, +1, -1, +2, -2, 0, -3, -1, -3, 0, -3, +1);
        readonly int[][] leftPlane = SMPTool.GetNew(10, 2, 0, -2, +1, -1, +1, 0, +1, +1, +1, +2, +1, 0, +2, -1, +3, 0, +3, +1, +3);
        readonly int[][] rightPlane = SMPTool.GetNew(10, 2, 0, -2, -1, -1, -1, 0, -1, +1, -1, +2, -1, 0, -2, -1, -3, 0, -3, +1, -3);

        static mapType[][] mapCopy(mapType[][] types) => types.Select(x => x.Select(x2 => x2).ToArray()).ToArray();

        void dfs(int nowPNum, List<Node> VN)
        {
            if (nowPNum > pNum)
            {
                Node Node = new();
                Node.map = mapCopy(a);
                VN.Add(Node);
                return;
            }
            var b = mapCopy(a);
            for (int dir = 0; dir < 4; dir++)   // 枚举飞机朝向
                for (int i = 1; i <= wid; i++)
                    for (int j = 1; j <= wid; j++)
                    {  // 枚举机头位置
                        a = mapCopy(b);   // 首先初始化数组a
                        bool flag = true;
                        if (a[i][j] != mapType.empty) continue;
                        a[i][j] = mapType.planeHead;
                        for (int k = 0; k < 9; k++)
                        {   // 枚举机身位置
                            int ii, jj;
                            if (dir == 0)
                            {
                                ii = i + upPlane[k][0];
                                jj = j + upPlane[k][1];
                            }
                            else if (dir == 1)
                            {
                                ii = i + downPlane[k][0];
                                jj = j + downPlane[k][1];
                            }
                            else if (dir == 2)
                            {
                                ii = i + leftPlane[k][0];
                                jj = j + leftPlane[k][1];
                            }
                            else
                            {
                                ii = i + rightPlane[k][0];
                                jj = j + rightPlane[k][1];
                            }
                            if (ii < 1 || ii > wid || jj < 1 || jj > wid)
                            {
                                flag = false;
                                break;
                            };
                            if (a[ii][jj] != mapType.empty)
                            {
                                flag = false;
                                break;
                            };
                            a[ii][jj] = mapType.plane;
                        }
                        if (flag) dfs(nowPNum + 1, VN);
                    }
        }

        List<Node> initNodes(mapType[][]? nmap = null)
        {
            /**
             * 给出所有可能的摆放方式（已去重）
             */
            if (nmap == null)
            {
                for (int i = 1; i <= wid; i++)
                    for (int j = 1; j <= wid; j++)
                        a[i][j] = mapType.empty;
            }
            else
            {
                for (int i = 1; i <= wid; i++)
                    for (int j = 1; j <= wid; j++)
                        a[i][j] = nmap[i][j];
            }

            List<Node> temp = new(), ret = new();
            dfs(1, temp);
            List<long> s = new();   // 用于去重
            foreach (Node x in temp)
            {
                long h = x.Hash();
                if (!s.Contains(h))
                {
                    ret.Add(x);
                    s.Add(h);
                }
            }
            return ret;
        }

        void elimination(int x, int y, mapType m)
        {
            List<Node> temp = new(nowNodes.Capacity);
            foreach (Node t in nowNodes) temp.Add(t);
            nowNodes.Clear();
            foreach (Node t in temp)
            {
                if (t.map[x][y] == m) nowNodes.Add(t);
            }
        }

        public BestPosition(mapType[][]? nmap = null)
        {
            nowNodes = initNodes(nmap);
            nowMap = SMPTool.GetNew(wide, wide, mapType.unknown);
            if (nmap != null)
                for (int i = 1; i <= wid; i++)
                    for (int j = 1; j <= wid; j++)
                        nowMap[i][j] = nmap[i][j];
        }

        List<Node> nowNodes; //count为可能个数
        mapType[][] nowMap;
        public int tot = 0;//击毁飞机的总数

        public Position getNextStep()
        {
            if (nowNodes.Count == 1)
            {
                var xx = nowNodes.First();
                for (int i = 1; i <= wid; i++)
                    for (int j = 1; j <= wid; j++)
                        if (xx.map[i][j] == mapType.planeHead && nowMap[i][j] == mapType.unknown)
                            return new(i - 1, j - 1);
            }
            int ii = 0, jj = 0, maxEarn = 0;
            for (int i = 1; i <= wid; i++)
                for (int j = 1; j <= wid; j++)
                    if (nowMap[i][j] == mapType.unknown)
                    {  // 枚举可以选的位置
                       // 首先计算当前位置各种情况的频率
                        int p1 = 0, p2 = 0, p3 = 0;
                        foreach (Node x in nowNodes)
                        {
                            if (x.map[i][j] == mapType.planeHead) p2++;
                            if (x.map[i][j] == mapType.plane) p1++;
                            if (x.map[i][j] == mapType.empty) p3++;
                        }
                        int earn = p3 * (p1 + p2) + p2 * (p1 + p3) + p1 * (p2 + p3);
                        if (earn > maxEarn)
                        {
                            ii = i;
                            jj = j;
                            maxEarn = earn;
                        }
                    }
            return new(ii - 1, jj - 1);
        }

        public void Input(Position pos, mapType res)
        {
            nowMap[pos.x + 1][pos.y + 1] = res;
            elimination(pos.x + 1, pos.y + 1, res);
            if (res == mapType.planeHead) tot++;
        }


    }
};