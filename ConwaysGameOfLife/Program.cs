using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConwaysGameOfLife
{
    internal class Program
    {
        private static Board _game;
        private static Patterns _patterns;

        public static Action DoStartThings => () =>
        {
            _game.RandomiseBoard();
            //_patterns.InsertPattern(Environment.CurrentDirectory + @"\brain.txt", 20, 20);
        };

        private static void Main()
        {
            Maximize();
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            _game = new Board(39, 70);
            Console.CancelKeyPress += (sender, e) =>
            {
                _game = new Board(39, 70);
                _patterns = new Patterns(_game);
                DoStartThings();
                e.Cancel = true;
            };

            _patterns = new Patterns(_game);
            DoStartThings();
            while (true)
            {
                _game.DoGameTick();
                _game.PrintGame();
                Thread.Sleep(Constants.SleepTime);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        private static void Maximize()
        {
            var p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }
}
}