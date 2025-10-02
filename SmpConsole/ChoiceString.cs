using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SMPConsole
{
    partial class ColorString
    {
        public virtual void SetKey(ref int index) { }
        public virtual bool RunKey(ConsoleKey ck) => false;

    }

    public class ChoiceString : ColorString, IDeepCopy<ChoiceString>
    {
        public ChoiceString(string content, ConsoleColor color, Action action, params ConsoleKey[]? keys) : base(content, color)
        {
            this.Action = action;
            this.Keys = keys != null ? keys.ToList() : new();
        }
        public ChoiceString(ColorString content, Action action, params ConsoleKey[]? keys) : base(content.Str, content.Color)
        {
            this.Action = action;
            this.Keys = keys != null ? keys.ToList() : new();
        }
        public override ChoiceString GetDeepCopy() => new(new(Str), Color, Action, Keys.ToArray());
        public override ChoiceString DeepCopy { get => GetDeepCopy(); }

        public void AddKeys(params ConsoleKey[] keys)
        {
            this.Keys.AddRange(keys);
        }
        public override void SetKey(ref int index)
        {
            Keys.Add(ChoiceKeys[index]);
            Str = ChoiceKeyChars[index++].ToString() + Str;
        }
        public override bool RunKey(ConsoleKey ck)
        {
            if (Keys.Contains(ck))
            {
                Action();
                return true;
            }
            return false;
        }

        public List<ConsoleKey> Keys { get; }
        public Action Action { get; }

        //最大35个
        static public ConsoleKey[] ChoiceKeys = Enumerable.Range('1', 9).Concat(Enumerable.Range('A', 26)).Select(x => (ConsoleKey)x).ToArray();
        static public string[] ChoiceKeyChars = Enumerable.Range('1', 9).Concat(Enumerable.Range('A', 26)).Select(x => ((char)x).ToString() + '.').ToArray();

    }

    partial class IELine
    {
        static public int SetChoiceKey(this IEnumerable<Line> lines, int index)
        {
            foreach (var line in lines)
                index = line.SetChoiceKey(index);
            return index;
        }

    }

}
