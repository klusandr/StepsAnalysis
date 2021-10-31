using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.Model {

    /// <summary>
    /// User, as well as information about the steps taken for each day.
    /// </summary>
    public class User {
        private string _name;
        private string _surname;
        private UserStepsListByDay _stepsList;

        /// <summary>
        /// Get user name.
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Get user surname.
        /// </summary>
        public string Surname => _surname;
        /// <summary>
        /// Get user steps list on the days.
        /// </summary>
        public UserStepsListByDay StepsList => _stepsList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="surname">User surname.</param>
        /// <param name="stepsList">User steps list on the days</param>
        public User(string name, string surname, List<Day> stepsList) {
            _name = name;
            _surname = surname;
            _stepsList = new UserStepsListByDay(this, stepsList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="surname">User surname.</param>
        public User(string name, string surname) : this(name, surname, new List<Day>()) { }

        public User(User user) {
            _name = user._name;
            _surname = user._surname;
            _stepsList = new UserStepsListByDay(this, user._stepsList);
        }
    }
}
