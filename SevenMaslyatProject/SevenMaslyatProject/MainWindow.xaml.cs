using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SevenMaslyatProject;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Button myButton = new Button();
        myButton.Width = 100;
        myButton.Height = 30;
        myButton.Content = "Кепочка";
        grid1.Children.Add(myButton);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string text = textBox2.Text;
        if (text != "")
        {
            MessageBox.Show(text);
        }
    }
}