using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTriangulationUFMA20210309.Model {
    class IntersectionPoint {
        #region Fields, Properties and Variables

        // Second X Point of Both Lines
        private double XSecond;

        // Penult X Point of Both Lines
        private double XPenult;

        // Second Y Point of Both Lines
        private double YSecond;

        // Penult Y Point of Both Lines
        private double YPenult;

        // Line T
        private LineSegment lineT;
        public LineSegment LineT {
            get { return lineT; }
            set { lineT = value; }
        }

        // Line V
        private LineSegment lineV;
        public LineSegment LineV {
            get { return lineV; }
            set { lineV = value; }
        }

        // Intersection Points
        private PointXY pointOfIntersection;
        public PointXY PointOfIntersection {
            get { return pointOfIntersection; }
            set { pointOfIntersection = value; }
        }

        // Intersection?
        private bool intersection;
        public bool Intersection {
            get { return intersection; }
            set { intersection = value; }
        }

        #endregion

        #region Constructors
        public IntersectionPoint(LineSegment lineT, LineSegment lineV) {

            LineT = lineT;
            LineV = lineV;

            // Intersection Points
            PointOfIntersection = new PointXY(0,0);
            PointOfIntersection.X = (lineV.B - lineT.B) / (lineT.M - lineV.M);
            PointOfIntersection.Y = (lineT.M * PointOfIntersection.X) + lineT.B;

            // Second and Penult
            List<double> pointsX = new List<double> { lineT.StartPoint.X, lineT.EndPoint.X, lineV.StartPoint.X, lineV.EndPoint.X };
            var pointsXinOrder = pointsX.OrderBy(x => x).ToList();

            List<double> pointsY = new List<double> { lineT.StartPoint.Y, lineT.EndPoint.Y, lineV.StartPoint.Y, lineV.EndPoint.Y };
            var pointsYinOrder = pointsY.OrderBy(y => y).ToList();

            XSecond = pointsXinOrder[1];
            YSecond = pointsYinOrder[1];
            XPenult = pointsXinOrder[2];
            YPenult = pointsYinOrder[2];

            // Intersection
            if ((PointOfIntersection.X > XSecond)&&(PointOfIntersection.X < XPenult)&&(PointOfIntersection.Y > YSecond)&&(PointOfIntersection.Y < YPenult)) {
                Intersection = true;
            } else {
                Intersection = false;
            }
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
