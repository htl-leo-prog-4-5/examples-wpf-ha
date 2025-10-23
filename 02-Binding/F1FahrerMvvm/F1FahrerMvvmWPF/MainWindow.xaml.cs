using System.Windows;

namespace F1FahrerMvvmWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var window = new F1FahrerMvvmWPF.Views.F1Driver();
        window.ShowDialog();
    }
}