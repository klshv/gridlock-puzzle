namespace GridLock.application {

    public class BlockService {

        public static Block GetBlockMovedLeft(Block inputBlock) {
            if (inputBlock.Direction == Direction.Horizontal)
                return inputBlock with { X = inputBlock.X - 1 };
            return inputBlock;
        }

        public static Block GetBlockMovedRight(Block inputBlock) {
            if (inputBlock.Direction == Direction.Horizontal)
                return new Block(inputBlock.Length, inputBlock.Direction, inputBlock.X + 1, inputBlock.Y);
            return inputBlock;
        }

        public static Block GetBlockMovedDown(Block inputBlock) {
            if (inputBlock.Direction == Direction.Vertical)
                return new Block(inputBlock.Length, inputBlock.Direction, inputBlock.X, inputBlock.Y - 1);
            return inputBlock;
        }

        public static Block GetBlockMovedUp(Block inputBlock) {
            if (inputBlock.Direction == Direction.Vertical)
                return inputBlock with { Y = inputBlock.Y + 1 };
            return inputBlock;
        }
    }
}