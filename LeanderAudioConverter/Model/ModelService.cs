using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LeanderAudioConverter.View.LeanderAudioConverterViewer;

namespace LeanderAudioConverter.Model
{
    public class ModelService
    {
        private LeanderAudioConverterView mainWindow;

        private Semaphore taskCounter;

        private List<InputFile> inputFiles;
        private List<OutputFile> outputFiles;

        private LeanderAudioConverterPath inputPath;
        private LeanderAudioConverterPath outputPath;
        private LeanderAudioConverterPath ffmpegPath;

        private string inputFormat;
        private string outputFormat;

        public Semaphore TaskCounter
        {
            get { return taskCounter; }
        }

        public LeanderAudioConverterPath InputPath
        {
            get { return inputPath; }
        }
        public LeanderAudioConverterPath OutputPath
        {
            get { return outputPath; }
        }
        public LeanderAudioConverterPath FFmpegPath
        {
            get { return ffmpegPath; }
        }

        public string InputFormat
        {
            set { inputFormat = value; }
            get { return inputFormat; }
        }
        public string OutputFormat
        {
            set { outputFormat = value; }
            get { return outputFormat; }
        }

        public ModelService(LeanderAudioConverterView mainWindow, string inputPath, string outputPath, string ffmpegPath, string inputFormat, string outputFormat, int maxTasks)
        {
            this.mainWindow = mainWindow;

            this.taskCounter = new Semaphore(maxTasks, maxTasks);

            this.inputPath = new LeanderAudioConverterPath("inputPath", inputPath);
            this.outputPath = new LeanderAudioConverterPath("outputPath", outputPath);
            this.ffmpegPath = new LeanderAudioConverterPath("ffmpegPath", ffmpegPath);

            this.inputFormat = inputFormat;
            this.outputFormat = outputFormat;

            inputFiles = new List<InputFile>();
            outputFiles = new List<OutputFile>();
        }

        public void ChangePath(LeanderAudioConverterPath leanderAudioConverterPath, string newPath)
        {
            leanderAudioConverterPath.Path = newPath.Replace("\"", "");
        }

        public List<InputFile> GetInputFiles()
        {
            return inputFiles;
        }

        public List<OutputFile> GetOutputFiles()
        {
            return outputFiles;
        }

        public void CreateInputFile(string name, string path)
        {
            if (!InputFileExists(name))
            {
                InputFile inputFile = new InputFile(name, path);

                inputFiles.Add(inputFile);
            }
        }

        public void CreateInputFiles(List<Dictionary<string, string>> files)
        {
            foreach (Dictionary<string, string> file in files)
            {
                string name = file["name"];
                string path = file["path"];

                if (name != null && path != null)
                {
                    CreateInputFile(name, path);
                }
            }
        }

        private bool InputFileExists(string name)
        {
            bool result = false;

            foreach (InputFile inputFile in inputFiles)
            {
                if (inputFile.Name.Equals(name))
                {
                    result = true;
                }
            }

            return result;
        }

        public void CreateOutputFile(string name, string path, InputFile flacFile)
        {
            OutputFile outputFile = new OutputFile(name, path);

            outputFile.SetInputFile(flacFile);

            outputFiles.Add(outputFile);
        }

        public void CreateOutputFiles(List<InputFile> files)
        {
            outputFiles.Clear();

            foreach (InputFile inputFile in files)
            {
                string fileName = $"{Path.GetFileName(inputFile.Name)}";
                string fileDirectory = Path.GetDirectoryName(inputFile.Path);
                string interpretDirectory = Path.GetDirectoryName(fileDirectory);
                fileDirectory = fileDirectory.Substring(fileDirectory.LastIndexOf('\\'));
                interpretDirectory = interpretDirectory.Substring(interpretDirectory.LastIndexOf("\\"));
                string outputDirectory = $"{outputPath.Path}{interpretDirectory}{fileDirectory}";
                string filePath = $"{outputDirectory}\\{fileName}.{outputFormat}";

                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                if (fileName != null && filePath != null)
                {
                    CreateOutputFile(fileName, filePath, inputFile);
                }
            }
        }

        private bool OutputFileExists(string name)
        {
            bool result = false;

            foreach (OutputFile outputFile in outputFiles)
            {
                if (outputFile.Name.Equals(name))
                {
                    result = true;
                }
            }

            return result;
        }

        public void TasksFinished()
        {
            mainWindow.AllTasksFinished();
        }
    }
}
