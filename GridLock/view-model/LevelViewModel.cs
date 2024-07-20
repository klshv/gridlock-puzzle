using GridLock.application;
using GridLock.data;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace GridLock.view_model {

    public class LevelViewModel : INotifyPropertyChanged {
        
        private ObservableCollection<BlockViewModel> blocks = null!;
        private readonly IEnumerable<GridLineViewModel> gridLines = null!;
        private int cellSize;
        private static int currentLevel;
        private Field level = null!;
        private int moveCount;
        private int scorePoint;

        public event PropertyChangedEventHandler? PropertyChanged;

        public LevelViewModel(int _currentLevel) {
            VisualSettings = new VisualSettingsDataService();

            CurrentLevel = _currentLevel;
            ScorePoint = scorePoint;
            MoveCount = 0;
            GridLines = GenerateGrid();
        }

        public int CurrentLevel {
            get => currentLevel;
            private set {
                currentLevel = value;
                MoveCount = 0;
                level = LevelDataService.LoadLevel(value);
                cellSize = (int)(VisualSettings.CanvasSize.Height / level.Height);

                List<BlockViewModel> blockViewModels = level
                    .Blocks
                    .Select(x => new BlockViewModel(x, BlockType.Regular, VisualSettings, cellSize))
                    .ToList();

                BlockViewModel targetBlock = new(level.Target, BlockType.Target, VisualSettings, cellSize);
                blockViewModels.Add(targetBlock);

                Blocks = new ObservableCollection<BlockViewModel>(blockViewModels);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLevel)));
            }
        }

        public IList<BlockViewModel> Blocks {
            get => blocks;
            private set {
                if (Equals(blocks, value)) {
                    return;
                }
                blocks = new ObservableCollection<BlockViewModel>(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Blocks)));
            }
        }

        public IEnumerable<GridLineViewModel> GridLines {
            get => gridLines;
            private init {
                if (Equals(gridLines, value)) {
                    return;
                }
                gridLines = new ObservableCollection<GridLineViewModel>(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GridLines)));
            }
        }

        public int MoveCount {
            get => moveCount;
            private set {
                if (moveCount.Equals(value)) {
                    return;
                }
                moveCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MoveCount)));
            }
        }

        public int ScorePoint {
            get => scorePoint;
            private set {
                if (scorePoint.Equals(value)) {
                    return;
                }
                scorePoint = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScorePoint)));
            }
        }

        public int ExitY1 => cellSize * 2;

        public int ExitY2 => cellSize * 3;

        public VisualSettingsDataService VisualSettings { get; }

        public bool TryMoveBlock(BlockViewModel block, Vector offset) {
            bool isHorizontal = block.Model.Direction == Direction.Horizontal;
            double delta = isHorizontal ? offset.X : offset.Y;

            Console.WriteLine(
                $"Current block: {block.Model.X}, {block.Model.Y}, {block.Model.Direction} {block.Model.Length}");
            Console.WriteLine($"Delta: {delta} ({offset})");

            if (Math.Abs(delta) < cellSize) return false;

            Block updatedBlockModel = null!;
            if (isHorizontal) {
                switch (delta) {
                    case < 0:
                        FieldService.CanMoveLeft(level, block.Model, out updatedBlockModel);
                        break;
                    case > 0:
                        FieldService.CanMoveRight(level, block.Model, out updatedBlockModel);
                        break;
                }
            } else {
                switch (delta) {
                    case < 0:
                        FieldService.CanMoveUp(level, block.Model, out updatedBlockModel);
                        break;
                    case > 0:
                        FieldService.CanMoveDown(level, block.Model, out updatedBlockModel);
                        break;
                }
            }

            if (updatedBlockModel == null) {
                return false;
            }
            
            level.Replace(block.Model, updatedBlockModel);
            block.Model = updatedBlockModel;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLevel)));

            if (!block.IsTarget || block.Model.X + block.Model.Length < level.Width) {
                return false;
            }

            ScorePoint += MoveCount < 100 * (int)Math.Ceiling(CurrentLevel / 10.0)
                ? 100 * (int)Math.Ceiling(CurrentLevel / 10.0) - MoveCount % (100 * (int)Math.Ceiling(CurrentLevel / 10.0))
                : 10 * (int)Math.Ceiling(CurrentLevel / 10.0);
            MoveCount = 0;
            CurrentLevel++;
            return true;
        }

        public void IncreaseMoveCount() {
            MoveCount++;
        }

        private IEnumerable<GridLineViewModel> GenerateGrid() {
            return GenerateHorizontalGrid()
                .Concat(GenerateVerticalGrid());
        }

        private IEnumerable<GridLineViewModel> GenerateHorizontalGrid() {
            List<GridLineViewModel> result = new();
            for (var i = 0; i < level.Height; i++) {
                double y = i * VisualSettings.CanvasSize.Height / level.Height;
                GridLineViewModel newLine = new(
                    new Point(0, y),
                    new Point(VisualSettings.CanvasSize.Width, y),
                    VisualSettings
                );
                result.Add(newLine);
            }
            return result;
        }

        private IEnumerable<GridLineViewModel> GenerateVerticalGrid() {
            List<GridLineViewModel> result = new();
            for (var i = 0; i < level.Width; i++) {
                double x = i * VisualSettings.CanvasSize.Width / level.Width;
                GridLineViewModel newLine = new(
                    new Point(x, 0),
                    new Point(x, VisualSettings.CanvasSize.Height),
                    VisualSettings
                );
                result.Add(newLine);
            }
            return result;
        }
    }
}