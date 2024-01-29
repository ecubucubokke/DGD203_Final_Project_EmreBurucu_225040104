using System;
using System.Threading;

namespace TextBasedGame
{
    class Program
    {
        static char[,] map = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        static int playerX = 1;
        static int playerY = 1;
        static bool timeIsUp = false;

        static void Main()
        {
            // Set coordinates for special locations
            int textX = 1, textY = 2;
            int goblinX = 2, goblinY = 2;

            while (true)
            {
                PrintMap();
                Console.WriteLine("Welcome to the Text-Based Adventure Game!");
                Console.Write("Enter a direction (N, S, E, W) or 'exit' to quit: ");
                string input = Console.ReadLine().ToUpper();

                if (input == "EXIT")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                MovePlayer(input);

                if (playerX == textX && playerY == textY)
                {
                    Console.WriteLine("You found a mysterious text!  (The answer to the riddle is a computer part)");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    // some information or clue.
                }

                if (playerX == goblinX && playerY == goblinY)
                {
                    Console.WriteLine("You encountered a goblin!");
                    AskRiddleWithTimer();
                    break;  // End the game after answering the riddle correctly
                }
            }
        }

        static void PrintMap()
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == playerX && j == playerY)
                    {
                        Console.Write("[P] "); // Player
                    }
                    else if (i == 1 && j == 2)
                    {
                        Console.Write("[T] "); // Text
                    }
                    else if (i == 2 && j == 2)
                    {
                        Console.Write("[G] "); // Goblin
                    }
                    else
                    {
                        Console.Write($"[{map[i, j]}] ");
                    }
                }
                Console.WriteLine();
            }

            // Adding a delay to give the player time to read the map
            Thread.Sleep(1000);
        }

        static void MovePlayer(string direction)
        {
            switch (direction)
            {
                case "N":
                    if (playerX > 0) playerX--;
                    break;
                case "S":
                    if (playerX < 2) playerX++;
                    break;
                case "E":
                    if (playerY < 2) playerY++;
                    break;
                case "W":
                    if (playerY > 0) playerY--;
                    break;
                default:
                    Console.WriteLine("Invalid direction. Please enter N, S, E, or W.");
                    break;
            }
        }

        static void AskRiddleWithTimer()
        {
            Console.WriteLine("The goblin asks you a riddle:");
            Console.WriteLine("I have keys but no locks. I have space but no room. You can enter, but you can't go inside. What am I?");
            Console.WriteLine("Choose the correct answer:");

            string[] options = { "A) A keyboard", "B) A mouse", "C) A door", "D) A car", "E) A book" };

            foreach (var option in options)
            {
                Console.WriteLine(option);
            }

            Console.Write("10 seconds to answer (enter the letter): ");
            string answer = "";

            using (var timer = new Timer(TimerCallback, null, 10000, Timeout.Infinite))
            {
                
                var readInputThread = new Thread(() => { answer = Console.ReadLine()?.ToUpper(); });
                readInputThread.Start();

                
                readInputThread.Join();

               
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            if (!timeIsUp)
            {
                if (answer != null && answer == "A")
                {
                    Console.WriteLine("Correct! The goblin is impressed. You won!");
                    Console.WriteLine("Press enter to exit...");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Wrong answer! The goblin kills you. Game over!");
                    Console.WriteLine("Press enter to exit...");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("\nTime is up! The goblin got bored and attacked you. Game over!");
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        static void TimerCallback(object state)
        {
            Console.WriteLine("\nTime is up! The goblin got bored and attacked you. Game over!");
            Console.WriteLine("Press enter to exit...");
            timeIsUp = true;
        }
    }
}
