using System.Windows;
using MacWpfTheme.Helpers;

namespace MacWpfTheme.Demo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => MacWindowHelper.Bind(this);
    }
}
