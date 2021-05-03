using ProjectTriangulationUFMA20210309.Model;
using ProjectTriangulationUFMA20210309.View;
using ProjectTriangulationUFMA20210309.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using LineSegment = ProjectTriangulationUFMA20210309.Model.LineSegment;

namespace ProjectTriangulationUFMA20210309.ViewModel {
    class IntersectionPointViewModel : BaseViewModel {

        #region Fields, Properties and Variables
        private IntersectionPointControl intersectionPointControl;
        public IntersectionPointControl IntersectionPointControl {
            get { return intersectionPointControl; }
            set { intersectionPointControl = value; }
        }

        private ViewPortArea viewPortArea;
        public ViewPortArea ViewPortArea {
            get { return viewPortArea; }
            // readonly
            //set { viewPortArea = value; }
        }

        private LineSegment lineT;
        public LineSegment LineT {
            get { return lineT; }
            set { lineT = value; OnPropertyChanged(); }
        }

        private LineSegment lineV;
        public LineSegment LineV {
            get { return lineV; }
            set { lineV = value; OnPropertyChanged(); }
        }

        private IntersectionPoint intersectionPoint;
        public IntersectionPoint IntersectionPoint {
            get { return intersectionPoint; }
            set { intersectionPoint = value; OnPropertyChanged(); }
        }


        private ICommand updateGraphicsCommand;
        public ICommand UpdateGraphicsCommand {
            get { return updateGraphicsCommand; }
            set { updateGraphicsCommand = value; }
        }
        #endregion

        #region Constructors
        public IntersectionPointViewModel() {
            viewPortArea = new ViewPortArea(0, 1000, 0, 1000);

            //UpdateGraphics(IntersectionPoint);
            UpdateGraphicsCommand = new DelegateCommand(UpdateGraphics);
        }
        #endregion

        #region Update Graphics Command
        // ToDo: Implement role to UpdateGraphicsCanExecute() execute
        public bool UpdateGraphicsCanExecute(object parameter) {
            IntersectionPoint intersectionPoint = parameter as IntersectionPoint;
            if (intersectionPoint != null) {
                return true;
            } else {
                return false;
            }
        }
        public void UpdateGraphics(object parameter) {

            IntersectionPointControl.viewPortCanvas.Children.Clear();

            // Line T
            double tXstart = Convert.ToDouble(IntersectionPointControl.txtTXstart.Text);
            double tYstart = Convert.ToDouble(IntersectionPointControl.txtTYstart.Text);
            double tXend = Convert.ToDouble(IntersectionPointControl.txtTXend.Text);
            double tYend = Convert.ToDouble(IntersectionPointControl.txtTYend.Text);
            PointXY pointStartT = new PointXY(tXstart, tYstart);
            PointXY pointEndT = new PointXY(tXend, tYend);
            LineT = new LineSegment(pointStartT, pointEndT);
            Line pathT = new Line();
            pathT.X1 = viewPortArea.XNormalize(tXstart, IntersectionPointControl.viewPortCanvas.Width);
            pathT.Y1 = viewPortArea.YNormalize(tYstart, IntersectionPointControl.viewPortCanvas.Height);
            pathT.X2 = viewPortArea.XNormalize(tXend, IntersectionPointControl.viewPortCanvas.Width);
            pathT.Y2 = viewPortArea.YNormalize(tYend, IntersectionPointControl.viewPortCanvas.Height);
            pathT.Stroke = Brushes.Blue;
            pathT.StrokeThickness = 2;
            IntersectionPointControl.viewPortCanvas.Children.Add(pathT);

            // Line V
            double vXstart = Convert.ToDouble(IntersectionPointControl.txtVXstart.Text);
            double vYstart = Convert.ToDouble(IntersectionPointControl.txtVYstart.Text);
            double vXend = Convert.ToDouble(IntersectionPointControl.txtVXend.Text);
            double vYend = Convert.ToDouble(IntersectionPointControl.txtVYend.Text);
            PointXY pointStartV = new PointXY(vXstart, vYstart);
            PointXY pointEndV = new PointXY(vXend, vYend);
            LineV = new LineSegment(pointStartV, pointEndV);
            Line pathV = new Line();
            pathV.X1 = viewPortArea.XNormalize(vXstart, IntersectionPointControl.viewPortCanvas.Width);
            pathV.Y1 = viewPortArea.YNormalize(vYstart, IntersectionPointControl.viewPortCanvas.Height);
            pathV.X2 = viewPortArea.XNormalize(vXend, IntersectionPointControl.viewPortCanvas.Width);
            pathV.Y2 = viewPortArea.YNormalize(vYend, IntersectionPointControl.viewPortCanvas.Height);
            pathV.Stroke = Brushes.Red;
            pathV.StrokeThickness = 2;
            IntersectionPointControl.viewPortCanvas.Children.Add(pathV);

            // Intersection Point
            IntersectionPoint = new IntersectionPoint(lineT, lineV);
            if (IntersectionPoint.Intersection) {
                IntersectionPointControl.lblIntersection.Text = "Intersection? true!";
            } else {
                IntersectionPointControl.lblIntersection.Text = "Intersection? false!";
            }
            IntersectionPointControl.lblPointX.Text = "X: " + Math.Round(IntersectionPoint.PointOfIntersection.X, 2).ToString();
            IntersectionPointControl.lblPointY.Text = "Y: " + Math.Round(IntersectionPoint.PointOfIntersection.Y, 2).ToString();

            IntersectionPointControl.lblTb.Text = "Tb: " + Math.Round(IntersectionPoint.LineT.B, 2).ToString();
            IntersectionPointControl.lblTm.Text = "Tm: " + Math.Round(IntersectionPoint.LineT.M, 2).ToString();

            IntersectionPointControl.lblVb.Text = "Vb: " + Math.Round(IntersectionPoint.LineV.B, 2).ToString();
            IntersectionPointControl.lblVm.Text = "Vm: " + Math.Round(IntersectionPoint.LineV.M, 2).ToString();

            Ellipse circleAtPoint = new Ellipse();
            circleAtPoint.Fill = new SolidColorBrush(Color.FromRgb(50, 50, 50));
            circleAtPoint.Stroke = Brushes.Red;
            circleAtPoint.StrokeThickness = 2;
            circleAtPoint.Width = 10;
            circleAtPoint.Height = 10;
            Canvas.SetLeft(circleAtPoint, (viewPortArea.XNormalize(IntersectionPoint.PointOfIntersection.X, IntersectionPointControl.viewPortCanvas.Width) - 5));
            Canvas.SetTop(circleAtPoint, (viewPortArea.YNormalize(IntersectionPoint.PointOfIntersection.Y, IntersectionPointControl.viewPortCanvas.Height) - 5));
            IntersectionPointControl.viewPortCanvas.Children.Add(circleAtPoint);
        }
        #endregion

    }
}
