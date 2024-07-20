using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GridLock.application {

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Direction {
        Horizontal,
        Vertical
    }

    public record Block {
        public Block(int length, Direction direction, int x, int y) {
            Length = length;
            Direction = direction;
            X = x;
            Y = y;
        }

        public int Length { get; init; }
        public Direction Direction { get; init; }
        public int X { get; init; }
        public int Y { get; init; }

        public bool Intersects(Block block2) {
            IEnumerable<(int, int)> coordinates1 = ToCoordinates();
            IEnumerable<(int, int)> coordinates2 = block2.ToCoordinates();

            IEnumerable<(int, int)> valueTuples = coordinates1.ToList();
            bool intersects = valueTuples.Any(point1 => coordinates2.Any(point2 => point1 == point2));
            Console.WriteLine(
                $"{string.Join(",", valueTuples.Select(x => x.ToString()))} intersects {string.Join(",", coordinates2.Select(x => x.ToString()))} ? {intersects}");

            return intersects;
        }

        private IEnumerable<(int, int)> ToCoordinates() {
            var coordinates = new List<(int, int)>();
            for (var i = 0; i < Length; i++) {
                coordinates.Add(Direction == Direction.Horizontal ? (X + i, Y) : (X, Y - i));
            }
            return coordinates;
        }
    }
}
