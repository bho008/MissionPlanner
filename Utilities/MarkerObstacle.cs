using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MissionPlanner.Utilities
{
    class MarkerObstacle : GMapMarker
    {
        public Pen Pen = new Pen(Brushes.White, 2);
        public double Radius = 0.0;
        public Color Color
        {
            get { return Pen.Color; }
            set
            {
                if (!initcolor.HasValue) initcolor = value;
                Pen.Color = value;
            }
        }

        Color? initcolor = null;

        public GMapMarker InnerMarker;

        // m
        public int wprad = 5559;

        public void ResetColor()
        {
            if (initcolor.HasValue)
                Color = initcolor.Value;
            else
                Color = Color.White;
        }
        public MarkerObstacle(PointLatLng p, double radius) : base(p)
        {
            Pen.DashStyle = DashStyle.Dash;

            // do not forget set Size of the marker
            // if so, you shall have no event on it ;}
            Size = new System.Drawing.Size(50, 50);
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
            Radius = radius;
        }
        public override void OnRender(Graphics g)
        {
            base.OnRender(g);

            if (wprad == 0 || Overlay.Control == null)
                return;

            // if we have drawn it, then keep that color
            if (!initcolor.HasValue)
                Color = Color.White;

            //wprad = 300;

            // undo autochange in mouse over
            //if (Pen.Color == Color.Blue)
            //  Pen.Color = Color.White;

            double width =
                (Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
                    Overlay.Control.FromLocalToLatLng(Overlay.Control.Width, 0)) * 1000);
            double height =
                (Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
                    Overlay.Control.FromLocalToLatLng(Overlay.Control.Height, 0)) * 1000);
            double m2pixelwidth = Overlay.Control.Width / width;
            double m2pixelheight = Overlay.Control.Height / height;

            GPoint loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * Radius)), LocalPosition.Y);
            // MainMap.FromLatLngToLocal(wpradposition);


            int x = LocalPosition.X - Offset.X - (int)(Math.Abs(loc.X - LocalPosition.X) / 2);
            int y = LocalPosition.Y - Offset.Y - (int)Math.Abs(loc.X - LocalPosition.X) / 2;
            int widtharc = (int)Math.Abs(loc.X - LocalPosition.X);
            int heightarc = (int)Math.Abs(loc.X - LocalPosition.X);
            //Console.WriteLine("width {0}, height {1}, m2pixelwidth{2}, m2pixelheight{3}, x{4}, y{5}, widtharc{6}, heightarc{7}", width, height, m2pixelwidth, m2pixelheight, x, y, widtharc, heightarc);
            //Console.WriteLine("hello world");
            if (widtharc > 0 && Overlay.Control.Zoom > 3)
            {
                g.DrawArc(Pen, new System.Drawing.Rectangle(x, y, widtharc, heightarc), 0, 360);
                /*
                g.FillPie(new SolidBrush(Color.FromArgb(75, Color.Blue)), x, y, widtharc, heightarc, 0, 360);
                g.DrawString(Overlay.Control.FromLocalToLatLng(0, 0).ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, -50));

                g.DrawString(Overlay.Control.FromLocalToLatLng(Overlay.Control.Width, 0).ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 0));
                g.DrawString(width.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 50));
                g.DrawString(height.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 100));
                g.DrawString(m2pixelwidth.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 150));
                g.DrawString(m2pixelheight.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 200));
                g.DrawString(x.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 250));
                g.DrawString(y.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 300));
                g.DrawString(widtharc.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 350));
                g.DrawString(heightarc.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.AliceBlue, new Point(100, 400));

                g.DrawEllipse(Pens.Black, LocalPosition.X, LocalPosition.Y, (float)Radius, (float)Radius);
                */
            }
        }
    }
}
