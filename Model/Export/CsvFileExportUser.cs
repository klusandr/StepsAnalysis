using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;
using StepsAnalysis.Model.Views;

namespace StepsAnalysis.Model.Export {
    class CsvFileExportUser : IFileSaveUser {
        public async void Save(User user, string fileName) {
            using (StreamWriter sw = new StreamWriter(fileName, false)) {
                await sw.WriteLineAsync(CsvSerializer.SerializeToString(new JsonUserView(user)));
            }
        }
    }
}
