
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LeanderAudioConverter.Model;

using static LeanderAudioConverter.View.ViewManager;

namespace LeanderAudioConverter.View.FoldersViewer
{
    /// <summary>
    /// Interaktionslogik für FoldersView.xaml
    /// </summary>
    public partial class FoldersView : Window
    {
        private ModelService modelService;

        public FoldersView(ModelService modelService)
        {
            InitializeComponent();
            this.modelService = modelService;

            ListViewPath.Items.Add(modelService.InputPath);
            ListViewPath.Items.Add(modelService.OutputPath);
            ListViewPath.Items.Add(modelService.FFmpegPath);
        }

        private void ButtonChangeSelectedEntry_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedEntryPopUp popUp = ShowChangeSelectedEntryPopUp(this, modelService);

            if (!popUp.ButtonClicked)
            {
                return;
            }

            if (popUp.NewPath != null)
            {
                popUp.NewPath.Replace("\"", "");
            }

            if (!Directory.Exists(popUp.NewPath) && !File.Exists(popUp.NewPath))
            {
                return;
            }

            modelService.ChangePath(ListViewPath.SelectedItem as LeanderAudioConverterPath, popUp.NewPath);
            ListViewPath.Items.Refresh();
        }
    }
}
