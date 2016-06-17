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
using System.IO;
using System.Text.RegularExpressions;

namespace MissionPlanner.GCSViews
{
    public partial class AddObstaclesTest : Form
    {
        private System.Windows.Forms.DataGridView Commands;
        private System.Windows.Forms.DataGridViewComboBoxColumn Command;



        public double radius = 200.0;
        bool start = false;
        public AddObstaclesTest()
        {
            InitializeComponent();
        }

        private void drawObstacles()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //33.9737° N, 117.3281° W
            double.TryParse(textBox1.Text, out radius);
            Console.WriteLine("radius: " + radius);
            PointLatLng p1 = new PointLatLng(33.9737, -117.3281); // create latitude longitude object 
            MarkerObstacle UCR_Obstacle = new MarkerObstacle(p1, radius, Color.Blue); //create new MarkerObstacle, Marker Obstacle is in Utilities Folder
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

        private void buttonSDAEnable_Click(object sender, EventArgs e)
        {
            if (!InteropData.enableSDA)
            {
                InteropData.enableSDA = true;
                buttonSDAEnable.BackColor = Color.Green;
                Console.WriteLine("SDA Enabled");
            }
            else
            {
                InteropData.enableSDA = false;
                buttonSDAEnable.BackColor = Color.Red;
                Console.WriteLine("SDA Disabled");
            }
        }

        private void testUploadWP_Click(object sender, EventArgs e)
        {
            //MainV2.comPort.MAV.
            MAVLinkInterface port = MainV2.comPort;
            MainV2.comPort.giveComport = true;

            MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
            bool use_int = (MainV2.comPort.MAV.cs.capabilities & MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;
            if (use_int)
            {
                Console.WriteLine("use ints");
            }
            Locationwp testWP = new Locationwp();
            testWP.id = (byte)MAVLink.MAV_CMD.WAYPOINT;
            int a = 0;
            //testWP.id = (byte)Commands.Rows[a].Cells[Command.Index].Tag;

            testWP.Set(33.7789, -117.193504, 100, (byte)MAVLink.MAV_CMD.WAYPOINT); //altitude in m
            /*
            var ans = MainV2.comPort.setWP(testWP, (ushort)1, frame, 0, 1, use_int);
            Console.WriteLine(ans.ToString());
            if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
            {
                wpUploadStatus.Text = "wp accepted";
            }
            */
            MainV2.comPort.setGuidedModeWP(testWP);

            MainV2.comPort.giveComport = false;

        }

        private void drawBufferCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (InteropData.drawBuffer)
            {
                InteropData.drawBuffer = false;
                InteropData.printStationary = true;

            }
            else
            {
                InteropData.drawBuffer = true;
                InteropData.printStationary = true;

            }
        }
        bool eggDropEnabled = false;

        private void buttonEnableEggdrop_Click(object sender, EventArgs e)
        {
            if (!eggDropEnabled)
            {
                buttonEnableEggdrop.BackColor = Color.Green;
                eggDropEnabled = true;
            }
            else
            {
                buttonEnableEggdrop.BackColor = Color.Red;
                eggDropEnabled = false;
            }

        }

        private void drawLinesCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (InteropData.printDirectedLines)
            {
                InteropData.printDirectedLines = false;
            }
            else InteropData.printDirectedLines = true;
        }

        bool drawBoundaries = true;
        private void drawMissionBounderies_Click(object sender, EventArgs e)
        {
            if (drawBoundaries)
            {
                List<PointLatLng> polygonPointsOPArea = new List<PointLatLng>();
                List<PointLatLng> polygonPointsSearchArea = new List<PointLatLng>();
                string latString, lonString;
                float latFloat, lonFloat;
                foreach (string line in File.ReadLines(@"GCSViews/mission_items.txt", Encoding.UTF8))
                {
                    // process the line

                    //A1,38.14626944,-76.42816389
                    Console.WriteLine(line);
                    if (line[0] == 'A')
                    {
                        //Console.WriteLine(line.IndexOf(","));
                        string temp = line.Substring(line.IndexOf(",") + 1);
                        latString = Regex.Match(temp, @"\d+(\.\d+)?").Value;
                        latFloat = float.Parse(latString);
                        //Console.WriteLine(temp);

                        temp = temp.Substring(temp.IndexOf(",") + 1);
                        //lonString = Regex.Match(temp, @"[\-\+]+\d+(\.\d+)?").Value;
                        lonFloat = float.Parse(temp);
                        //Console.WriteLine(temp);

                        //Console.WriteLine(latFloat.ToString() + " " + lonFloat.ToString() +  ";");
                        var point = new PointLatLng(latFloat, lonFloat);
                        polygonPointsOPArea.Add(point);
                    }

                    //S1,38.14315556,-76.43388056

                    if (line[0] == 'S')
                    {
                        string temp = line.Substring(line.IndexOf(",") + 1);
                        latString = Regex.Match(temp, @"\d+(\.\d+)?").Value;
                        latFloat = float.Parse(latString);

                        temp = temp.Substring(temp.IndexOf(",") + 1);
                        lonFloat = float.Parse(temp);

                        var point = new PointLatLng(latFloat, lonFloat);
                        polygonPointsSearchArea.Add(point);
                    }

                }

                GMapPolygon opBoundary = new GMapPolygon(polygonPointsOPArea, "operational area");
                //homeroute.Stroke = new Pen(Color.Yellow, 2);

                opBoundary.Stroke = new Pen(Color.Red, 4);
                opBoundary.Fill = new SolidBrush(Color.FromArgb(30, Color.Blue));
                MissionPlanner.GCSViews.FlightData.CompetitionOverlayOPArea.Polygons.Add(opBoundary);
                MissionPlanner.GCSViews.FlightPlanner.CompetitionOverlay.Polygons.Add(opBoundary);

                GMapPolygon searchArea = new GMapPolygon(polygonPointsSearchArea, "search area");
                searchArea.Stroke = new Pen(Color.Green, 2);
                searchArea.Fill = new SolidBrush(Color.FromArgb(30, Color.White));
                MissionPlanner.GCSViews.FlightData.CompetitionOverlaySearchArea.Polygons.Add(searchArea);
                MissionPlanner.GCSViews.FlightPlanner.CompetitionOverlay.Polygons.Add(searchArea);

                drawBoundaries = false;
            }
        }
    }
}
