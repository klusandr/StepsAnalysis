using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepsAnalysis.Model {
    public interface IFileSaveUser {
        public void Save(User user, string fileName);
    }
}
