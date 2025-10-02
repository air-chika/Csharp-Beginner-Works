using Google.Protobuf;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{

    public interface IDeepCopy<T>
    {
        T GetDeepCopy();
        T DeepCopy { get; }
    }

    public partial class ColorString : IDeepCopy<ColorString>
    {
        public ColorString(string str, ConsoleColor color)
        {
            this.Str = new(str);
            this.Color = color;
        }
        static public IEnumerable<ColorString> SameColorStrings(ConsoleColor color, params string[] strs)
        {
            return strs.Select(str => new ColorString(str, color));
        }
        public virtual ColorString GetDeepCopy() => new(new(Str), Color);
        public virtual ColorString DeepCopy { get => GetDeepCopy(); }
        public ColorString SameColorCopy(string str) => new(str, Color);

        public string Str { get; protected set; }
        public ConsoleColor Color { get; set; }


        public virtual void Write()
        {
            Console.ForegroundColor = Color;
            Console.Write(Str);
        }

        public override string ToString() => Str;
        public static ConsoleColor defaultFore = ConsoleColor.Yellow;
        public static implicit operator ColorString(string str) => new(str, defaultFore);
    }



  


}
