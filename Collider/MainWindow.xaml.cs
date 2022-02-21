using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer frame;

        public MainWindow()
        {
            InitializeComponent();
            frame = new DispatcherTimer();
            
        }
        void Log(string title, object content)
        {
            Console.WriteLine($"[{title}] >> {content}");
        }
        void setObject(ObjRect obj)
        {
            obj.Opacity = 0.5;
            obj.WIDTH = Width - obj.Width;
            obj.HEIGHT = Height - obj.Height;
            obj.Start_Frmae();
        }

        bool isOverlap(ObjRect a, ObjRect b)
        {
            Vector[] a_cld = a.Colliders;
            Vector[] b_cld = b.Colliders;

            if (a_cld[1].X < b_cld[0].X || a_cld[0].X > b_cld[1].X)
            {
                return false;
            }
            if (a_cld[1].Y < b_cld[0].Y || a_cld[0].Y > b_cld[1].Y)
            { 
                return false; 
            }

            return true;
        }

        Vector abs(Vector v)
        {
            if (v.X < 0)
            {
                v.X *= -1;
            }
            if (v.Y < 0)
            {
                v.Y *= -1;
            }
            return v;
        }

        Vector getDirection(Vector v)
        {
            Vector result = new Vector(0, 0);
            if(v.X > 0)
            {
                result.X = 1;
            }
            else if(v.X < 0)
            {
                result.X = -1;
            }
            if(v.Y > 0)
            {
                result.Y = 1;
            }
            else if(v.Y < 0)
            {
                result.Y = -1;
            }
            return result;
        }

        bool Crush(ObjRect a, ObjRect b)
        {
            if(isOverlap(a, b))
            {
                Vector[] b_Ek = new Vector[] { b.Ek, getDirection(b.V) };
                b.setEk(a.Ek, getDirection(a.V));
                a.setEk(b_Ek[0], b_Ek[1]);
                return true;
            }
            return false;
        }

        void Frame_Tick(object sender, EventArgs e)
        {
            Crush(obj1, obj2);
            Crush(obj1, obj3);
            Crush(obj1, obj4);
            Crush(obj1, obj5);
            Crush(obj2, obj3);
            Crush(obj2, obj4);
            Crush(obj2, obj5);
            Crush(obj3, obj4);
            Crush(obj3, obj5);
            Crush(obj4, obj5);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setObject(obj1);
            setObject(obj2);
            setObject(obj3);
            setObject(obj4);
            setObject(obj5);
            frame.Tick += new EventHandler(Frame_Tick);
            frame.Interval = TimeSpan.FromMilliseconds(1);
            frame.Start();
        }
    }
}
