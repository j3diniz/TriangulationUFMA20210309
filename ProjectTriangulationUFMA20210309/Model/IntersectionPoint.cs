using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTriangulationUFMA20210309.Model {
    class IntersectionPoint {


        #region Fields, Properties and Variables
        // ToDo: Implement Class Line and Point

        // Second X Point of Both Lines
        private double XSecond;

        // Penult X Point of Both Lines
        private double XPenult;

        // Second Y Point of Both Lines
        private double YSecond;

        // Penult Y Point of Both Lines
        private double YPenult;

        // Line T
        private double TXstart;
        private double TYstart;
        private double TXend;
        private double TYend;
        public double Tm;
        public double Tb;

        // Line V
        private double VXstart;
        private double VYstart;
        private double VXend;
        private double VYend;
        public double Vm;
        public double Vb;

        // Intersection Points

        private double pointX;
        public double PointX {
            get { return pointX; }
            set { pointX = value; }
        }

        private double pointY;
        public double PointY {
            get { return pointY; }
            set { pointY = value; }
        }

        // Intersection?
        private bool intersection;
        public bool Intersection {
            get { return intersection; }
            set { intersection = value; }
        }

        #endregion

        #region Constructors
        public IntersectionPoint(double tXstart, double tYstart, double tXend, double tYend, double vXstart, double vYstart, double vXend, double vYend) {
            // Line T
            TXstart = tXstart;
            TYstart = tYstart;
            if (tXstart== tXend) {
                TXend = tXend+1;
            } else {
                TXend = tXend;
            }
            TYend = tYend;
            Tm = (TYend - TYstart) / (TXend - TXstart);
            Tb = TYstart - (Tm * TXstart);


            // Line V
            VXstart = vXstart;
            VYstart = vYstart;
            if (vXstart == vXend) {
                VXend = vXend + 1;
            } else {
                VXend = vXend;
            }
            VYend = vYend;
            Vm = (VYend - VYstart) / (VXend - VXstart);
            Vb = VYstart - (Vm * VXstart);

            // Intersection Points
            PointX = (Vb - Tb) / (Tm - Vm);
            PointY = (Tm * PointX) + Tb;

            // Second and Penult
            List<double> pointsX = new List<double> { TXstart, TXend, VXstart, VXend };
            var pointsXinOrder = pointsX.OrderBy(x => x).ToList();

            List<double> pointsY = new List<double> { TYstart, TYend, VYstart, VYend };
            var pointsYinOrder = pointsY.OrderBy(y => y).ToList();

            XSecond = pointsXinOrder[1];
            YSecond = pointsYinOrder[1];
            XPenult = pointsXinOrder[2];
            YPenult = pointsYinOrder[2];

            // Intersection
            if ((PointX>XSecond)&&(PointX<XPenult)&&(PointY>YSecond)&&(PointY<YPenult)) {
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
