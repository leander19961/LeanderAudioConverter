using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LeanderAudioConverter.Model;

namespace LeanderAudioConverter.IO
{
    internal static class LeanderAudioConverterFileIO
    {
        internal static List<Dictionary<string, string>> GetInputFiles(string inputPath, string inputFormat)
        {
            List<Dictionary<string, string>> inputFiles = new List<Dictionary<string, string>>();

            foreach (string file in Directory.GetFiles(inputPath, $"*.{inputFormat}", SearchOption.AllDirectories))
            {
                Dictionary<string, string> inputFile = new Dictionary<string, string>();
                inputFile["name"] = Path.GetFileName(file.Replace($".{inputFormat}", ""));
                inputFile["path"] = file;
                inputFiles.Add(inputFile);
            }

            return inputFiles;
        }

        internal static void StartConverting(List<OutputFile> outputFiles, string ffmpegPath, ModelService modelService, Semaphore taskCounter)
        {
            List<Task> tasks = new List<Task>();

            foreach (OutputFile outputfile in outputFiles)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    modelService.TaskCounter.WaitOne();

                    ProcessStartInfo ffmpegProcessStartInfo = new ProcessStartInfo(ffmpegPath, $"-i \"{Path.GetFullPath(outputfile.GetInputFile().Path)}\" -y \"{Path.GetFullPath(outputfile.Path)}\"")
                    {
                        CreateNoWindow = true,
                    };

                    Process ffmpegProcess = new Process();
                    ffmpegProcess.StartInfo = ffmpegProcessStartInfo;
                    ffmpegProcess.Start();
                    ffmpegProcess.WaitForExit();
                });

                task.GetAwaiter().OnCompleted(() => TaskFinished(modelService, tasks));
                tasks.Add(task);
            }
        }

        internal static void TaskFinished(ModelService modelService, List<Task> tasks)
        {
            modelService.TaskCounter.Release();

            bool allTasksFinished = true;
            foreach (Task task in tasks)
            {
                if (!task.IsCompleted)
                {
                    allTasksFinished = false;
                }
            }

            if (allTasksFinished)
            {
                modelService.TasksFinished();
            }
        }
    }
}
