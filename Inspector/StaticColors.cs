using System.Drawing;
using System.Drawing.Drawing2D;

namespace Inspector
{
    public static class StaticColors
    {
        public static Brush ConvBrush = new SolidBrush(Color.FromArgb(51, 85, 136));
        
        //public static Brush GroupBrush = new HatchBrush(HatchStyle.DarkVertical, Color.LightBlue, Color.LightYellow);
        public static Brush GroupBrush = new SolidBrush( Color.LightBlue);
    }
}
