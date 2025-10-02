using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmpForm
{
    public static class Photo
    {
        static public bool SamePhoto(string filea, string fileb)
        {
            Bitmap a = new(1, 1), b = new(1, 1);
            try
            {
                a = new Bitmap(filea);
                b = new Bitmap(fileb);
            }
            catch
            {
                a.Dispose();
                b.Dispose();
                return false;
            }
            try
            {
                if (a.Width != b.Width) return false;
                if (a.Height != b.Height) return false;
                for (int i = 0 ; i < a.Width ; i++)
                {
                    if (a.GetPixel(i, 0) != b.GetPixel(i, 0)) return false;
                }
                return true;
            }
            finally
            {
                a.Dispose();
                b.Dispose();
            }
        }


    }
}
