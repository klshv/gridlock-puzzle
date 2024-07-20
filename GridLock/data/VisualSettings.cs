using System.Windows;
using System.Windows.Media;

namespace GridLock.data {

    public class VisualSettingsDataService {

        public int GridLineThickness => 0;

        public Brush Stroke => Brushes.LightYellow;

        public Size CanvasSize => new(500, 500);

        public int BoundaryThickness => 10;

        public Brush BoundaryStroke => Brushes.Gray;

        public Brush BoundaryStrokeExit => Brushes.Blue;

        public int BoundaryThicknessExit => 5;
    }
}