using System;
using System.Collections.Generic;
using System.Linq;

namespace GridLock.application {

    internal class FieldService {

        public static void CanMoveLeft(Field field, Block inputBlock, out Block newBlock) {
            Block testBlock = BlockService.GetBlockMovedLeft(inputBlock);
            Console.WriteLine($"Left from {inputBlock.X} to {testBlock.X}");
            if (testBlock.X < 0) {
                newBlock = null!;
                return;
            }

            if (GetBlocksExceptOf(field, inputBlock).Any(block => testBlock.Intersects(block))) {
                newBlock = null!;
                return;
            }

            newBlock = testBlock;
        }

        public static void CanMoveRight(Field field, Block inputBlock, out Block newBlock) {
            Block testBlock = BlockService.GetBlockMovedRight(inputBlock);
            Console.WriteLine($"Right from {inputBlock.X} to {testBlock.X}");
            if (testBlock.X + testBlock.Length - 1 >= field.Width) {
                newBlock = null!;
                return;
            }

            if (GetBlocksExceptOf(field, inputBlock).Any(block => testBlock.Intersects(block))) {
                newBlock = null!;
                return;
            }

            newBlock = testBlock;
        }

        public static void CanMoveDown(Field field, Block inputBlock, out Block newBlock) {
            Block testBlock = BlockService.GetBlockMovedDown(inputBlock);
            Console.WriteLine($"Down from {inputBlock.Y} to {testBlock.Y}");
            if (testBlock.Y - testBlock.Length < 0) {
                newBlock = null!;
                return;
            }

            if (GetBlocksExceptOf(field, inputBlock).Any(block => testBlock.Intersects(block))) {
                newBlock = null!;
                return;
            }

            newBlock = testBlock;
        }

        public static void CanMoveUp(Field field, Block inputBlock, out Block newBlock) {
            Block testBlock = BlockService.GetBlockMovedUp(inputBlock);
            Console.WriteLine($"Up from {inputBlock.Y} to {testBlock.Y}");
            if (testBlock.Y > field.Height) {
                newBlock = null!;
                return;
            }

            if (GetBlocksExceptOf(field, inputBlock).Any(block => testBlock.Intersects(block))) {
                newBlock = null!;
                return;
            }

            newBlock = testBlock;
        }

        private static IEnumerable<Block> GetBlocksExceptOf(Field field, Block excludeBlock) {
            return field.Blocks.Where(b => !b.Equals(excludeBlock));
        }
    }
}