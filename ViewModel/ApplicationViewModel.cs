using StepsAnalysis.Model;
using StepsAnalysis.Model.Export;
using StepsAnalysis.ViewModel.Commands;
using StepsAnalysis.ViewModel.Dialogs;
using StepsAnalysis.ViewModel.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;

namespace StepsAnalysis.ViewModel {
    class ApplicationViewModel {
        private ObservableCollection<UserView> _users;
        private UserView _selectedUser = null;
        private Color _distinguishUsersColor = Color.FromArgb(200, 100, 200, 100);
        private IFileLoadUsers _fileLoad = new JsonFileLoadUsers();
        private IFileSaveDialog _fileSaveDialog = new FileExportDialog();
        private IFilesOpenDialog _filesOpenDialog = new JSONFilesOpenDialog();
        private IFileSaveUser _fileSave = new FileExportUser();

        ///Commands
        private Command _openFileCommand;
        private Command _exportFileCommand;
        private Command _importFileCommand;

        /// <summary>
        /// Get users list.
        /// </summary>
        public ObservableCollection<UserView> Users => _users;
        /// <summary>
        /// Get/Set selected user. Set user results in plotting.
        /// </summary>
        public UserView SelectedUser {
            get => _selectedUser;
            set {
                _selectedUser = value;
                if (_selectedUser != null) {
                    MainChart.Instance.ClearLines();
                    MainChart.Instance.AddLine(UserStepsByDayToPonts(_selectedUser));
                }
            }
        }

        public Command OpenFileCommand => _openFileCommand ??= new((obj) => {
            try {
                string[] fileName = _filesOpenDialog.Open();
                if (fileName != null) {
                    ConvertUserList(_fileLoad.Load(fileName));
                }
            } catch (FileOpenDialogException) {
                MessageBox.Show("Невозможно открыть файл.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (JsonException) {
                MessageBox.Show("JSON файл повреждён или несответсует формату.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (ArgumentNullException) {
                MessageBox.Show("JSON файл повреждён.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
        /// <summary>
        /// Command for export selected user.
        /// </summary>
        public Command ExportFileCommand => _exportFileCommand ??= new((obj) => {
            try {
                var fileName = _fileSaveDialog.Save();

                if (fileName != null) {
                    _fileSave.Save(_selectedUser, fileName);
                }
            } catch (ArgumentNullException) {
                MessageBox.Show("Невозможно экспортировать файл.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }, new((obj) => {
            return _selectedUser != null;
        }));
        /// <summary>
        /// Command
        /// </summary>
        public Command ImportFileCommand => _importFileCommand ??= new((obj) => {

        });

        /// <summary>
        /// 
        /// </summary>
        public ApplicationViewModel() {
            _users = new ObservableCollection<UserView>();
            _users.CollectionChanged += UsersUpdate;
        }

        private void UsersUpdate(object sender, NotifyCollectionChangedEventArgs e) {
            DistinguishUsers();
        }

        private void ConvertUserList(List<User> users) {
            _users.Clear();
            foreach (var user in users) {
                _users.Add(user);
            }
        }

        private void DistinguishUsers() {
            foreach (UserView user in _users) {
                double d = (double)(user.AverageSteps / (double)user.MaxSteps);
                if ((double)(user.AverageSteps / (double)user.MaxSteps) < 1 - 0.2 || (double)user.MinSteps / (double)(user.AverageSteps) < 1 - 0.2) {
                    user.BackgroundColor.Color = _distinguishUsersColor;
                }
            }
        }

        private MainChart.Point[] UserStepsByDayToPonts(UserView user) {
            var points = new List<MainChart.Point>();

            for (int i = 0; i < user.StepsList.Count; i++) {
                var point = new MainChart.Point(i, user.StepsList[i].Steps);

                if (user.StepsList[i].Steps == user.MaxSteps) {
                    point.Parameter = "max";
                } else if (user.StepsList[i].Steps == user.MinSteps) {
                    point.Parameter = "min";
                }

                points.Add(point);
            }

            return points.ToArray();
        }
    }
}
