// Project Snake
// Copyright © 2021 Pasha Barahimi <pashabarahimi@gmail.com>
// This program is a simple snake game
// Coded in 1/9/2021
using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake
{
    class Program
    {
        #region Functions
        /// <summary>
        /// This is an optional function which shows a progress bar before the game starts
        /// </summary>
        static void Loading()
        {
            Console.SetCursorPosition(45, 14);
            Console.Write("Let's Begin The Game!");
            Console.SetCursorPosition(44, 16);
            Console.Write("[");
            Console.SetCursorPosition(66, 16);
            Console.Write("]");
            Console.SetCursorPosition(45, 16);
            for (byte progressBar = 0; progressBar <= 20; progressBar++)
            {
                Console.CursorVisible = false;
                if (progressBar % 2 == 0)
                    Console.ForegroundColor = ConsoleColor.Black;
                if (progressBar % 2 == 1)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("█");
                Thread.Sleep(100);
            }
            Console.Clear();
        }

        /// <summary>
        /// In this function user will be able to choose the game's level
        /// </summary>
        /// <returns>Delay Value: 200ms for easy, 100ms for normal and 50ms for hard</returns>
        static int ChooseLevel()
        {
            Console.Write("Welcome to snake! Choose your level:\nPress 1 for easy\nPress 2 for normal\nPress 3 for hard\n");
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        return 200;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return 100;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        return 50;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// This function starts the game
        /// </summary>
        /// <param name="delay">Delay value returned from <see cref="ChooseLevel"/></param>
        /// /// <param name="score">User's score after losing the game</param>
        /// <returns>The game status which is whether won or lost. See also <seealso cref="GameStatus"/></returns>
        static GameStatus Snake(int delay, out int score)
        {
            // Few requirements before the game starts
            Board gameBoard = new Board();
            SnakePart head = new SnakePart(Point.ToPoint(gameBoard.Width / 2, gameBoard.Height / 2 - 1));
            gameBoard.AddSnake(head.Location);
            List<SnakePart> snakeParts = new List<SnakePart> { new SnakePart(Point.ToPoint(gameBoard.Width / 2, gameBoard.Height / 2)) };
            gameBoard.AddSnake(snakeParts[0].Location);
            SnakePart tail = new SnakePart(Point.ToPoint(gameBoard.Width / 2, gameBoard.Height / 2 + 1));
            gameBoard.AddSnake(tail.Location);
            Direction headDirection = Direction.Up;
            gameBoard.SetFruitLocation();
            while (true)
            {
                do
                {
                    GameStatus status = SnakePart.SnakeMove(head, snakeParts, tail, gameBoard, headDirection);
                    if (status != GameStatus.StillPlaying)
                    {
                        score = snakeParts.Count - 1;
                        return status; // The game status is whether won or lost
                    }

                    Thread.Sleep(delay);
                } while (!Console.KeyAvailable); // Checks if the user wants to change the direction

                switch (Console.ReadKey(true).Key) // Changing the direction if it is possible
                {
                    case ConsoleKey.UpArrow:
                        if (headDirection != Direction.Down)
                            headDirection = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        if (headDirection != Direction.Up)
                            headDirection = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (headDirection != Direction.Right)
                            headDirection = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        if (headDirection != Direction.Left)
                            headDirection = Direction.Right;
                        break;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// Console appearance when the game hasn't been  started yet or it has ended
        /// </summary>
        static void MainBoard()
        {
            Console.SetWindowSize(112, 31);
            Console.CursorVisible = true;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        /// <summary>
        /// Program starts here
        /// </summary>
        static void Main()
        {
            while (true)
            {
                Console.Title = "Snake";
                MainBoard();
                int delay = ChooseLevel();
                Console.Clear();
                Loading();
                GameStatus status = Snake(delay, out int score);
                MainBoard();
                switch (status) // This part executes when the game ends
                {
                    case GameStatus.Lost:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Beep(750, 500);
                        Console.WriteLine($"Oops you lost! Your score : {score}");
                        break;
                    case GameStatus.Won:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Congratulations! You won!!");
                        break;
                    default:
                        return; // Just to remove warning
                }
                Console.WriteLine("If you want to exit press escape. Otherwise, press any other key.");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape) // Checking whether the user wants to exit the game or replay
                    return;
            }
        }
        #endregion
    }
}
