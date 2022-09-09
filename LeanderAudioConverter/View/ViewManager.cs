using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LeanderAudioConverter.Model;
using LeanderAudioConverter.View.FoldersViewer;

namespace LeanderAudioConverter.View
{
    public static class ViewManager
    {
        public static FoldersView ShowFoldersView(Window owner, ModelService modelService)
        {
            FoldersView foldersView = new FoldersView(modelService)
            {
                Owner = owner,
            };

            foldersView.Show();

            return foldersView;
        }

        public static ChangeSelectedEntryPopUp ShowChangeSelectedEntryPopUp(Window owner, ModelService modelService)
        {
            ChangeSelectedEntryPopUp changeSelectedEntryPopUp = new ChangeSelectedEntryPopUp(modelService)
            {
                Owner = owner,
            };

            changeSelectedEntryPopUp.ShowDialog();

            return changeSelectedEntryPopUp;
        }
    }
}
