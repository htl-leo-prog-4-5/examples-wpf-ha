namespace Wpf;

using System.IO;
using System.Windows;
using Base.WpfMvvm;
using Microsoft.Win32;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }

    public string? ShowFileOpenDialog()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Drawing Files (*.txt)|*.txt";
        openFileDialog.DefaultDirectory = Directory.GetCurrentDirectory();
        if (openFileDialog.ShowDialog() == false)
        {
            return null;
        }

        return openFileDialog.FileName;
    }
}