using System.Collections.Generic;
using System.Linq;

namespace GridLock.application {

    public class Field {

        public Field(Block target, IEnumerable<Block> blocks, int height, int width) {
            Target = target;
            Blocks = blocks;
            Height = height;
            Width = width;
        }

        public void Replace(Block from, Block to) {
            Blocks = Blocks.Where(x => !x.Equals(from)).Append(to);
        }

        public IEnumerable<Block> Blocks { get; private set; }
        public Block Target { get; }
        public int Height { get; }
        public int Width { get; }
    }
}
