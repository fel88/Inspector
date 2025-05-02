using System.Drawing;

namespace Inspector
{
    public interface IUIElement
    {
        PointF Position { get; set; }
        void Draw(DrawingContext ctx);
        bool ContainsPoint(PointF p);
    }
}


