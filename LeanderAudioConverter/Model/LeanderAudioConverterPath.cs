using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanderAudioConverter.Model
{
    public class LeanderAudioConverterPath
    {
        private string name;
        private string path;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public LeanderAudioConverterPath(string name, string path)
        {
            this.name = name;
            this.path = path;
        }
    }
}
