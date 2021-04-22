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

        private List<LineSegment> meshLines;
        public List<LineSegment> MeshLines {
            get { return meshLines; }
            set { meshLines = value; }
        }

        private List<LineSegment> rawLines;
        public List<LineSegment> RawLines {
            get { return rawLines; }
            set { rawLines = value; }
        }
        #endregion

        #region Constructors
        public MeshPoints(List<PointXY> points) {
            Points = points;
            RawLines = new List<LineSegment>();
            MeshLines = new List<LineSegment>();
        }
        #endregion

        public void DrawRawLines() {
            for (int i = 0; i < Points.Count; i++) {
                for (int j = i + 1; j < Points.Count; j++) {
                    RawLines.Add(new LineSegment(Points[i], Points[j]));
                }
            }
        }

        public void DrawMeshLines() {
            for (int i = 0; i < Points.Count; i++) {
                for (int j = i + 1; j < Points.Count; j++) {
                    LineSegment actualLine = new LineSegment(Points[i], Points[j]);
                    if (MeshLines.Count == 0) {
                        MeshLines.Add(actualLine);
                    } else {
                        bool hasIntersection = false;
                        for (int k = 0; k < MeshLines.Count; k++) {
                            LineSegment comparedLine = MeshLines[k];
                            IntersectionPoint intersectionPoint = new IntersectionPoint(actualLine, comparedLine);
                            if (intersectionPoint.Intersection) {
                                hasIntersection = true;
                                if (actualLine.Length>comparedLine.Length) {
                                    break;
                                } else {
                                    MeshLines.Remove(comparedLine);
                                    MeshLines.Add(actualLine);
                                }
                            }
                        }
                        if (hasIntersection == false) {
                            MeshLines.Add(actualLine);
                        }
                    }
                }
            }
        }

    }
}
