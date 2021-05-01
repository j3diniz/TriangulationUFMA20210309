using ProjectTriangulationUFMA20210309.View;
using ProjectTriangulationUFMA20210309.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectTriangulationUFMA20210309.ViewModel {
    class MainViewModel : BaseViewModel {

        #region Fields, Properties and Variables
        private MainView mainView;
        public MainView MainView {
            get { return mainView; }
            set { mainView = value; }
        }

        private BaseViewModel currentViewModel;
        public BaseViewModel CurrentViewModel {
            get { return currentViewModel; }
            set { currentViewModel = value; OnPropertyChanged(); }
        }

        private UserControl currentView;
        public UserControl CurrentView {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }

        private ICommand changeViewCommand;
        public ICommand ChangeViewCommand {
            get { return changeViewCommand; }
            set { changeViewCommand = value; }
        }

        private MeshPointsViewModel meshPointsViewModel;
        public MeshPointsViewModel MeshPointsViewModel {
            get { return meshPointsViewModel; }
            set { meshPointsViewModel = value; OnPropertyChanged(); }
        }

        private MeshPointsControl meshPointsControl;
        public MeshPointsControl MeshPointsControl {
            get { return meshPointsControl; }
            set { meshPointsControl = value; OnPropertyChanged(); }
        }

        private IntersectionPointViewModel intersectionPointViewModel;
        public IntersectionPointViewModel IntersectionPointViewModel {
            get { return intersectionPointViewModel; }
            set { intersectionPointViewModel = value; OnPropertyChanged(); }
        }


        private IntersectionPointControl intersectionPointControl;
        public IntersectionPointControl IntersectionPointControl {
            get { return intersectionPointControl; }
            set { intersectionPointControl = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructors
        public MainViewModel() {
            ChangeViewCommand = new DelegateCommand(ChangeView, ChangeViewCanExecute);
        }
        #endregion

        #region Change View Command
        public bool ChangeViewCanExecute(object parameter) {
            UserControl userControl = parameter as UserControl;
            if (userControl == null) {
                return false;
            } else {
                return true;
            }
        }
        public void ChangeView(object parameter) {
            UserControl userControl = parameter as UserControl;

            CurrentView = userControl;
            this.MainView.MainContentControl.Content = CurrentView;

        }
        #endregion

    }
}
