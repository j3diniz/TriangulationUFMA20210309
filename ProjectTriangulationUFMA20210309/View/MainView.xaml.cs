using ProjectTriangulationUFMA20210309.ViewModel;
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
using System.Windows.Shapes;

namespace ProjectTriangulationUFMA20210309.View {
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window {

        #region Fields, Properties and Variables
        readonly MainViewModel mainViewModel;

        // References to UserControls
        readonly MeshPointsControl meshPointsControl;
        readonly MeshPointsViewModel meshPointsViewModel;
        readonly IntersectionPointControl intersectionPointControl;
        readonly IntersectionPointViewModel intersectionPointViewModel;
        #endregion

        public MainView() {
            InitializeComponent();

            intersectionPointControl = new IntersectionPointControl();
            meshPointsControl = new MeshPointsControl();
            this.MainContentControl.Content = meshPointsControl;

            #region Connecting the main ViewModel with View
            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
            #endregion

            #region Connecting the ViewModel with View UserControl
            // To MeshPoints
            meshPointsViewModel = new MeshPointsViewModel();
            meshPointsViewModel.MeshPointsControl = meshPointsControl;
            mainViewModel.MeshPointsControl = meshPointsControl;
            mainViewModel.MeshPointsViewModel = meshPointsViewModel;
            mainViewModel.CurrentView = meshPointsControl;
            mainViewModel.CurrentViewModel = meshPointsViewModel;

            // To IntersectionPoint
            intersectionPointViewModel = new IntersectionPointViewModel();
            intersectionPointViewModel.IntersectionPointControl = intersectionPointControl;
            mainViewModel.IntersectionPointControl = intersectionPointControl;
            mainViewModel.IntersectionPointViewModel = intersectionPointViewModel;
            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // Referencing the local ViewModel to this View
            this.mainViewModel.MainView = this;
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
