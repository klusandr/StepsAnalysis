using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StepsAnalysis.Model {
    
    class JsonFileLoadUsers : IFileLoadUsers {
        private struct UserLoad {
            public int Rank { get; set; }
            public string User { get; set; }
            public string Status { get; set; }
            public int Steps { get; set; }

            public string GetName() {
                return User?.Substring(User.IndexOf(' ') + 1) ?? "";
            }

            public string GetSurname() {
                return User?.Substring(0, User.IndexOf(' ')) ?? "";
            }
        }

        private List<User> _users;
        private int _dayNumber;

        /// <summary>
        /// Loads a users and steps data from a JSON file.
        /// </summary>
        /// <param name="fileName">Path and name json file</param>
        /// <returns>Users list</returns>
        public List<User> Load(string fileName) {
            _users = new List<User>();
            _dayNumber = 1;

            List<UserLoad> usersLoad = JsonSerializer.Deserialize<List<UserLoad>>(GetJSONFileString(fileName));

            if (_users.Count == 0) {
                AddUsers(usersLoad);
            } else {
                AddUsersDay(usersLoad);
            }

            return (_users.Count != 0) ? _users : throw new ArgumentNullException("Users is not load.");
        }

        /// <summary>
        /// Loads a users and steps data from a JSON files.
        /// </summary>
        /// <param name="fileNames">Paths and names json files</param>
        /// <returns>Users list</returns>
        public List<User> Load(string[] fileNames) {
            _users = new List<User>();
            _dayNumber = 1;

            foreach (var fileName in fileNames) {
                List<UserLoad> usersLoad = JsonSerializer.Deserialize<List<UserLoad>>(GetJSONFileString(fileName));

                if (_users.Count == 0) {
                    AddUsers(usersLoad);
                } else {
                    AddUsersDay(usersLoad);
                }
            }

            return (_users.Count != 0) ? _users : throw new ArgumentNullException("Users is not load.");
        }

        private UserLoad GetNewUserLoad(List<UserLoad> usersLoad) {
            foreach (var userLoad in usersLoad) {
                bool userFind = false;
                foreach (var user in _users) {
                    if (user.Name == userLoad.GetName() && user.Surname == userLoad.GetSurname()) {
                        userFind = true;
                    }
                }
                if (!userFind) {
                    return userLoad;
                }
            }
            throw new ArgumentNullException("Not find new user.");
        }

        private List<UserLoad> GetNewUsersLoad(List<UserLoad> usersLoad) {
            var findUsersLoad = new List<UserLoad>();
            foreach (var userLoad in usersLoad) {
                bool findUser = false;
                foreach (var user in _users) {
                    if (user.Name == userLoad.GetName() && user.Surname == userLoad.GetSurname()) {
                        findUser = true;
                    }
                }
                if (!findUser) {
                    findUsersLoad.Add(userLoad);
                }
            }
            return (findUsersLoad.Count != 0) ? findUsersLoad : throw new ArgumentNullException("Not find new users."); ;
        }

        private void AddUsersDay(List<UserLoad> usersLoad) {
            foreach (var user in _users) {
                var day = new Day(0, "Indefined", -1);

                foreach (var userLoad in usersLoad) {
                    if (user.Name == userLoad.GetName() && user.Surname == userLoad.GetSurname()) {
                        day.Rank = userLoad.Rank;
                        day.Status = userLoad.Status;
                        day.Steps = userLoad.Steps;
                        break;
                    }
                }

                user.StepsList.Add(day);
            }
            if (_dayNumber > 1) {
                if (usersLoad.Count - _users.Count == 1) {
                    AddUser(GetNewUserLoad(usersLoad));
                } else if (usersLoad.Count - _users.Count > 1) {
                    AddUsers(GetNewUsersLoad(usersLoad));
                }
            }
        }

        private void AddUser(UserLoad userLoad, List<Day> oldDays = null) {
            var user = new User(userLoad.GetName(), userLoad.GetSurname(), new List<Day>());

            if (oldDays != null) {
                user.StepsList.AddRange(oldDays);
            }

            user.StepsList.Add(userLoad.Rank, userLoad.Status, userLoad.Steps);

            _users.Add(user);
        }

        private void AddUsers(List<UserLoad> usersLoad) {
            foreach (var userLoad in usersLoad) {
                AddUser(userLoad, GetOldDays());
            }
        }

        private List<Day> GetOldDays() {
            if (_dayNumber == 1) {
                return null;
            }

            var oldDays = new List<Day>();

            for (int i = 0; i < _dayNumber; i++) {
                oldDays.Add(new Day(0, "Indefined", -1));
            }

            return oldDays;
        }

        private static string GetJSONFileString(string path) {
            using (StreamReader fs = new StreamReader(path)) {
                return fs.ReadToEnd();
            }
        }
    }
}
