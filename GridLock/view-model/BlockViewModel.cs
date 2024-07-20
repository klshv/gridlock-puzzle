using GridLock.application;
using GridLock.data;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace GridLock.view_model {

    public enum BlockType {
        Target,
        Regular
    }

    public class BlockViewModel : INotifyPropertyChanged {
        
        private Block block;
        private readonly VisualSettingsDataService settings;
        private readonly int cellSize;
        private readonly BlockType blockType;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public BlockViewModel(Block block, BlockType type, VisualSettingsDataService settings, int cellSize) {
            this.block = block;
            this.cellSize = cellSize;
            this.settings = settings;
            blockType = type;
            Fill = type switch {
                BlockType.Target => Brushes.RoyalBlue,
                BlockType.Regular => block.Length == 2 ? Brushes.LightGreen : Brushes.Green,
                _ => throw new NotImplementedException("Specified block type is not supported!")
            };
            StrokeThickness = 1;
            FillThickness = type switch {
                BlockType.Target => Brushes.Blue,
                BlockType.Regular => block.Length == 2 ? Brushes.LimeGreen : Brushes.DarkGreen,
                _ => throw new NotImplementedException("Specified block type is not supported!")
            };
        }

        public int Top => (int)(settings.CanvasSize.Height - block.Y * cellSize);

        public int Left => block.X * cellSize;


        public Size Size =>
            block.Direction == Direction.Horizontal
                ? new Size(cellSize * block.Length, cellSize)
                : new Size(cellSize, cellSize * block.Length);

        public Brush Fill { get; }

        public Brush FillThickness { get; }

        public bool IsTarget => blockType == BlockType.Target;

        public int StrokeThickness { get; }

        public Block Model {
            get => block;
            set {
                block = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Model)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Left)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Top)));
            }
        }
    }
}