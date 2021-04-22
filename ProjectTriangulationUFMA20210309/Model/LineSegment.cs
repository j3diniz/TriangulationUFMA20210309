using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTriangulationUFMA20210309.Model {
    class LineSegment {

        #region Fields, Properties and Variables
        private PointXY startPoint;
        public PointXY StartPoint {
            get { return startPoint; }
            set { startPoint = value; }
        }

        private PointXY endPoint;
        public PointXY EndPoint {
            get { return endPoint; }
            set { endPoint = value; }
        }

        // Inclination
        private double m;
        public double M {
            get { return m; }
            set { m = value; }
        }

        // y = 0
        private double b;
        public double B {
            get { return b; }
            set { b = value; }
        }

        private double length;
        public double Length {
            get { return length; }
            set { length = value; }
        }


        // Can be deleted? No = True
        private bool persistent;
        public bool Persistent {
            get { return persistent; }
            set { persistent = value; }
        }
        #endregion

        #region Constructors
        public LineSegment(PointXY startPoint, PointXY endPoint) {
            if (startPoint.X < endPoint.X) {
                StartPoint = startPoint;
                EndPoint = endPoint;
            } else if ((startPoint.X == endPoint.X)&&(startPoint.Y < endPoint.Y)) {
                StartPoint = startPoint;
                EndPoint = endPoint;
            } else {
                StartPoint = endPoint;
                EndPoint = startPoint;
            }

            // Case of line parallel to the y-axis
            if (StartPoint.X == EndPoint.X) {
                EndPoint.X = endPoint.X + 1;
            }

            M = (EndPoint.Y - StartPoint.Y) / (EndPoint.X - StartPoint.X);
            B = StartPoint.Y - (M * StartPoint.X);

            // Length
            Length= Math.Sqrt(Math.Pow(EndPoint.Y - StartPoint.Y, 2) + Math.Pow(EndPoint.X - StartPoint.X, 2));

        }
        #endregion

        public double Min(double a, double b) {
            return (a < b) ? a : b;
        }

        public double Max(double a, double b) {
            return (a > b) ? a : b;
        }

    }
}
