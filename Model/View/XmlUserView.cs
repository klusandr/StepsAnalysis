using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.Model.Views {
    [Serializable]
    public class XmlUserView {
        public string Name;
        public string Surname;
        public int AverageSteps;
        public int MaxSteps;
        public int MinSteps;
        public UserStepsListByDay StepsList;

        public XmlUserView() { }

        public XmlUserView(User user) {
            Name = user.Name;
            Surname = user.Surname;
            AverageSteps = user.StepsList.GetAverageStepsNumber();
            MaxSteps = user.StepsList.GetMaxStepsNumber();
            MinSteps = user.StepsList.GetMinStepsNumber();
            StepsList = user.StepsList;
        }
    }
}
