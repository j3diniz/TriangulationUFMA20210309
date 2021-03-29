using ProjectTriangulationUFMA20210309.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectTriangulationUFMA20210309 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowTwoLines : Window {

        private readonly ViewPortArea viewPortArea;
        private Line pathT;
        private Line pathV;
        private Ellipse circleAtPoint;
        private IntersectionPoint intersectionPoint;

        // Line T
        private double tXstart;
        private double tYstart;
        private double tXend;
        private double tYend;

        // Line V
        private double vXstart;
        private double vYstart;
        private double vXend;
        private double vYend;

        public MainWindowTwoLines() {
            InitializeComponent();
            viewPortArea= new ViewPortArea(0, 1000, 0, 1000);

            UpdateGraphics();
        }

        #region Events
        private void Exit_OnClick(object sender, RoutedEventArgs e) {
            this.Close();
        }
        private void UpdateGraphics_OnClick(object sender, RoutedEventArgs e) {
            UpdateGraphics();
        }
        #endregion

        private void UpdateGraphics() {

            // Line T
            tXstart = Convert.ToDouble(txtTXstart.Text);
            tYstart = Convert.ToDouble(txtTYstart.Text);
            tXend = Convert.ToDouble(txtTXend.Text);
            tYend = Convert.ToDouble(txtTYend.Text);

            // Line V
            vXstart = Convert.ToDouble(txtVXstart.Text);
            vYstart = Convert.ToDouble(txtVYstart.Text);
            vXend = Convert.ToDouble(txtVXend.Text);
            vYend = Convert.ToDouble(txtVYend.Text);

            viewPortCanvas.Children.Remove(pathT);
            pathT = new Line();
            pathT.X1 = viewPortArea.XNormalize(tXstart, viewPortCanvas.Width);
            pathT.Y1 = viewPortArea.YNormalize(tYstart, viewPortCanvas.Height);
            pathT.X2 = viewPortArea.XNormalize(tXend, viewPortCanvas.Width);
            pathT.Y2 = viewPortArea.YNormalize(tYend, viewPortCanvas.Height);
            pathT.Stroke = Brushes.Blue;
            pathT.StrokeThickness = 2;
            viewPortCanvas.Children.Add(pathT);

            viewPortCanvas.Children.Remove(pathV);
            pathV = new Line();
            pathV.X1 = viewPortArea.XNormalize(vXstart, viewPortCanvas.Width);
            pathV.Y1 = viewPortArea.YNormalize(vYstart, viewPortCanvas.Height);
            pathV.X2 = viewPortArea.XNormalize(vXend, viewPortCanvas.Width);
            pathV.Y2 = viewPortArea.YNormalize(vYend, viewPortCanvas.Height);
            pathV.Stroke = Brushes.Red;
            pathV.StrokeThickness = 2;
            viewPortCanvas.Children.Add(pathV);

            // Intersection Point
            intersectionPoint = new IntersectionPoint(tXstart, tYstart, tXend, tYend, vXstart, vYstart, vXend, vYend);
            txtPointX.Text = intersectionPoint.PointX.ToString();
            txtPointY.Text = intersectionPoint.PointY.ToString();

            txtTb.Text = intersectionPoint.Tb.ToString();
            txtTm.Text = intersectionPoint.Tm.ToString();

            txtVb.Text = intersectionPoint.Vb.ToString();
            txtVm.Text = intersectionPoint.Vm.ToString();

            viewPortCanvas.Children.Remove(circleAtPoint);
            circleAtPoint = new Ellipse();
            circleAtPoint.Fill = new SolidColorBrush(Color.FromRgb(50, 50, 50));
            circleAtPoint.Stroke = Brushes.Red;
            circleAtPoint.StrokeThickness = 2; 
            circleAtPoint.Width= 10;
            circleAtPoint.Height = 10;
            Canvas.SetLeft(circleAtPoint,(viewPortArea.XNormalize(intersectionPoint.PointX, viewPortCanvas.Width) - 5));
            Canvas.SetTop(circleAtPoint, (viewPortArea.YNormalize(intersectionPoint.PointY, viewPortCanvas.Height) - 5));
            viewPortCanvas.Children.Add(circleAtPoint);
            
        }

    }
}
