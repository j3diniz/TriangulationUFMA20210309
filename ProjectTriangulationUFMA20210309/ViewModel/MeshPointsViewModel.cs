using ProjectTriangulationUFMA20210309.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTriangulationUFMA20210309.ViewModel {
    class MeshPointsViewModel : BaseViewModel {

        #region Fields, Properties and Variables
        private MeshPointsControl meshPointsControl;
        public MeshPointsControl MeshPointsControl {
            get { return meshPointsControl; }
            set { meshPointsControl = value; }
        }
        #endregion
    }
}
