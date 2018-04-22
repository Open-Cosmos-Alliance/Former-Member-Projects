using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Programs.Games
{
    // Snake game from Witchcraft OS
    public static class Snake
    {
        public static int score;
        public static int count;
        public static int[] x; // rows
        public static int[] y; // cols
        public enum EDirection : int { none, left, right, up, down }
        public static EDirection direction;
        public static bool lose;
        public static void Start()
        {
            score = 0;
            count = 0;
            lose = false;
            direction = EDirection.none;
            x = new int[] { Console.WindowWidth / 2 };
            y = new int[] { Console.WindowHeight / 2 };
            do
            {
                // GameLoop
                {
                    // Read the key
                    ConsoleKey key = Console.ReadKey().Key;
                    // Set the direction
                    if (key == ConsoleKey.UpArrow) direction = EDirection.up;
                    else if (key == ConsoleKey.DownArrow) direction = EDirection.down;
                    else if (key == ConsoleKey.LeftArrow) direction = EDirection.left;
                    else if (key == ConsoleKey.RightArrow) direction = EDirection.right;
                    // Up
                    if (direction == EDirection.up)
                    {
                        y[y.Length] = y[y.Length - 1] - 1;
                        if (y[y.Length - 1] == 0) lose = true;
                    }
                    // Down
                    else if (direction == EDirection.down)
                    {
                        y[y.Length] = y[y.Length - 1] + 1;
                        if (y[y.Length - 1] == Console.WindowHeight) lose = true;
                    }
                    // Left
                    else if (direction == EDirection.left)
                    {
                        x[x.Length] = x[x.Length - 1] - 1;
                        if (x[x.Length - 1] == 0) lose = true;
                    }
                    // Right
                    else if (direction == EDirection.right)
                    {
                        x[x.Length] = x[x.Length - 1] + 1;
                        if (x[x.Length - 1] == Console.WindowWidth) lose = true;
                    }
                    // Clear screen
                    Console.Clear();
                    // Draw X positions
                    foreach (int posx in x)
                    {
                        Console.CursorLeft = posx;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("#");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    // Draw Y position
                    foreach (int posy in y)
                    {
                        Console.CursorTop = posy;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("#");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    // Check if count is >= 8
                    if (count >= 8)
                    {
                        // Remove first X pos
                        x[count] = 0;
                        // Remove first Y pos
                        y[count] = 0;
                    }
                    // Increment count
                    if (direction != EDirection.none) count++;
                    // Increment score
                    if (direction != EDirection.none) score += 2;
                }
            }
            while (!lose);
            Console.Clear();
            Console.CursorTop = (Console.WindowWidth / 2) - 2;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("YOU LOSE!");
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.CursorTop++;
            Console.WriteLine("Your score: " + score.ToString());
        }
    }
}
