using GMap.NET;
using GMap.NET.WindowsForms;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPlanner
{
    public class InteropData
    {
        //public InteropData testData = new InteropData();
        //public ObstacleObject obs = new ObstacleObject();
        public static InteropData InteropData_moving = new InteropData();
        public static InteropData InteropData_stationary = new InteropData();
        public static InteropData InteropData_READER = new InteropData();
        static List<ObstacleStruct> Obstacle_list_moving = new List<ObstacleStruct>();
        static List<ObstacleStruct> Obstacle_list_stationary = new List<ObstacleStruct>();
        static List<ObstacleStruct> Obstacle_list_reader = new List<ObstacleStruct>();
        static List<ObstacleStruct> Obstacle_list_stationary_final = new List<ObstacleStruct>();
        public static bool printStationary = true;
        public static bool printDirectedLines = true;
        public double shortestDistanceToObs = 99999;

        public static bool drawBuffer = true;

        double bufferDistance = 250; // buffer distance of 100ft
        double currBearing = 0.0;
        double currSpeed = 0.0;
        double currAlt = 0.0;
        double currLat = 0.0;
        double currLng = 0.0;

        public static bool breached = false;
        public static int breachedId = -1;
        public static bool enableSDA = false;
        double R = 6378137;

        public InteropData()
        {
            //Obstacle_list = null;
            //Obstacle_list_reader = null;
            //InteropData_ = null;
            //InteropData_READER = null;
        }

        public void pushObjectStationary(int id, double x, double y, double r, double h, bool stationary)
        {
            if (stationary && printStationary)
            {
                if (id == 0 && Obstacle_list_stationary.Count > 0)
                {
                    Obstacle_list_stationary_final = new List<ObstacleStruct>();
                    for (int i = 0; i < Obstacle_list_stationary.Count; i++)
                    {
                        Obstacle_list_reader.Add(Obstacle_list_stationary[i]);
                        //Obstacle_list_stationary_final.Add(Obstacle_list_stationary[i]);
                        //Obstacle_list_stationary_final = new List<ObstacleStruct>(Obstacle_list_reader);

                        Obstacle_list_stationary_final.Add((ObstacleStruct)(Obstacle_list_reader[i].Clone()));


                        //Console.WriteLine(Obstacle_list_stationary_final[i].x);
                    }
                    if (printStationary)
                    {
                        MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataStationary.Markers.Clear();
                        printObjects();
                        printStationary = false;
                        //Obstacle_list_stationary_final = new List<ObstacleStruct>(Obstacle_list_reader);
                    }
                    //Obstacle_list_stationary.Clear();
                    drawLinesStationaryObs(MainV2.comPort.MAV.cs.Location);


                }
                if (printStationary)
                {
                    //ObstacleObject newObj = new ObstacleObject(id, x, y, r, h, stationary);
                    var newObjStruct = new ObstacleStruct(id, x, y, r, h, stationary);


                    Obstacle_list_stationary.Add(newObjStruct);
                    //Console.WriteLine("obstacle stationary list count: " + Obstacle_list_stationary.Count);

                }
            }
        }

        public void pushObjectMoving(int id, double x, double y, double r, double h, bool stationary)
        {
            if (id == 0 && Obstacle_list_moving.Count > 0)
            {
                for (int i = 0; i < Obstacle_list_moving.Count; i++)
                {
                    Obstacle_list_reader.Add(Obstacle_list_moving.ElementAt(i));
                }
                printObjects();
                Obstacle_list_moving.Clear();
            }
            ObstacleStruct newObjStruct = new ObstacleStruct(id, x, y, r, h, stationary);
            Obstacle_list_moving.Add(newObjStruct);
            var currentPos = getCurrentMavLocation();

            //Console.WriteLine(currentPos.Lat + " " + currentPos.Lng + " " + currentPos.Alt);
        }

        public void printObjects()
        {
            MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataMoving.Clear();
            MissionPlanner.GCSViews.FlightData.InteropFun.Polygons.Clear();

            for (int i = 0; i < Obstacle_list_reader.Count; i++)
            {
                //Console.WriteLine("x: " + Obstacle_list_reader[i].x + "\ty: " + Obstacle_list_reader[i].y + " h: " + Obstacle_list_reader[i].height);
                //ObstacleObject obs = Obstacle_list_reader.ElementAt(i);
                ObstacleStruct obs = Obstacle_list_reader.ElementAt(i);

                PointLatLng p1 = new PointLatLng(obs.x, obs.y); // create latitude longitude object 
                MarkerObstacle UCR_Obstacle = new MarkerObstacle(p1, obs.radius, Color.Green); //create new MarkerObstacle, Marker Obstacle is in Utilities Folder
                MarkerObstacle UCR_Obstacle_buffer = new MarkerObstacle(p1, (obs.radius + bufferDistance / 2) / 2, Color.Beige);
                UCR_Obstacle.ToolTipText = "UCR Test Obstacle"; //Give Marker Obstacle a Name
                UCR_Obstacle.ToolTipMode = MarkerTooltipMode.OnMouseOver; //Enable text to show on mouse hover over  
                drawLines(p1, MainV2.comPort.MAV.cs.Location);
                drawLinesStationaryObs(MainV2.comPort.MAV.cs.Location);
                if (obs.stationary)
                {
                    UCR_Obstacle.Color = Color.Blue;
                    if (drawBuffer)
                    {
                        MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataStationary.Markers.Add(UCR_Obstacle_buffer);
                    }
                    MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataStationary.Markers.Add(UCR_Obstacle);

                }
                if (drawBuffer)
                {
                    MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataMoving.Markers.Add(UCR_Obstacle_buffer);
                }
                UCR_Obstacle.Color = Color.Blue;
                MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataMoving.Markers.Add(UCR_Obstacle);

            }
            Obstacle_list_reader.Clear();
        }

        public void drawLinesStationaryObs(PointLatLng mavPos)
        {
            //Console.WriteLine("obstacle stationary list count: " + Obstacle_list_stationary_final.Count);
            MissionPlanner.GCSViews.FlightData.interopPolygonOverlay.Polygons.Clear();
            breachedId = -1;
            breached = false;
            for (int i = 0; i < Obstacle_list_stationary_final.Count; i++)
            {
                //ObstacleObject obs = Obstacle_list_stationary_final.ElementAt(i);
                ObstacleStruct obs = Obstacle_list_stationary_final.ElementAt(i);

                List<PointLatLng> polygonPoints = new List<PointLatLng>();
                var point = new PointLatLng(obs.x, obs.y);
                //Console.WriteLine(Obstacle_list_stationary_final[i].x);
                polygonPoints.Add(point);

                polygonPoints.Add(mavPos);

                GMapPolygon lineStat = new GMapPolygon(polygonPoints, "dist from obs");
                //Console.Write(lineStat.Distance * 3280.84 + "\t"); //distance calculated in feet
                if (lineStat.Distance * 3280.84 - obs.radius < bufferDistance)
                {

                    //Console.WriteLine(lineStat.Distance * 3280.84 - obs.radius + "\t");

                    breached = true;
                    breachedId = i;
                    currBearing = MainV2.comPort.MAV.cs.nav_bearing;
                    currLat = mavPos.Lat;
                    currLng = mavPos.Lng;
                    currAlt = MainV2.comPort.MAV.cs.alt / 3.28084;
                    Console.WriteLine(currAlt);
                    if (enableSDA)
                    {
                        Locationwp testWP = new Locationwp(); //initialize waypoint to nav to
                        testWP.id = (byte)MAVLink.MAV_CMD.WAYPOINT; //set testWP id to WAYPOINT
                        if ((currBearing > 0 && currBearing < 90) || (currBearing > 270 && currLng < point.Lng))
                        {

                            //new_latitude  = latitude  + (dy / r_earth) * (180 / pi);
                            //new_longitude = longitude + (dx / r_earth) * (180 / pi) / cos(latitude * pi / 180);
                            testWP.lat = point.Lat;
                            testWP.lng = point.Lng + (bufferDistance / R) * (180 / Math.PI) / Math.Cos(point.Lat * Math.PI / 180);
                            testWP.alt = (float)currAlt;
                        }
                        else if ((currBearing > 180 && currBearing <= 360) || (currBearing < 90 && currLng > point.Lng))
                        {
                            testWP.lat = point.Lat;
                            testWP.lng = point.Lng - (bufferDistance / R) * (180 / Math.PI) / Math.Cos(point.Lat * Math.PI / 180);
                            testWP.alt = (float)currAlt;
                        }

                        MainV2.comPort.setGuidedModeWP(testWP);


                    }
                }
                if (printDirectedLines)
                {
                    MissionPlanner.GCSViews.FlightData.interopPolygonOverlay.Polygons.Add(lineStat);
                }
            }
        }

        public void drawLines(PointLatLng point, PointLatLng mavPos)
        {
            List<PointLatLng> polygonPoints = new List<PointLatLng>();
            polygonPoints.Add(point);
            polygonPoints.Add(mavPos);
            GMapPolygon line = new GMapPolygon(polygonPoints, "dist from obs");
            line.Stroke = new Pen(Color.PapayaWhip);
            if (printDirectedLines)
            {
                MissionPlanner.GCSViews.FlightData.InteropFun.Polygons.Add(line);
            }
        }

        public PointLatLngAlt getCurrentMavLocation()
        {
            //PointLatLngAlt currLoc = new PointLatLngAlt(MainV2.comPort.MAV.cs.Location);
            return MainV2.comPort.MAV.cs.Location;
        }
    }
}
