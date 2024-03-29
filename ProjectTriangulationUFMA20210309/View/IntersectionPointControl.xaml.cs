﻿using ProjectTriangulationUFMA20210309.ViewModel;
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

namespace ProjectTriangulationUFMA20210309.View {
    /// <summary>
    /// Interaction logic for IntersectionPointControl.xaml
    /// </summary>
    public partial class IntersectionPointControl : UserControl {

        #region Fields, Properties and Variables
        // Reference of the View parent
        MainViewModel mainViewModel;
        MainView mainView;

        //Reference of the local ViewModel
        IntersectionPointViewModel intersectionPointViewModel;
        #endregion

        public IntersectionPointControl() {
            InitializeComponent();
        }

        #region Connecting the parent View, this View and the local ViewModel
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            // Getting parent View
            Window parentWindow = Window.GetWindow(this);
            this.mainView = parentWindow as MainView;

            this.mainViewModel = mainView.DataContext as MainViewModel;
            this.intersectionPointViewModel = this.mainViewModel.IntersectionPointViewModel;
            this.DataContext = this.intersectionPointViewModel;

            intersectionPointViewModel.UpdateGraphics(intersectionPointViewModel.IntersectionPoint);
        }
        #endregion
    }
}
