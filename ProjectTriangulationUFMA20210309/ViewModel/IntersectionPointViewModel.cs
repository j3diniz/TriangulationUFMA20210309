using ProjectTriangulationUFMA20210309.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTriangulationUFMA20210309.ViewModel {
    class IntersectionPointViewModel : BaseViewModel {

        #region Fields, Properties and Variables
        private IntersectionPointControl intersectionPointControl;
        public IntersectionPointControl IntersectionPointControl {
            get { return intersectionPointControl; }
            set { intersectionPointControl = value; }
        }
        #endregion

    }
}
