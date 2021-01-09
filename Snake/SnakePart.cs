using System.Collections.Generic;

namespace Snake
{
    public class SnakePart
    {
        #region Properties
        public Point Location { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor executes when a new <see cref="SnakePart"/> is made
        /// </summary>
        /// <param name="location">A <see cref="Point"/> struct which shows the snake part coordinates</param>
        public SnakePart(Point location)
        {
            Location = location;
        }
        #endregion

        #region Functions
        /// <summary>
        /// This function executes each time the snake moves
        /// </summary>
        /// <param name="head">A <see cref="SnakePart"/> which is initialized as the head of the head snake</param>
        /// <param name="snakeParts">A list of <see cref="SnakePart"/> objects which contains all the snake's body except the head and the tail</param>
        /// <param name="tail">A <see cref="SnakePart"/> which is initialized as the tail of the head snake</param>
        /// <param name="gameBoard">The <see cref="Board"/> which is made in the beginning of the game</param>
        /// <param name="headDirection">The next snake's <see cref="Direction"/> which is either left, right, up or down</param>
        /// <returns>The <see cref="GameStatus"/> which is either still playing, won or lost</returns>
        public static GameStatus SnakeMove(SnakePart head, List<SnakePart> snakeParts, SnakePart tail, Board gameBoard, Direction headDirection)
        {
            Point headTemp = head.Location;
            Point newPoint; // Next head's location if possible
            switch (headDirection)
            {
                case Direction.Up:
                    newPoint = Point.ToPoint(head.Location.X, head.Location.Y == 0 ? gameBoard.Height - 1 : head.Location.Y - 1);
                    break;
                case Direction.Down:
                    newPoint = Point.ToPoint(head.Location.X, head.Location.Y == gameBoard.Height - 1 ? 0 : head.Location.Y + 1);
                    break;
                case Direction.Left:
                    newPoint = Point.ToPoint(head.Location.X == 0 ? gameBoard.Width - 1 : head.Location.X - 1, head.Location.Y);
                    break;
                case Direction.Right:
                    newPoint = Point.ToPoint(head.Location.X == gameBoard.Width - 1 ? 0 : head.Location.X + 1, head.Location.Y);
                    break;
                default:
                    newPoint = Point.ToPoint(0, 0); // Just to remove the warning
                    break;
            }

            if (!gameBoard.FreePoints.Contains(newPoint)) // Checks if the snake's head touched its body
                return GameStatus.Lost;
            // Moving the snake in the console
            head.Location = newPoint;
            gameBoard.AddSnake(newPoint);
            Point tailTemp = snakeParts[snakeParts.Count - 1].Location;
            for (int i = snakeParts.Count - 1; i > 0; i--)
                snakeParts[i].Location = snakeParts[i - 1].Location;
            snakeParts[0].Location = headTemp;
            if (head.Location.Equals(gameBoard.Fruit)) // The snake ate the fruit
            {
                snakeParts.Add(new SnakePart(tailTemp)); // Resizing the snake after eating the fruit
                if (snakeParts.Count == gameBoard.Height * gameBoard.Width - 2) // The screen is full so the user wins
                    return GameStatus.Won;
                gameBoard.SetFruitLocation(); // Putting another fruit in the console
            }
            else
            {
                gameBoard.RemoveSnake(tail.Location);
                tail.Location = tailTemp;
            }

            return GameStatus.StillPlaying;
        }
        #endregion
    }
}