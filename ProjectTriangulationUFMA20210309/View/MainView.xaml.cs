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
    public partial class MainWindow : Window {

        private readonly ViewPortArea viewPortArea;
        private Line path;

        public MainWindow() {
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
            viewPortCanvas.Children.Remove(path);
            path = new Line();
            path.X1 = viewPortArea.XNormalize(Convert.ToDouble(txtTXstart.Text), viewPortCanvas.Width);
            path.Y1 = viewPortArea.YNormalize(Convert.ToDouble(txtTYstart.Text), viewPortCanvas.Height);
            path.X2 = viewPortArea.XNormalize(Convert.ToDouble(txtTXend.Text), viewPortCanvas.Width);
            path.Y2 = viewPortArea.YNormalize(Convert.ToDouble(txtTYend.Text), viewPortCanvas.Height);
            path.Stroke = Brushes.Blue;
            path.StrokeThickness = 2;
            viewPortCanvas.Children.Add(path);


        }

    }
}
