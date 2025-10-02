using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmpForm
{
    public static class SmpControl
    {
        static public void SmpChange(this Button button)
        {
            button.BackColor = SmpColor.棕;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.MouseDownBackColor = button.FlatAppearance.MouseOverBackColor = SmpColor.金;
            button.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }
    }
}
