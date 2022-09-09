using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanderAudioConverter.Model
{
    public class InputFile
    {
        private string name;
        private string path;

        private OutputFile outputFile;

        public string Name
        {
            get { return name; }
        }
        public string Path
        {
            get { return path; }
        }

        public InputFile(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public OutputFile GetOutputFile()
        {
            return outputFile;
        }

        public InputFile SetOutputFile(OutputFile value)
        {
            if (this.outputFile == null)
            {
                outputFile = value;
                outputFile.SetInputFile(this);
            }

            return this;
        }
    }
}
