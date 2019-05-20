namespace ConwaysGameOfLife
{
    public class BoardArray
    {
        public BoardArray(bool[][] board) => Board = board;

        private bool[][] Board { get; }

        // Use modulo math to allow for unlimited indexes
        public bool[] this[int x]
        {
            get => Board[(x + Length) % Length];
            set => Board[(x + Length) % Length] = value;
        }
        
        public bool this[int x, int y]
        {
            get
            {
                x += Length;
                y += this[x].Length;
                return Board[x % Length][y % this[x].Length];
            }
            set
            {
                x += Length;
                y += this[x].Length;
                Board[x % Length][y % this[x].Length] = value;
            }
        }

        public int Length => Board.Length;
    }
}