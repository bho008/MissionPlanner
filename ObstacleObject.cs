using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPlanner
{
    class ObstacleObject
    {
        public double x = 0.0;
        public double y = 0.0;
        public double radius = 0.0;
        public bool stationary = true;
        public int id = 0;
        public static ObstacleObject obs;
        public ObstacleObject()
        {
            id = 0;
            x = 0.0;
            y = 0.0;
            radius = 0.0;
            stationary = true;
        }
        public ObstacleObject(int i, double lat, double lng, double rad, bool sta)
        {
            id = i;
            x = lat;
            y = lng;
            radius = rad;
            stationary = sta;
        }

        public double getX()
        {
            return x;
        }
        public double getY()
        {
            return y;
        }
        public double getRadius()
        {
            return radius;
        }
    }
}
