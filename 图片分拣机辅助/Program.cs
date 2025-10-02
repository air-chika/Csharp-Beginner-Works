namespace 图片分拣机辅助
{
    internal static class Program
    {

        static public readonly string spath = @"E:\琥珀\VS project 2022\夜雀食堂 Alpha1.1\图片分拣机\bin\Debug\net6.0-windows\cache";

        static void Main()
        {
            var xs = Directory.GetDirectories(@"E:\AIM1").SelectMany(x => Directory.GetFiles(x));
            var ys = Directory.GetFiles(spath);
            foreach (var x in xs)
            {
                var cache = spath + "\\" + Path.GetFileName(x);
                if (ys.Contains(cache) && SmpForm.Photo.SamePhoto(x, cache))
                    File.Delete(cache);
            }

        }
    }
}