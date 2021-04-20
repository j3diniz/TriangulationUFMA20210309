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
        private List<PointXY> points;

        public MainWindow() {
            InitializeComponent();
            viewPortArea = new ViewPortArea(0, 1000, 0, 1000);
            // points = new List<PointXY> { new PointXY(50, 100), new PointXY(500, 500), new PointXY(50, 700), new PointXY(800, 800), new PointXY(800, 50) };
            points = new List<PointXY>();

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

            viewPortCanvas.Children.Clear();

            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromRgb(50, 50, 50));

            if ((points != null) && (points.Count > 0)) {
                foreach (PointXY point in points) {
                    Ellipse circleAtPoint = new Ellipse();
                    circleAtPoint.Fill = solidColorBrush;
                    circleAtPoint.Stroke = Brushes.Red;
                    circleAtPoint.StrokeThickness = 2;
                    circleAtPoint.Width = 10;
                    circleAtPoint.Height = 10;
                    Canvas.SetLeft(circleAtPoint, (viewPortArea.XNormalize(point.X, viewPortCanvas.Width) - 5));
                    Canvas.SetTop(circleAtPoint, (viewPortArea.YNormalize(point.Y, viewPortCanvas.Height) - 5));
                    viewPortCanvas.Children.Add(circleAtPoint);
                }
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
                    points.Add(new PointXY(Convert.ToDouble(auxiliar[0]), Convert.ToDouble(auxiliar[1])));
                }
                //anagramas.Dicionario = dicionario;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error!");
            }
        }
    }
}
