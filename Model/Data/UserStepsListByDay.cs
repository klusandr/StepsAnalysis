using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.Model {

    /// <summary>
    /// User rank, status and steps by one day
    /// </summary>
    public struct Day {

        private int _rank;
        private string _status;
        private int _steps;

        public int Rank { get => _rank; set {
                if (value < 0) {
                    _rank = 0;
                } else {
                    _rank = value;
                }
            } 
        }
        public string Status { get => _status; set => _status = value; }
        public int Steps { get => _steps ; set {
                if (value < -1) {
                    _steps = -1;
                } else {
                    _steps = value;
                }
            }
        }

        public Day(int rank, string status, int steps) {
            _rank = 1;
            _status = "";
            _steps = 0;

            Rank = rank;
            Status = status;
            Steps = steps;
        }
    }

    public class UserStepsListByDay : List<Day> {
        private User _user;

        /// <summary>
        /// Get user.
        /// </summary>
        public User User => _user;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">User.</param>
        public UserStepsListByDay(User user) {
            _user = user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="daysList">List steps, rank and user status by day</param>
        public UserStepsListByDay(User user, List<Day> daysList) : base(daysList) {
            _user = user;
        }

        /// <summary>
        /// Adds a day to the end of the list.
        /// </summary>
        /// <param name="rank">User rank on this day</param>
        /// <param name="userStatus">User status on this day</param>
        /// <param name="steps">User steps on this day</param>
        public void Add(int rank, string userStatus, int steps) {
            Add(new Day() { Rank = rank, Status = userStatus, Steps = steps });
        }

        /// <summary>
        /// Get average number of steps.
        /// </summary>
        /// <returns>Average number of steps in int</returns>
        public int GetAverageStepsNumber() {
            return (int)Math.Round(this.Average(day => day.Steps));
        }

        /// <summary>
        /// Get minimal number of steps.
        /// </summary>
        /// <returns>Minimal number of steps</returns>
        public int GetMinStepsNumber() {
            return this.Where(day => day.Steps > 0).Min(day => day.Steps);
        }

        /// <summary>
        /// Get maximal number of steps.
        /// </summary>
        /// <returns>Maximal number of steps</returns>
        public int GetMaxStepsNumber() {
            return this.Max(day => day.Steps);
        }
    }
}
