using StepsAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StepsAnalysis.ViewModel.Views {
    public class UserView {
        private User _user;
        private SolidColorBrush _backgroundColor;

        /// <summary>
        /// Get user.
        /// </summary>
        public User User => _user;
        /// <summary>
        /// Get user name.
        /// </summary>
        public string Name => _user.Name;
        /// <summary>
        /// Get user surname.
        /// </summary>
        public string Surname => _user.Surname;
        /// <summary>
        /// Get user steps list on the days.
        /// </summary>
        public UserStepsListByDay StepsList => _user.StepsList;
        /// <summary>
        /// Get average number of steps.
        /// </summary>
        public int AverageSteps => _user.StepsList.GetAverageStepsNumber();
        /// /// <summary>
        /// Get maximal number of steps.
        /// </summary>
        public int MaxSteps => _user.StepsList.GetMaxStepsNumber();
        /// <summary>
        /// Get minimal number of steps.
        /// </summary>
        public int MinSteps => _user.StepsList.GetMinStepsNumber();
        /// <summary>
        /// Get background color System.Windows.Media.SolidColorBrush.
        /// </summary>
        public SolidColorBrush BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">User</param>
        public UserView(User user) {
            _user = user;
            _backgroundColor = new SolidColorBrush();
        }

        /// <summary>
        /// Overloading UserView to User Conversion Operations.
        /// </summary>
        /// <param name="user">User</param>
        public static implicit operator UserView(User user) {
            return new UserView(user);
        }

        /// <summary>
        /// Overloading User to UserView Conversion Operations.
        /// </summary>
        /// <param name="userView">User view</param>
        public static implicit operator User(UserView userView) {
            return userView._user;
        }
    }
}
