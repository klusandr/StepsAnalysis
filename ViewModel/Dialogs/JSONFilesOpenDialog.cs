using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.ViewModel.Dialogs {
    class JSONFilesOpenDialog : IFilesOpenDialog {
        OpenFileDialog _fileDialog;

        /// <summary>
        /// Get format opens files. 
        /// </summary>
        public string FilesFormat => ".json";

        /// <summary>
        /// Get formats opens files. 
        /// </summary>
        public string[] FilesFormats => null;

        /// <summary>
        /// 
        /// </summary>
        public JSONFilesOpenDialog() {
            _fileDialog = new OpenFileDialog() { Filter = "Json files(*.json)|*.json", Multiselect = true };
        }

        /// <summary>
        /// Get files nemes.
        /// </summary>
        /// <returns>Files nemes</returns>
        public string[] Open() {
            if (_fileDialog.ShowDialog() == true) {
                return _fileDialog.FileNames;
            }
           
            return null;
        }
    }
}
