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
        public double stat_obs_x { get; set; }
        public double stat_obs_y { get; set; }
        //public ObstacleObject obs = new ObstacleObject();
        public static InteropData InteropData_ { get; set; }
        List<ObstacleObject> Obstacle_list = new List<ObstacleObject>();
        public InteropData()
        {
            stat_obs_x = 0.0;
            stat_obs_y = 0.0;
        }
        public void pushObject(double x, double y, double r, bool stationary)
        {
            ObstacleObject newObj = new ObstacleObject(x, y, r, stationary);
            Obstacle_list.Add(newObj);
        }

        public void printObjects()
        {
            for(int i = 0; i < Obstacle_list.Count; i++)
            {
                Console.WriteLine("Obstacle x: " + Obstacle_list[i].x);
            }

        }
    }
}
