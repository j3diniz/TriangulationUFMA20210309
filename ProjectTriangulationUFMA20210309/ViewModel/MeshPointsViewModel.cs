using Microsoft.Win32;
using ProjectTriangulationUFMA20210309.Model;
using ProjectTriangulationUFMA20210309.View;
using ProjectTriangulationUFMA20210309.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.IO;
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
    class MeshPointsViewModel : BaseViewModel {

        #region Fields, Properties and Variables
        private MeshPointsControl meshPointsControl;
        public MeshPointsControl MeshPointsControl {
            get { return meshPointsControl; }
            set { meshPointsControl = value; }
        }

        private ViewPortArea viewPortArea;
        public ViewPortArea ViewPortArea {
            get { return viewPortArea; }
            // readonly
            //set { viewPortArea = value; }
        }

        private List<PointXY> inputPoints;
        public List<PointXY> InputPoints {
            get { return inputPoints; }
            set { inputPoints = value; OnPropertyChanged(); }
        }

        private string hasPoint;
        public string HasPoint {
            get { return hasPoint; }
            set { hasPoint = value; OnPropertyChanged(); }
        }


        private MeshPoints meshPoints;
        public MeshPoints MeshPoints {
            get { return meshPoints; }
            set { meshPoints = value; OnPropertyChanged(); }
        }

        private ICommand loadPointsCommand;
        public ICommand LoadPointsCommand {
            get { return loadPointsCommand; }
            set { loadPointsCommand = value; }
        }

        private ICommand updateGraphicsCommand;
        public ICommand UpdateGraphicsCommand {
            get { return updateGraphicsCommand; }
            set { updateGraphicsCommand = value; }
        }

        private ICommand computeLinesCommand;
        public ICommand ComputeLinesCommand {
            get { return computeLinesCommand; }
            set { computeLinesCommand = value; }
        }

        #endregion

        #region Constructors
        public MeshPointsViewModel() {
            viewPortArea = new ViewPortArea(0, 1000, 0, 1000);
            InputPoints = new List<PointXY>();
            MeshPoints = new MeshPoints(InputPoints);
            HasPoint = "No";

            LoadPointsCommand = new DelegateCommand(LoadPoints);
            UpdateGraphicsCommand = new DelegateCommand(UpdateGraphics, UpdateGraphicsCanExecute);
            ComputeLinesCommand = new DelegateCommand(ComputeLines, ComputeLinesCanExecute);
        }
        #endregion

        #region Load Points Command
        public void LoadPoints(object parameter) {
            try {
                string initialDirectory = parameter as string;
                string arquivo = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Abrir Cotas";
                //openFileDialog.InitialDirectory = @"C:\Temp\UFMA";
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = "Rtf documents|*.rtf|Txt files (*.txt)|*.txt|Csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == true) {
                    arquivo = openFileDialog.FileName;
                }

                string[] arquivoLinhas = File.ReadAllLines(arquivo);
                MeshPoints.Points.Clear();
                for (int i = 0; i < arquivoLinhas.Length; i++) {
                    string[] auxiliar = arquivoLinhas[i].Split('/', ';', ',');
                    MeshPoints.Points.Add(new PointXY(Convert.ToDouble(auxiliar[0]), Convert.ToDouble(auxiliar[1])));
                }
                if (MeshPoints.Points.Count > 0) {
                    HasPoint = "Yes";
                }
                MeshPointsControl.lblPointsInfo.Text = "Loaded Points: " + MeshPoints.Points.Count;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error!");
            }
        }
        #endregion

        #region Update Graphics Command
        public bool UpdateGraphicsCanExecute(object parameter) {
            MeshPoints meshPoints = parameter as MeshPoints;
            if (meshPoints != null && meshPoints.Points.Count > 0) {
                return true;
            } else {
                return false;
            }
        }
        public void UpdateGraphics(object parameter) {
            MeshPointsControl.viewPortCanvas.Children.Clear();

            #region Points
            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromRgb(50, 50, 50));

            if ((MeshPoints.Points != null) && (MeshPoints.Points.Count > 0)) {
                foreach (PointXY point in MeshPoints.Points) {
                    Ellipse circleAtPoint = new Ellipse();
                    circleAtPoint.Fill = solidColorBrush;
                    circleAtPoint.Stroke = Brushes.Red;
                    circleAtPoint.StrokeThickness = 1;
                    circleAtPoint.Width = 5;
                    circleAtPoint.Height = 5;
                    Canvas.SetLeft(circleAtPoint, (ViewPortArea.XNormalize(point.X, MeshPointsControl.viewPortCanvas.Width) - (circleAtPoint.Width / 2)));
                    Canvas.SetTop(circleAtPoint, (ViewPortArea.YNormalize(point.Y, MeshPointsControl.viewPortCanvas.Height) - (circleAtPoint.Height / 2)));
                    MeshPointsControl.viewPortCanvas.Children.Add(circleAtPoint);
                }
            }
            #endregion

            #region Raw Lines
            if (MeshPointsControl.chkRawLines.IsChecked == true) {
                foreach (LineSegment lineItem in MeshPoints.RawLines) {
                    Line line = new Line();
                    line.X1 = ViewPortArea.XNormalize(lineItem.StartPoint.X, MeshPointsControl.viewPortCanvas.Width);
                    line.Y1 = ViewPortArea.YNormalize(lineItem.StartPoint.Y, MeshPointsControl.viewPortCanvas.Height);
                    line.X2 = ViewPortArea.XNormalize(lineItem.EndPoint.X, MeshPointsControl.viewPortCanvas.Width);
                    line.Y2 = ViewPortArea.YNormalize(lineItem.EndPoint.Y, MeshPointsControl.viewPortCanvas.Height);
                    line.Stroke = Brushes.Red;
                    line.StrokeThickness = 2;
                    MeshPointsControl.viewPortCanvas.Children.Add(line);
                }
            }
            #endregion

            #region Mesh Lines
            if (MeshPointsControl.chkMeshLines.IsChecked == true) {
                foreach (LineSegment lineItem in MeshPoints.MeshLines) {
                    Line line = new Line();
                    line.X1 = ViewPortArea.XNormalize(lineItem.StartPoint.X, MeshPointsControl.viewPortCanvas.Width);
                    line.Y1 = ViewPortArea.YNormalize(lineItem.StartPoint.Y, MeshPointsControl.viewPortCanvas.Height);
                    line.X2 = ViewPortArea.XNormalize(lineItem.EndPoint.X, MeshPointsControl.viewPortCanvas.Width);
                    line.Y2 = ViewPortArea.YNormalize(lineItem.EndPoint.Y, MeshPointsControl.viewPortCanvas.Height);
                    line.Stroke = Brushes.Blue;
                    line.StrokeThickness = 2;
                    MeshPointsControl.viewPortCanvas.Children.Add(line);
                }
            }
            #endregion

        }
        #endregion

        #region Update Graphics Command
        public bool ComputeLinesCanExecute(object parameter) {
            MeshPoints meshPoints = parameter as MeshPoints;
            if (meshPoints != null && meshPoints.Points.Count > 0) {
                return true;
            } else {
                return false;
            }
        }
        public void ComputeLines(object parameter) {
            MeshPoints.DrawRawLines();
            MeshPoints.DrawMeshLines();
            MeshPointsControl.lblRawLinesInfo.Text = "Raw Lines: " + meshPoints.RawLines.Count;
            MeshPointsControl.lblMeshLinesInfo.Text = "Mesh Lines: " + meshPoints.MeshLines.Count;
        }
        #endregion

    }
}
