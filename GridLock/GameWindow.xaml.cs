using GridLock.view_model;

using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GridLock;

public partial class GameWindow {
    
    public GameWindow() {
        Owner = Application.Current.MainWindow;

        InitializeComponent();
        _levelViewModel = new LevelViewModel(1);
        DataContext = _levelViewModel;
    }   
    
    private int _currentLevel;
    private LevelViewModel _levelViewModel;
    private bool _startedDragBlock;
    private Rectangle? _currentBlock;
    private Point _previousMousePosition = new() { X = 0, Y = 0 };

    private void GameGridlock_OnLoaded(object sender, RoutedEventArgs e) {
    }

    private void RuleButton_Click(object sender, RoutedEventArgs e) {
        var ruleWindow = new RuleWindow();
        ruleWindow.Show();
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e) {
        if (_levelViewModel.MoveCount == 0) {
            return;
        }
        _levelViewModel = new LevelViewModel(_currentLevel);
        DataContext = _levelViewModel;
    }

    private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e) {
        if (sender is not Rectangle block) return;
        _startedDragBlock = true;
        _currentBlock = block;
        _previousMousePosition = GetMousePosition(e);
    }

    private void Rectangle_MouseMove(object sender, MouseEventArgs e) {
        if (!_startedDragBlock) return;
        Point currentPosition = GetMousePosition(e);
        bool moved = _levelViewModel.TryMoveBlock((_currentBlock?.DataContext as BlockViewModel)!,
            currentPosition - _previousMousePosition);
        _currentLevel = _levelViewModel.CurrentLevel;
        
        _startedDragBlock = !moved;
    }

    private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e) {
        if (!_startedDragBlock) return;
        _startedDragBlock = false;
        _currentBlock = null;
        _levelViewModel.IncreaseMoveCount();
    }

    private Point GetMousePosition(MouseEventArgs e) {
        var workspaceElement = FindName("WorkSpaceGridlock") as IInputElement;
        return e.GetPosition(workspaceElement);
    }
}