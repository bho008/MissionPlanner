﻿using System;
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
    }
}
