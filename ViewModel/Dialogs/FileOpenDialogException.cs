using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.ViewModel.Dialogs {

    [Serializable]
    public class FileOpenDialogException : Exception {
        public FileOpenDialogException() { }
        public FileOpenDialogException(string message) : base(message) { }
        public FileOpenDialogException(string message, Exception inner) : base(message, inner) { }
        protected FileOpenDialogException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
