using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTriangulationUFMA20210309.Model {
    class MeshPoints {

        #region Fields, Properties and Variables
        private List<PointXY> points;
        public List<PointXY> Points {
            get { return points; }
            set { points = value; }
        }

        private List<LineSegment> lines;
        public List<LineSegment> Lines {
            get { return lines; }
            set { lines = value; }
        }
        #endregion

        #region Constructors
        public MeshPoints(List<PointXY> points) {
            Points = points;
            Lines = new List<LineSegment>();
        }
        #endregion

        public void DrawRawLines() {
            for (int i = 0; i < Points.Count; i++) {
                for (int j = i + 1; j < Points.Count; j++) {
                    Lines.Add(new LineSegment(Points[i], Points[j]));
                }
            }
        }

    }
}
