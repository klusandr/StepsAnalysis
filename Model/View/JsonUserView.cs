using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.Model.Views {
    [Serializable]
    public class JsonUserView {
        private User _user;

        public string Name => _user.Name;

        public string Surname => _user.Surname;

        public int AverageSteps => _user.StepsList.GetAverageStepsNumber();

        public int MaxSteps => _user.StepsList.GetMaxStepsNumber();

        public int MinSteps => _user.StepsList.GetMinStepsNumber();

        public UserStepsListByDay StepsList => _user.StepsList;

        public JsonUserView(User user) {
            _user = user;
        }
    }
}
