using System.Drawing;

namespace OpenCVTemplate
{
    class AddRectangle
    {
        public Rectangle ShowRectangle(PointF Start, PointF End)
        {

            int _left = (int)((Start.X - End.X > 0) ? End.X : Start.X);
            int _down = (int)((Start.Y - End.Y > 0) ? Start.Y : End.Y);
            int _top = (int)((Start.Y - End.Y > 0) ? End.Y : Start.Y);
            int _right = (int)((Start.X - End.X > 0) ? Start.X : End.X);

            Rectangle rect = Rectangle.FromLTRB(_left, _top, _right, _down);

            return rect;
        }
    }
}
