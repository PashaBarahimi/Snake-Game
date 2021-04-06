using System;
using System.Collections.Generic;

namespace Snake
{
    public class Board
    {
        #region Properties
        private const ConsoleColor ForeGroundColor = ConsoleColor.DarkGreen;
        public List<Point> FreePoints = new List<Point>();
        private readonly Random _rnd;
        public int Height { get; }
        public int Width { get; }
        public Point Fruit { private set; get; }
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor is executed when a new <see cref="Board"/> is made
        /// </summary>
        public Board()
        {
            Height = 20;
            Width = 50;
            Console.SetWindowSize(Width, Height);
            Console.CursorVisible = false;
            Console.ForegroundColor = ForeGroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    FreePoints.Add(Point.ToPoint(i, j)); // Adding all the console's existing points to the free points list
            _rnd = new Random();
        }
        #endregion

        #region Functions
        /// <summary>
        /// This function puts a fruit in a free point which is a <see cref="Point"/> chosen randomly from <see cref="FreePoints"/>
        /// </summary>
        public void SetFruitLocation()
        {
            int position = _rnd.Next(0, FreePoints.Count);
            Fruit = FreePoints[position];
            Console.SetCursorPosition(Fruit.X, Fruit.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("▒");
            Console.ForegroundColor = ForeGroundColor;
        }

        /// <summary>
        /// This function prints a new snake part in the console
        /// </summary>
        /// <param name="location">This is a <see cref="Point"/> which shows where the snake part should be printed</param>
        public void AddSnake(Point location)
        {
            Console.SetCursorPosition(location.X, location.Y);
            Console.Write("█");
            FreePoints.Remove(location);
        }

        /// <summary>
        /// This function removes a snake part from the console
        /// </summary>
        /// <param name="location">This is a <see cref="Point"/> that gives the coordinates of the snake part that should be removed</param>
        public void RemoveSnake(Point location)
        {
            Console.SetCursorPosition(location.X, location.Y);
            Console.Write(" ");
            FreePoints.Add(location);
        }
        #endregion
    }
}