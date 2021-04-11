using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTriangulationUFMA20210309.Model {
    class PointXY {

        #region Fields, Properties and Variables
        private double x;
        public double X {
            get { return x; }
            set { x = value; }
        }

        private double y;
        public double Y {
            get { return y; }
            set { y = value; }
        }
        #endregion

        #region Constructors
        public PointXY(double x, double y) {
            X = x;
            Y = y;
        }
        #endregion

        // ToDo: Implement ToString()
    }
}
