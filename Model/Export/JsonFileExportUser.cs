using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using StepsAnalysis.Model.Views;

namespace StepsAnalysis.Model.Export {
    class JsonFileExportUser : IFileSaveUser {
        public async void Save(User user, string fileName) {
            using (StreamWriter sw = new StreamWriter(fileName, false)) {
                await sw.WriteLineAsync(JsonSerializer.Serialize(new JsonUserView(user), new JsonSerializerOptions {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                }));
            }
        }
    }
}
