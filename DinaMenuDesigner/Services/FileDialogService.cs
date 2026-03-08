using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Text;

namespace DinaMenuDesigner.Services
{
    public class FileDialogService : IFileDialogService
    {
        public string? GetOpenFilePath(string filter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                Multiselect = false,
                Title="Chargement du menu..."
            };
            return openFileDialog.ShowDialog()  == true ? openFileDialog.FileName : null;
        }

        public string? GetSaveFilePath(string filter)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = filter,
                Title = "Sauvegarde du menu..."
            };
            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : null;
        }
    }
}
