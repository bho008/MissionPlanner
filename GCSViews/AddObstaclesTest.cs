using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MissionPlanner.Utilities;

namespace MissionPlanner.GCSViews
{
    public partial class AddObstaclesTest : Form
    {
        public double radius = 200.0;
        bool start = false;
        public AddObstaclesTest()
        {
            InitializeComponent();
        }

        private void drawObstacles()
        {
            //InteropData.InteropData_READER.
            while (start)
            {
                Console.WriteLine("hello");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //33.9737° N, 117.3281° W
            double.TryParse(textBox1.Text, out radius);
            Console.WriteLine("radius: " + radius);
            PointLatLng p1 = new PointLatLng(33.9737, -117.3281); // create latitude longitude object 
            MarkerObstacle UCR_Obstacle = new MarkerObstacle(p1, radius); //create new MarkerObstacle, Marker Obstacle is in Utilities Folder
            UCR_Obstacle.ToolTipText = "UCR Test Obstacle"; //Give Marker Obstacle a Name
            UCR_Obstacle.ToolTipMode = MarkerTooltipMode.OnMouseOver; //Enable text to show on mouse hover over
            MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataMoving.Markers.Add(UCR_Obstacle); //add Marker obstacle to Overlay ObstaclesOverlay
                                                                                               //MissionPlanner.GCSViews.FlightPlanner.ObstaclesOverlayPlanner.Markers.Add(UCR_Obstacle); //add Marker obstacle to Overlay ObstaclesOverlay


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!start)
            {
                start = true;
                drawObstacles();
            }
            else start = false;
        }
    }
}
