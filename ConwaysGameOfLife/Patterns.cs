using System;
using System.IO;

namespace ConwaysGameOfLife
{
    public class Patterns
    {
        private readonly Board _board;

        public Patterns(Board board)
        {
            _board = board;
        }

        public void InsertPattern(string path, int x, int y)
        {
            var pattern = File.ReadAllLines(path);
            var charLines = Array.ConvertAll(pattern, z => z.ToCharArray());
            InsertPattern(x, y, charLines);
        }

        public void InsertPattern(int x, int y, char[][] data)
        {
            for (var insertX = 0; insertX < data.Length; insertX++)
            for (var insertY = 0; insertY < data[insertX].Length; insertY++)
            {
                var result = data[insertX][insertY] == '*';
                _board.BoardArray[x + insertX, y + insertY] = result;
            }
        }

        public void InsertGlider(int x, int y)
        {
            var glider = new[]
            {
                new[] {' ', 'x', ' '},
                new[] {' ', ' ', 'x'},
                new[] {'x', 'x', 'x'}
            };
            InsertPattern(x, y, glider);
        }
    }
}