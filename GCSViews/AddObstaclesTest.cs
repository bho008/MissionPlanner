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
        public AddObstaclesTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //33.9737° N, 117.3281° W
            PointLatLng p1 = new PointLatLng(33.9737, -117.3281);
            MissionPlanner.GCSViews.FlightData.ObstaclesOverlay.Markers.Add(new MarkerObstacle(p1));
        }
    }
}
