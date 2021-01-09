namespace Snake
{
    public struct Point
    {
        public int X;
        public int Y;

        /// <summary>
        /// Converts the x and y coordinates to a <see cref="Point"/> struct
        /// </summary>
        /// <param name="xPoint">The x coordinate</param>
        /// <param name="yPoint">The y coordinate</param>
        /// <returns>The <see cref="Point"/> with the given coordinates</returns>
        public static Point ToPoint(int xPoint, int yPoint)
        {
            Point newPoint;
            newPoint.X = xPoint;
            newPoint.Y = yPoint;
            return newPoint;
        }
    }

}