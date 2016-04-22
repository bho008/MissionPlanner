using GMap.NET;
using GMap.NET.WindowsForms;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
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
        List<ObstacleObject> Obstacle_list_moving = new List<ObstacleObject>();
        List<ObstacleObject> Obstacle_list_stationary = new List<ObstacleObject>();
        List<ObstacleObject> Obstacle_list_reader = new List<ObstacleObject>();
        private bool printStationary = true;
        public InteropData()
        {
            //Obstacle_list = null;
            //Obstacle_list_reader = null;
            //InteropData_ = null;
            //InteropData_READER = null;
        }

        public void pushObjectStationary(int id, double x, double y, double r, bool stationary)
        {
            if (stationary && printStationary)
            {
                if(id == 0 && Obstacle_list_stationary.Count > 0)
                {
                    for(int i = 0; i < Obstacle_list_stationary.Count; i++)
                    {
                        Obstacle_list_reader.Add(Obstacle_list_stationary[i]);
                    }
                    printObjects();
                    printStationary = false;
                    //Obstacle_list_stationary.Clear();
                }
                ObstacleObject newObj = new ObstacleObject(id, x, y, r, stationary);
                Obstacle_list_stationary.Add(newObj);
            }
        }

        public void pushObjectMoving(int id, double x, double y, double r, bool stationary)
        {
            if (id == 0 && Obstacle_list_moving.Count > 0)
            {
                for(int i = 0; i < Obstacle_list_moving.Count; i++)
                {
                    Obstacle_list_reader.Add(Obstacle_list_moving.ElementAt(i));
                }
                printObjects();
                Obstacle_list_moving.Clear();
            }
            ObstacleObject newObj = new ObstacleObject(id, x, y, r, stationary);
            Obstacle_list_moving.Add(newObj);
        }

        public void printObjects()
        {
            MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataMoving.Clear();
            for (int i = 0; i < Obstacle_list_reader.Count; i++)
            {
                Console.WriteLine("x: " + Obstacle_list_reader[i].x + "\ty: " + Obstacle_list_reader[i].y );
                ObstacleObject obs = Obstacle_list_reader.ElementAt(i);
                PointLatLng p1 = new PointLatLng(obs.getX(), obs.getY()); // create latitude longitude object 
                MarkerObstacle UCR_Obstacle = new MarkerObstacle(p1, obs.getRadius()); //create new MarkerObstacle, Marker Obstacle is in Utilities Folder
                UCR_Obstacle.ToolTipText = "UCR Test Obstacle"; //Give Marker Obstacle a Name
                UCR_Obstacle.ToolTipMode = MarkerTooltipMode.OnMouseOver; //Enable text to show on mouse hover over
                if (obs.stationary)
                {
                    MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataStationary.Markers.Add(UCR_Obstacle);
                }
                MissionPlanner.GCSViews.FlightData.ObstaclesOverlayDataMoving.Markers.Add(UCR_Obstacle);
            }
            Obstacle_list_reader.Clear();
            
        }
    }
}
