using Microsoft.Win32;
using ProjectTriangulationUFMA20210309.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
using LineSegment = ProjectTriangulationUFMA20210309.Model.LineSegment;

namespace ProjectTriangulationUFMA20210309 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly ViewPortArea viewPortArea;
        private List<PointXY> inputPoints;
        private MeshPoints meshPoints;

        public MainWindow() {
            InitializeComponent();
            viewPortArea = new ViewPortArea(0, 1000, 0, 1000);
            inputPoints = new List<PointXY>();
            meshPoints = new MeshPoints(inputPoints);

            UpdateGraphics();
        }

        #region Events
        private void Exit_OnClick(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void UpdateGraphics_OnClick(object sender, RoutedEventArgs e) {
            if ((meshPoints.Points != null) && (meshPoints.Points.Count > 0)) {
                UpdateGraphics();
            } else {
                MessageBox.Show("Load points!", "Warning!");
            }
        }

        private void LoadPoints_OnClick(object sender, RoutedEventArgs e) {
            try {
                string arquivo = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Abrir Cotas";
                openFileDialog.InitialDirectory = @"C:\Temp\UFMA";
                openFileDialog.Filter = "Rtf documents|*.rtf|Txt files (*.txt)|*.txt|Csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == true) {
                    arquivo = openFileDialog.FileName;
                }

                string[] arquivoLinhas = File.ReadAllLines(arquivo);
                for (int i = 0; i < arquivoLinhas.Length; i++) {
                    string[] auxiliar = arquivoLinhas[i].Split('/', ';', ',');
                    meshPoints.Points.Add(new PointXY(Convert.ToDouble(auxiliar[0]), Convert.ToDouble(auxiliar[1])));
                }
                lblPointsInfo.Text = "Loaded Points: " + meshPoints.Points.Count;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void ComputeLines_OnClick(object sender, RoutedEventArgs e) {
            if ((meshPoints.Points != null) && (meshPoints.Points.Count > 0)) {
                meshPoints.DrawRawLines();
                UpdateGraphics();
            } else {
                MessageBox.Show("Load points!", "Warning!");
            }
        }
        #endregion

        private void UpdateGraphics() {

            viewPortCanvas.Children.Clear();

            #region Points
            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromRgb(50, 50, 50));

            if ((meshPoints.Points != null) && (meshPoints.Points.Count > 0)) {
                foreach (PointXY point in meshPoints.Points) {
                    Ellipse circleAtPoint = new Ellipse();
                    circleAtPoint.Fill = solidColorBrush;
                    circleAtPoint.Stroke = Brushes.Red;
                    circleAtPoint.StrokeThickness = 1;
                    circleAtPoint.Width = 5;
                    circleAtPoint.Height = 5;
                    Canvas.SetLeft(circleAtPoint, (viewPortArea.XNormalize(point.X, viewPortCanvas.Width) - (circleAtPoint.Width / 2)));
                    Canvas.SetTop(circleAtPoint, (viewPortArea.YNormalize(point.Y, viewPortCanvas.Height) - (circleAtPoint.Height / 2)));
                    viewPortCanvas.Children.Add(circleAtPoint);
                }
            }
            if ((meshPoints.Lines != null) && (meshPoints.Lines.Count > 0)) {
                foreach (LineSegment lineItem in meshPoints.Lines) {
                    Line line = new Line();
                    line.X1 = viewPortArea.XNormalize(lineItem.StartPoint.X, viewPortCanvas.Width);
                    line.Y1 = viewPortArea.YNormalize(lineItem.StartPoint.Y, viewPortCanvas.Height);
                    line.X2 = viewPortArea.XNormalize(lineItem.EndPoint.X, viewPortCanvas.Width);
                    line.Y2 = viewPortArea.YNormalize(lineItem.EndPoint.Y, viewPortCanvas.Height);
                    line.Stroke = Brushes.Red;
                    line.StrokeThickness = 2;
                    viewPortCanvas.Children.Add(line);
                }
            }
            lblPointsInfo.Text = "Loaded Points: " + meshPoints.Points.Count;
            lblLinesInfo.Text = "Loaded Lines: " + meshPoints.Lines.Count;
            #endregion

            #region Lines
            lblLinesInfo.Text = "Loaded Lines: " + meshPoints.Lines.Count;
            #endregion

        }


    }
}
