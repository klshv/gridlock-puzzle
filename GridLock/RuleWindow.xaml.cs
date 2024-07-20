using System.Windows;

namespace GridLock; 

public partial class RuleWindow : Window {

    public RuleWindow() {
        Owner = Application.Current.MainWindow;
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
        Close();
    }

}