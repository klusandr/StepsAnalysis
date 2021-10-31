using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using StepsAnalysis.Model.Views;

namespace StepsAnalysis.Model.Export {
    class XmlFileExportUser : IFileSaveUser {
        XmlSerializer _serializer;

        public XmlFileExportUser() {
            _serializer = new XmlSerializer(typeof(XmlUserView));
        }

        public void Save(User user, string fileName) {
            using (StreamWriter sw = new StreamWriter(fileName, false)) {
                _serializer.Serialize(sw, new XmlUserView(user));
            }
        }
    }
}
