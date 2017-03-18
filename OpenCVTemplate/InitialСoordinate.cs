using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenCVTemplate
{
    public class InitialСoordinate
    {
        private AddRectangle _newRecrangle = new AddRectangle();
        private PointF _startPoint;

        //Метод осуществляющий перевод изображения вцерно белый цвет. Выделения контура у изображения и поиск линий на изображении.
        public void InitPoint(Image source, int Numeric, PictureBox PicturBox)
        {
            var img = new Image<Bgr, byte>((Bitmap)source);
            var gray = img.Convert<Gray, Byte>();
            Image<Gray, byte> gray2 = gray.PyrDown();
            gray2 = gray2.PyrUp();

            //Выделение контура
            CvInvoke.cvAdaptiveThreshold(gray2, gray2, 255,
                                         Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C,
                                         Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY, 3, Convert.ToInt32(Numeric) / 10f);
            gray2._ThresholdBinaryInv(new Gray(100), new Gray(255));

            //Выделение всех линий на изображении
            Image<Bgr, byte> res = img.Copy();
            LineSegment2D[] lines =
                gray2
                //.Convert<Gray, byte>()
                //.Canny(new Gray(Numeric), new Gray(Numeric))
                //.HoughLines(wqe, wqe, 1, Math.PI / 16, 1, 10, 1)[0];
                .HoughLinesBinary(1, Math.PI / 16, 1, 10, 1)[0];

            //foreach (LineSegment2D line in lines)
            //{
            //    res.Draw(line, new Bgr(Color.Blue), 1);

            //}

            //Поиск пересечения двух линий
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (LineSegment2D line in lines)
                {
                    PointF Point = IntersectionLine(lines[i].P1, lines[i].P2, line.P1, line.P2);

                    if (Point.X != 0)
                    {
                        _startPoint = Point;
                    }

                }
            }

            PicturBox.Image = res.ToBitmap();
        }

        //Определяет имеют ли 2 линии точку пересечению
        public PointF IntersectionLine(PointF start1, PointF end1, PointF start2, PointF end2)
        {
            PointF out_intersection = new PointF();
            PointF dir1 = new PointF();
            dir1.X = end1.X - start1.X;
            dir1.Y = end1.Y - start1.Y;
            PointF dir2 = new PointF();
            dir2.X = end2.X - start2.X;
            dir2.Y = end2.Y - start2.Y;

            //считаем уравнения прямых проходящих через отрезки
            float a1 = -dir1.Y;
            float b1 = +dir1.X;
            float d1 = -(a1 * start1.X + b1 * start1.Y);

            float a2 = -dir2.Y;
            float b2 = +dir2.X;
            float d2 = -(a2 * start2.X + b2 * start2.Y);

            //подставляем концы отрезков, для выяснения в каких полуплоскотях они
            float seg1_line2_start = a2 * start1.X + b2 * start1.Y + d2;
            float seg1_line2_end = a2 * end1.X + b2 * end1.Y + d2;

            float seg2_line1_start = a1 * start2.X + b1 * start2.Y + d1;
            float seg2_line1_end = a1 * end2.X + b1 * end2.Y + d1;

            //если концы одного отрезка имеют один знак, значит он в одной полуплоскости и пересечения нет.
            if (seg1_line2_start * seg1_line2_end >= 0 || seg2_line1_start * seg2_line1_end >= 0)
                return out_intersection;

            float u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);
            out_intersection.X = start1.X + u * dir1.X;
            out_intersection.Y = start1.Y + u * dir1.Y;

            return out_intersection;
        }


        public PointF StartPointReturn()
        {
            return _startPoint;
        }
    }
}
