using System;
using System.Diagnostics;
using System.Text;

namespace ConwaysGameOfLife
{
    public class Board
    {
        private readonly int _maxX;
        private readonly int _maxY;
        private long _frameDelta;
        private readonly Stopwatch _gameTickStopwatch = new Stopwatch();
        public BoardArray BoardArray;

        public Board(int maxX, int maxY)
        {
            _maxY = maxY;
            _maxX = maxX;

            BoardArray = InitializeNewBoard(_maxX, _maxY);
        }

        private int LiveNeighbors(int x, int y)
        {
            var liveNeighbors = 0;

            // Tests all neighbors
            for (var testX = x - 1; testX <= x + 1; testX++)
            {
                // Above
                if (BoardArray[testX, y + 1]) liveNeighbors++;


                // Below
                if (BoardArray[testX, y - 1]) liveNeighbors++;


                // Beside, ignores the inspected coordinate
                if (BoardArray[testX, y] && testX != x) liveNeighbors++;
            }

            return liveNeighbors;
        }

        public void RandomiseBoard()
        {
            var random = new Random();
            for (var x = 0; x < BoardArray.Length; x++)
            for (var y = 0; y < BoardArray[x].Length; y++)
                BoardArray[x, y] = random.NextDouble() > 0.5;
        }

        private BoardArray InitializeNewBoard(int x, int y)
        {
            // Initializes the jagged array
            var board = new bool[x][];

            // Creates each array item
            for (var row = 0; row < x; row++)
                board[row] = new bool[y];
            return new BoardArray(board);
        }

        public void DoGameTick()
        {
            _gameTickStopwatch.Restart();
            var newBoard = InitializeNewBoard(_maxX, _maxY);
            for (var x = 0; x < BoardArray.Length; x++)
            for (var y = 0; y < BoardArray[x].Length; y++)
            {
                var liveNeighbors = LiveNeighbors(x, y);
                if (BoardArray[x, y])
                {
                    if (liveNeighbors < 2)
                        newBoard[x, y] = false;
                    else if (liveNeighbors == 2 || liveNeighbors == 3)
                        newBoard[x, y] = true;
                    else if (liveNeighbors > 3) newBoard[x, y] = false;
                }
                else if (liveNeighbors == 3)
                {
                    newBoard[x, y] = true;
                }
            }

            BoardArray = newBoard;
            _gameTickStopwatch.Stop();
            _frameDelta = _gameTickStopwatch.ElapsedTicks;
        }

        public void PrintGame()
        {
            var stringBuilder = new StringBuilder();
            for (var y = 0; y < BoardArray[0].Length; y++)
            {
                for (var x = 0; x < BoardArray.Length; x++)
                    stringBuilder.Append(BoardArray[x, y] ? Constants.Alive : Constants.Dead);
                stringBuilder.Append(Environment.NewLine);
            }

            stringBuilder.Append(Environment.NewLine + $"Percent Alive: {GetPercentAlive()}%");

            stringBuilder.Append(Environment.NewLine +
                                 $"{(int) (1 / new TimeSpan(_frameDelta + (long) Constants.SleepTime * Constants.MillisecondsToTickRatio).TotalSeconds)} FPS");


            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
        }

        private decimal GetPercentAlive()
        {
            var alive = 0;
            var total = 0;
            for (var x = 0; x < BoardArray.Length; x++)
            for (var y = 0; y < BoardArray[0].Length; y++)
            {
                total++;
                if (BoardArray[x, y]) alive++;
            }

            return decimal.Round(alive / (decimal) total * 100, 2);
        }

        public void PrintGameSlow()
        {
            var output = "";
            for (var y = 0; y < BoardArray[0].Length; y++)
            {
                for (var x = 0; x < BoardArray.Length; x++)
                    output += BoardArray[x, y] ? Constants.Alive : Constants.Dead;
                output += Environment.NewLine;
            }

            Console.WriteLine(output);
        }
    }
}