using System.Drawing;
using System.Windows.Forms;

namespace OpenCVTemplate
{
    public class SupportGrid
    {
        private AddRectangle _newRecrangle = new AddRectangle();
        private int _thicknessLine = 5;

        //Строит и выводит вспомогательную сетку на изображение
        public void GridDrawing(Image source, PictureBox pictureBox1, int Numeric1, int Numeric2, int Numeric3, int Numeric4, Point StartPoint, int Numeric6, int Numeric7)
        {
            if (source != null)
            {
                pictureBox1.Image = (Image)source.Clone();

                Graphics g = Graphics.FromImage(pictureBox1.Image);

                int Width = Numeric1;
                int Height = Numeric2;
                int stepWidth = Numeric3;
                int stepHeight = Numeric4;

                int i = -pictureBox1.Width;

                int StepWidth = 0;
                int AngleY = Numeric6;

                while (i < pictureBox1.Width)
                {
                    StepWidth = i * Width + stepWidth + StartPoint.X;

                    g.DrawLine(new Pen(Brushes.Red, 1), new Point(StepWidth + AngleY, 0), new Point(StepWidth - AngleY, source.Height));

                    i++;
                }

                int j = -pictureBox1.Height;

                int StepHeight = 0;
                int AngleX = Numeric7;

                while (j + Height < pictureBox1.Height)
                {
                    StepHeight = j * Height + stepHeight + StartPoint.Y;

                    g.DrawLine(new Pen(Brushes.Red, 1), new Point(0, StepHeight + AngleX), new Point(source.Width, StepHeight - AngleX));

                    j++;
                }

                Point LastPoint = new Point();
                LastPoint.X = StartPoint.X - _thicknessLine;
                LastPoint.Y = StartPoint.Y - _thicknessLine;
                Point NextPoint = new Point();
                NextPoint.X = StartPoint.X + _thicknessLine;
                NextPoint.Y = StartPoint.Y + _thicknessLine;
                g.DrawEllipse(new Pen(Color.Green, 1), _newRecrangle.ShowRectangle(LastPoint, NextPoint));

            }

        }
    }
}
