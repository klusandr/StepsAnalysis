using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.ViewModel.Dialogs {
    interface IFilesOpenDialog {
        public string FilesFormat { get; }
        public string[] FilesFormats { get; }
        public string[] Open();
    }
}
