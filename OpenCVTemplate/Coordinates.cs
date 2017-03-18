using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCVTemplate
{
    public class Coordinates
    {
        private double _widthPictureBox;
        private double _heightPictureBox;
        private PictureBox _picturBox1;
        private Point _startPoint;

        private SupportGrid _gridDraw = new SupportGrid();


        public Coordinates(double WidthPictureBox, double HeightPictureBox, PictureBox PicturBox1)
        {
            _widthPictureBox = WidthPictureBox;
            _heightPictureBox = HeightPictureBox;
            _picturBox1 = PicturBox1;
        }

        //Масштабирование колесиком мышки
        public void WheelRotation(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                _picturBox1.Width += (int)_widthPictureBox;
                _picturBox1.Height += (int)_heightPictureBox;
            }
            else
            {
                _picturBox1.Width -= (int)_widthPictureBox;
                _picturBox1.Height -= (int)_heightPictureBox;
            }

            _picturBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        //Перемещение изображения
        public void MouseMove(MouseEventArgs e, Point MouseDownLocation)
        {
            _picturBox1.Left = e.X + _picturBox1.Left - MouseDownLocation.X;
            _picturBox1.Top = e.Y + _picturBox1.Top - MouseDownLocation.Y;
        }

        //Установка начальной координаты
        public void MouseDown(MouseEventArgs e, TextBox textBox1, TextBox textBox2)
        {
            _startPoint.X = (int)(e.Location.X * DeltaX());
            _startPoint.Y = (int)(e.Location.Y * DeltaY());
            textBox1.Text = _startPoint.X.ToString();
            textBox2.Text = _startPoint.Y.ToString();
        }

        //Расчет погрешности при изменении масштаба изображения
        public Point SelectPoint(MouseEventArgs e, int Numeric1, int Numeric2)
        {
            Point SelectPointFigure = new Point();

            SelectPointFigure.X = (int)(e.Location.X * DeltaX());
            SelectPointFigure.Y = (int)(e.Location.Y * DeltaY());

            PointF Coordinata = new Point();

            float XDelt = 0 - (float)(_startPoint.X - SelectPointFigure.X) / Numeric1;

            float YDelt = (float)(_startPoint.Y - SelectPointFigure.Y) / Numeric2;

            Coordinata.X = XDelt;
            Coordinata.Y = YDelt;

            MessageBox.Show(Coordinata.ToString());


            return SelectPointFigure;

        }

        //Коэфицент погрешности о координате Х
        public float DeltaX()
        {
            float X = (float)_picturBox1.Image.Width / (float)_picturBox1.Width;

            return X;
        }

        //Коэфицент погрешности о координате Y
        public float DeltaY()
        {
            float Y = (float)_picturBox1.Image.Height / (float)_picturBox1.Height;

            return Y;
        }

        public Point StartPointReturn
        {
            get
            {
                return _startPoint;
            }
            set
            {
                _startPoint = value;
            }
        }
    }
}
