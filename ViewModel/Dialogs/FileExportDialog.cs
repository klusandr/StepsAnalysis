using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.ViewModel.Dialogs {
    class FileExportDialog : IFileSaveDialog {
        private SaveFileDialog _fileDialog;

        private string[] _fileFormats = new string[] { ".json", ".xml", ".csv" };

        public string FileFormat => null;

        public string[] FileFormats => _fileFormats;

        public FileExportDialog() {
            _fileDialog = new SaveFileDialog() { 
                Filter = "Json files(*.json) | *.json| XML files(*.xml) | *.xml| CSV files(*.csv) | *.csv",
            };
        }

        public string Save() {
            if (_fileDialog.ShowDialog() == true) {
                return _fileDialog.FileName;
            }

            return null;
        }
    }
}
