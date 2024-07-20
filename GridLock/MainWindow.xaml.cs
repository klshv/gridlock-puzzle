using System.Windows;

namespace GridLock {

    public partial class MainWindow {

        public MainWindow() {
            InitializeComponent();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e) {
            var gameWindow = new GameWindow();
            gameWindow.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

       private void RuleButton_Click(object sender, RoutedEventArgs e) {
            var ruleWindow = new RuleWindow();
            ruleWindow.Show();
        }
    }
}