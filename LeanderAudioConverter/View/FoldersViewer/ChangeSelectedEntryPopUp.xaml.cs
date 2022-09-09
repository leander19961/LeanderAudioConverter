using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using LeanderAudioConverter.Model;

namespace LeanderAudioConverter.View.FoldersViewer
{
    /// <summary>
    /// Interaktionslogik für ChangeSelectedEntryPopUp.xaml
    /// </summary>
    public partial class ChangeSelectedEntryPopUp : Window
    {
        private ModelService modelService;

        private string newPath;
        private bool buttonClicked = false;

        public bool ButtonClicked
        {
            get { return buttonClicked; }
        }

        public string NewPath
        {
            get { return newPath; }
        }

        public ChangeSelectedEntryPopUp(ModelService modelService)
        {
            InitializeComponent();
            this.modelService = modelService;
        }

        private void ButtonChangeSelectedEntry_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked = true;
            newPath = TextBoxNewPath.Text;
            this.Close();
        }
    }
}
