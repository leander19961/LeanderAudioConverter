using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LeanderAudioConverter.Model;
using LeanderAudioConverter.Properties;
using LeanderAudioConverter.Resource;
using LeanderAudioConverter.View;

using static LeanderAudioConverter.IO.LeanderAudioConverterFileIO;

namespace LeanderAudioConverter.View.LeanderAudioConverterViewer
{
    /// <summary>
    /// Interaktionslogik für LeanderAudioConverterView.xaml
    /// </summary>
    public partial class LeanderAudioConverterView : Window
    {
        private ModelService modelService;

        public LeanderAudioConverterView()
        {
            InitializeComponent();

            ComboBoxInputFormat.SelectedIndex = Settings.Default.inputFormatIndex;
            ComboBoxOutputFormat.SelectedIndex = Settings.Default.outputFormatIndex;
            TextBoxMaxTasks.Text = Settings.Default.maxTasks.ToString();

            string inputFormat = (ComboBoxInputFormat.SelectedItem as FileFormat).FileFormatString;
            string outputFormat = (ComboBoxOutputFormat.SelectedItem as FileFormat).FileFormatString;
            int maxTasks = int.Parse(TextBoxMaxTasks.Text);

            modelService = new ModelService(this, Settings.Default.inputPath, Settings.Default.outputPath, Settings.Default.ffmpegPath, inputFormat, outputFormat, maxTasks);
        }

        private void ButtonGetInputFiles_Click(object sender, RoutedEventArgs e)
        {
            ListViewInput.Items.Clear();

            modelService.CreateInputFiles(GetInputFiles(modelService.InputPath.Path, modelService.InputFormat));

            foreach (InputFile inputFile in modelService.GetInputFiles())
            {
                ListViewInput.Items.Add(inputFile);
            }

            ListViewInput.Items.Refresh();
        }

        private void ButtonCreateOutputFiles_Click(object sender, RoutedEventArgs e)
        {
            ListViewOutput.Items.Clear();

            modelService.CreateOutputFiles(modelService.GetInputFiles());

            foreach (OutputFile outputFile in modelService.GetOutputFiles())
            {
                ListViewOutput.Items.Add(outputFile);
            }

            ListViewOutput.Items.Refresh();
        }

        private void ButtonOpenFoldersView_Click(object sender, RoutedEventArgs e)
        {
            ViewManager.ShowFoldersView(this, modelService);
        }

        private void ButtonStartConverting_Click(object sender, RoutedEventArgs e)
        {
            if (modelService.GetInputFiles().Count == 0 || modelService.GetOutputFiles().Count == 0)
            {
                return;
            }

            ProgressBarTaskWork.IsIndeterminate = true;
            StartConverting(modelService.GetOutputFiles(), modelService.FFmpegPath.Path, modelService, modelService.TaskCounter);
        }

        public void AllTasksFinished()
        {
            ProgressBarTaskWork.IsIndeterminate = false;
        }

        private void ComboBoxInputFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modelService != null)
            {
                modelService.InputFormat = (ComboBoxInputFormat.SelectedItem as FileFormat).FileFormatString;
            }
        }

        private void ComboBoxOutputFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modelService != null)
            {
                modelService.OutputFormat = (ComboBoxOutputFormat.SelectedItem as FileFormat).FileFormatString;
            }
        }

        private void TextBoxMaxTasks_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int tmp;
            if (int.TryParse(TextBoxMaxTasks.Text + e.Text, out tmp) && (0 < tmp && tmp < 100))
            {
                e.Handled = false;
                modelService.SetTaskCounter(tmp);
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Application_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings["inputPath"] = modelService.InputPath.Path;
            settings["outputPath"] = modelService.OutputPath.Path;
            settings["ffmpegPath"] = modelService.FFmpegPath.Path;
            settings["inputFormatIndex"] = ComboBoxInputFormat.SelectedIndex.ToString();
            settings["outputFormatIndex"] = ComboBoxOutputFormat.SelectedIndex.ToString();
            settings["maxTasks"] = TextBoxMaxTasks.Text;
            SettingsIO.SaveSettings(settings);
        }
    }
}
