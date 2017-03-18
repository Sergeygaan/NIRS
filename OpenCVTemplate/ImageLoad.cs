using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OpenCVTemplate
{
    class ImageLoad
    {
        private Image _source;
        private string _fileNameSave;

        //Загрузка изображения
        public void LoadImage(OpenFileDialog ofd)
        {
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _fileNameSave = ofd.FileName;
                Build(ofd.FileName);
            }
        }

       
        private void Build(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            _source = Image.FromFile(fileName);

            ConvertImage(_source);

            //Image result = new Bitmap(250, 250);
            //using (Graphics g = Graphics.FromImage((Image)result))
            //{
            //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    g.DrawImage(source, 0, 0, 250, 250);
            //    g.Dispose();
            //}

            //MessageBox.Show(result.Size.ToString());

            //source = result;
        }
        //Конвертирование изображения под нужное разрешения
        public void ConvertImage(Image Sourse)
        {
            double HeightImage = Sourse.Size.Height;
            double WidthImage = Sourse.Size.Width;
            if ((Sourse.Size.Height * Sourse.Size.Width >= 250000) && (Sourse.Size.Height * Sourse.Size.Width <= 1000000))
            {
                HeightImage = Sourse.Size.Height * 0.6;
                WidthImage = Sourse.Size.Width * 0.6;
            }

            if ((Sourse.Size.Height * Sourse.Size.Width >= 1000000) && (Sourse.Size.Height * Sourse.Size.Width <= 4000000))
            {
                HeightImage = Sourse.Size.Height * 0.2;
                WidthImage = Sourse.Size.Width * 0.2;
            }

            if ((Sourse.Size.Height * Sourse.Size.Width >= 4000000) && (Sourse.Size.Height * Sourse.Size.Width <= 9000000))
            {
                HeightImage = Sourse.Size.Height * 0.1;
                WidthImage = Sourse.Size.Width * 0.1;
            }

            if (Sourse.Size.Height * Sourse.Size.Width >= 9000000)
            {
                HeightImage = Sourse.Size.Height * 0.05;
                WidthImage = Sourse.Size.Width * 0.05;
            }


            Image result = new Bitmap((int)WidthImage, (int)HeightImage);
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(Sourse, 0, 0, (int)WidthImage, (int)HeightImage);
                g.Dispose();
            }

            _source = result;

        }

        public Image ReturnImage()
        {
            return _source;
        }

        //private Bitmap ExtractGraph(Image image)
        //{
        //    //

        //    var method = cbType.SelectedIndex;
        //    //
        //    var img = new Image<Bgr, byte>((Bitmap)image);
        //    var gray = img.Convert<Gray, Byte>();
        //    //smoothed
        //    Image<Gray, byte> gray2 = gray.PyrDown();
        //    gray2 = gray2.PyrUp();

        //    //canny method
        //    if (method == 1)
        //    {
        //        Image<Gray, byte> canny = gray2.Canny(new Gray(param1), new Gray(param1));
        //        return canny.ToBitmap();
        //    }

        //    //AdaptiveThreshold method
        //    if (method == 0)
        //    {
        //        CvInvoke.cvAdaptiveThreshold(gray2, gray2, 255,
        //                                     Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C,
        //                                     Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY, 3, param1/10f);
        //        gray2._ThresholdBinaryInv(new Gray(100), new Gray(255));

        //        //gray2._Erode(1);
        //        //gray2._Dilate(1);

        //        return gray2.ToBitmap();
        //    }

        //    return null;
        //}
    }
}
