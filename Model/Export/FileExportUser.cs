using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.Model.Export {
    class FileExportUser : IFileSaveUser {
        private Dictionary<string, IFileSaveUser> _saves;

        public FileExportUser() {
            _saves = new();
            _saves.Add("json", new JsonFileExportUser());
            _saves.Add("xml", new XmlFileExportUser());
            _saves.Add("csv", new CsvFileExportUser());
        }

        public void Save(User user, string fileName) {
            try {
                _saves[GetFileFormat(fileName)].Save(user, fileName);
            } catch (KeyNotFoundException) {
                throw new ArgumentNullException("Error savefile format.");
            }
        }

        private string GetFileFormat(string fileName) {
            return fileName.Substring(fileName.LastIndexOf('.')+1);
        }
    }
}
