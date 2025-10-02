using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BoomPlane.Map;
using BoomPlane.Pattern;
using System.Diagnostics;
using SMPConsole;

namespace BoomPlane.Build
{
    internal static class NetBuild
    {
        static Queue<byte> qbytes = new();
        static public void LoadQueue(byte[] bs)
        {
            foreach (var b in bs)
                qbytes.Enqueue(b);
        }

        static public Plane GetBytePlane()
        {
            int[] infos = new int[] { (int)NetBuild.qbytes.Dequeue(), (int)NetBuild.qbytes.Dequeue(), (int)NetBuild.qbytes.Dequeue() };
            return new(new(infos[0], infos[1]), (Direction)infos[2]);
            
        }

        static Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static public bool Host(string address)
        {
            var adds = address.Split(':', 2);
            try
            {
                socket.Bind(new IPEndPoint(IPAddress.Parse(adds[0]), int.Parse(adds[1])));
            }
            catch (SocketException)
            {
                SmpError.Exit("无效地址");
                return false;
            }
            socket.Listen(1);
            socket = socket.Accept();
            return true;
        }

        static public bool Join(string address)
        {
            var adds = address.Split(':', 2);
            try
            {
                socket.Connect(new IPEndPoint(IPAddress.Parse(adds[0]), int.Parse(adds[1])));
            }
            catch (Exception)
            {
                Console.WriteLine("无效地址");
                Console.ReadLine();
                return false;
            }
            return true;
        }

        static public Position ReceiveEnemyPos()
        {
            byte[] buffer = new byte[1024];
            socket.Receive(buffer, 2, SocketFlags.None);
            return new((int)buffer[0], (int)buffer[1]);
        }

        static public void SendOwnPos(Position pos)
        {
            byte[] buffer = new byte[2] { (byte)pos.x, (byte)pos.y };
            socket.Send(buffer);
        }
        static public void SendOwnGroup()
        {
            socket.Send(ReadBuild.map.ToBytes());
        }
        static public void ReceiveEnemyGroup()
        {
            byte[] bs = new byte[1024];
            socket.Receive(bs);
            LoadQueue(bs);
        }

    }
}
