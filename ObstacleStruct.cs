using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner
{
    public struct ObstacleStruct : ICloneable
    {
        private double _x;

        public double x
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        private double _y;
        public double y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        private double _radius;
        public double radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
            }
        }
        private bool _stationary;
        public bool stationary
        {
            get
            {
                return _stationary;
            }
            set
            {
                _stationary = value;
            }
        }
        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private double _height;
        public double height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        public ObstacleStruct(int i, double lat, double lng, double rad, double h, bool sta) : this()
        {
            id = i;
            x = lat;
            y = lng;
            radius = rad;
            height = h;
            stationary = sta;
        }

        public object Clone()
        {
            ObstacleStruct newObstacleStruct = (ObstacleStruct)this.MemberwiseClone();
            newObstacleStruct.x = this.x;
            newObstacleStruct.y = this.y;
            newObstacleStruct.id = this.id;
            newObstacleStruct.height = this.height;
            newObstacleStruct.radius = this.radius;
            newObstacleStruct.stationary = this.stationary;
            return newObstacleStruct;
        }
    }
}
