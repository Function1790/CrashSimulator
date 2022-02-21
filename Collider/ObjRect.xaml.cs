using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Collider
{
    /// <summary>
    /// ObjRect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ObjRect : UserControl
    {
        DispatcherTimer frame = new DispatcherTimer();
        Vector velocity = new Vector(0, 0);
        double mass = 1;
        
        public double WIDTH = 1000;
        public double HEIGHT = 1000;

        void Log(string title, object content)
        {
            Console.WriteLine($"[{title}] >> {content}");
        }

        public ObjRect()
        {
            InitializeComponent();
        }

        public double M
        {
            get
            {
                return mass;
            }
            set
            {
                mass = value;
            }
        }
        
        public Vector V
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        public Color BG
        {
            set
            {
                rect.Fill = new SolidColorBrush(value);
            }
        }

        public Vector Collider_Min
        {
            get
            {
                return new Vector(Margin.Left, Margin.Top);
            }
        }
        
        public Vector Collider_Max
        {
            get
            {
                return new Vector(Margin.Left + Width, Margin.Top + Height);
            }
        }

        public Vector[] Colliders
        {
            get
            {
                return new Vector[] { Collider_Min, Collider_Max };
            }
        }

        public void Frame_Tick(object sender, EventArgs e)
        {
            Move();
        }

        public Vector Ek
        {
            get
            {
                double m2 = (0.5) * M;
                return new Vector(V.X * V.X * m2, V.Y * V.Y * m2); 
            }
        }

        public void setEk(Vector ek, Vector direction)
        {
            double m2 = mass * (0.5);
            V = new Vector(Math.Sqrt(ek.X / m2) * direction.X, Math.Sqrt(ek.Y / m2) * direction.Y);
        }

        public void Move()
        {
             double x = Margin.Left + V.X;
             double y = Margin.Top + V.Y;

            if (x > WIDTH)
            {
                x = WIDTH;
                velocity.X *= -1;
            }
            else if (x < 0)
            {
                x = 0;
                velocity.X *= -1;
            }

            if (y > HEIGHT)
            {
                y = HEIGHT;
                velocity.Y *= -1;
            }
            else  if (y < 0)
            {
                y = 0;
                velocity.Y *= -1;
            }

            Margin = new Thickness(x, y, 0, 0);
        }

        public void MoveTo(Vector pos)
        {
            Margin = new Thickness(pos.X, pos.Y, 0, 0);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            frame.Tick += new EventHandler(Frame_Tick);
            frame.Interval = TimeSpan.FromMilliseconds(1);
        }

        public void Start_Frmae() { frame.Start(); }
    }
}
