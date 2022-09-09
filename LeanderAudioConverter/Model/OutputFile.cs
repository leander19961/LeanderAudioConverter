using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanderAudioConverter.Model
{
    public class OutputFile
    {
        private string name;
        private string path;

        private InputFile inputFile;

        public string Name
        {
            get { return name; }
        }
        public string Path
        {
            get { return path; }
        }

        public OutputFile(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public InputFile GetInputFile()
        {
            return inputFile;
        }

        public OutputFile SetInputFile(InputFile value)
        {
            if (this.inputFile == null)
            {
                inputFile = value;
                inputFile.SetOutputFile(this);
            }

            return this;
        }
    }
}
