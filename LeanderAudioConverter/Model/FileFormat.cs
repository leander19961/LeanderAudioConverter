using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanderAudioConverter.Model
{
    internal class FileFormat
    {
        private string fileFormatString;

        public string FileFormatString
        {
            set { fileFormatString = value; }
            get { return fileFormatString; }
        }

        override
        public string ToString()
        {
            return fileFormatString;
        }
    }
}
