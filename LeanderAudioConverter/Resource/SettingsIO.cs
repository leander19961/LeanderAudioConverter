using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeanderAudioConverter.Model;
using LeanderAudioConverter.Properties;

namespace LeanderAudioConverter.Resource
{
    internal static class SettingsIO
    {
        internal static void SaveSettings(Dictionary<string, string> settings)
        {
            Settings defaultSettings = Settings.Default;
            defaultSettings.inputPath = settings["inputPath"];
            defaultSettings.outputPath = settings["outputPath"];
            defaultSettings.ffmpegPath = settings["ffmpegPath"];

            try
            {
                defaultSettings.inputFormatIndex = int.Parse(settings["inputFormatIndex"]);
                defaultSettings.outputFormatIndex = int.Parse(settings["outputFormatIndex"]);
            }
            catch
            {
                defaultSettings.inputFormatIndex = 0;
                defaultSettings.outputFormatIndex = 0;
            }

            defaultSettings.Save();
        }
    }
}
