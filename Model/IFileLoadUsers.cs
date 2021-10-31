using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.Model {
    public interface IFileLoadUsers {

        public List<User> Load(string fileName);
        public List<User> Load(string[] fileNames);
    }
}
