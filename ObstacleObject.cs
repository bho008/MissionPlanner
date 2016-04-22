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
        public ObstacleObject(double lat, double lng, double rad, bool sta)
        {
            x = lat;
            y = lng;
            radius = rad;
            stationary = sta;
        }
    }
}
