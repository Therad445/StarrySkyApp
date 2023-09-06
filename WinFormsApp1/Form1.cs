using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StarrySkyApp
{
    public partial class Form1 : Form
    {
        private List<Star> stars = new List<Star>();
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += Form1_Paint;

            // Таймер для обновления анимации
            Timer timer = new Timer();
            timer.Interval = 30; // Обновление каждые 30 миллисекунд
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Вероятность появления новой звезды
            if (random.NextDouble() < 0.02)
            {
                int y = random.Next(0, this.Height - 1);
                int brightness = random.Next(50, 256);
                stars.Add(new Star(0, y, brightness));
            }

            List<Star> newStars = new List<Star>();
            foreach (var star in stars)
            {
                if (star.X < this.Width)
                {
                    star.X += 1;
                    newStars.Add(star);
                }
            }
            stars = newStars;

            this.Invalidate(); // Перерисовать форму
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var star in stars)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(star.Brightness, star.Brightness, star.Brightness)))
                {
                    e.Graphics.FillRectangle(brush, star.X, star.Y, 1, 1);
                }
            }
        }
    }

    public class Star
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Brightness { get; set; }

        public Star(int x, int y, int brightness)
        {
            X = x;
            Y = y;
            Brightness = brightness;
        }
    }
}
