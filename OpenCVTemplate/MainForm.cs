using System;
using System.Drawing;
using System.Windows.Forms;


namespace OpenCVTemplate
{
    public partial class MainForm : Form
    {
        private Point _mouseDownLocation;
        private bool _selectedPointStart = true;
        private bool _selectedPoint = true;

        private Point _startPoint;

        private InitialСoordinate _coordinatPoint = new InitialСoordinate();
        private SupportGrid _gridDraw = new SupportGrid();
        private Coordinates _coordinatClass;
        private ImageLoad _loadImage;

        public MainForm()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += new MouseEventHandler(this_MouseWheel);
          
        }

        //Загрузка изображения.
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _loadImage = new ImageLoad();

            _loadImage.LoadImage(ofd);

            pictureBox1.Image = _loadImage.ReturnImage();
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            if (_loadImage.ReturnImage() != null)
            {
                _coordinatClass = new Coordinates(pictureBox1.Width / 5, pictureBox1.Image.Height / 5, pictureBox1);
            }
        }

        //Левая кнопка мыши перемещение рисунка. Правая установка начальной координаты и поиск координат после устаноки начальной координаты.
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (!_selectedPointStart)
                {
                    _selectedPointStart = true;
                    _coordinatClass.MouseDown(e, textBox1, textBox2);
                    _startPoint = _coordinatClass.StartPointReturn;
                    button1_Click(sender, e);

                }

                if (!_selectedPoint)
                {
                    _coordinatClass.SelectPoint(e, (int)numericUpDown1.Value, (int)numericUpDown2.Value);
                   
                }
            }
        }

       //Перемещение картинки по форме
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _coordinatClass.MouseMove(e, _mouseDownLocation);
            }
        }

        //Масштабирование картинки
        void this_MouseWheel(object sender, MouseEventArgs e)
        {
            _coordinatClass.WheelRotation(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _gridDraw.GridDrawing(_loadImage.ReturnImage(), pictureBox1, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value, _startPoint, (int)numericUpDown6.Value, (int)numericUpDown7.Value);
           
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _selectedPointStart = false;
            button3.Text = "Начать";
            _selectedPoint = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_selectedPoint == true)
            {
                button3.Text = "Отключить";
                _selectedPoint = false;
            }
            else
            {
                button3.Text = "Начать";
                _selectedPoint = true;
            }
        }

        //Установка начальной координаты автоматически
        private void button4_Click(object sender, EventArgs e)
        {
            if (_coordinatClass != null)
            {
                _coordinatPoint.InitPoint(_loadImage.ReturnImage(), (int)numericUpDown5.Value, pictureBox1);
                _startPoint.X = (int)_coordinatPoint.StartPointReturn().X;
                _startPoint.Y = (int)_coordinatPoint.StartPointReturn().Y;
                
                _coordinatClass.StartPointReturn = _startPoint;
                textBox1.Text = _startPoint.X.ToString();
                textBox2.Text = _startPoint.Y.ToString();

                button1_Click(sender, e);
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}

