using GridLock.data;

using System.Windows;
using System.Windows.Media;

namespace GridLock.view_model {

    public class GridLineViewModel {

        private readonly VisualSettingsDataService settings;

        public GridLineViewModel(Point start, Point end, VisualSettingsDataService settings) {
            this.settings = settings;
            Start = start;
            End = end;
        }

        public Brush Stroke => settings.Stroke;

        public int Thickness => settings.GridLineThickness;

        public Point Start { get; }

        public Point End { get; }
    }
}