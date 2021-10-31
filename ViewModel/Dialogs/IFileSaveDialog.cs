using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.ViewModel.Dialogs {
    interface IFileSaveDialog {
        public string FileFormat { get; }
        public string[] FileFormats { get; }
        public string Save();
    }
}
