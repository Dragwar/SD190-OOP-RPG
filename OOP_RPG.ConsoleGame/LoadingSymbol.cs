using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OOP_RPG.ConsoleGame
{
    public class LoadingSymbol
    {
        public string Name { get; set; }
        public string LoadingMessage { get; set; }
        public string FinishedLoadingMessage { get; set; }
        public List<char> SpinnerCharacters { get; set; }
        public int AnimationDelay { get; set; }
        public int Counter { get; set; }

        // Constructor
        public LoadingSymbol(string name, string loadingMessage, string finishedLoadingMessage, int animationDelay, List<char> spinnerCharacters)
        {
            Name = name;
            LoadingMessage = loadingMessage;
            FinishedLoadingMessage = finishedLoadingMessage;
            SpinnerCharacters = spinnerCharacters.Take(4).ToList();
            AnimationDelay = animationDelay;
            Counter = 0;
        }

        // Constructor
        public LoadingSymbol(List<char> spinnerCharacters, int animationDelay)
        {
            Name = "Loading Symbol";
            LoadingMessage = "Loading . . .";
            FinishedLoadingMessage = "Finished Loading\n\nPress Any Key To Continue";
            SpinnerCharacters = spinnerCharacters.Take(4).ToList();
            AnimationDelay = animationDelay;
            Counter = 0;
        }

        // Constructor
        public LoadingSymbol(int animationDelay)
        {
            Name = "Loading Symbol";
            LoadingMessage = "Loading . . .";
            FinishedLoadingMessage = "Finished Loading\n\nPress Any Key To Continue";
            SpinnerCharacters = new List<char>() { '-', '/', '|', '\\' };
            AnimationDelay = animationDelay;
            Counter = 0;
        }

        // Constructor
        public LoadingSymbol(string loadingMessage, string finishedLoadingMessage)
        {
            Name = "Loading Symbol";
            LoadingMessage = loadingMessage;
            FinishedLoadingMessage = finishedLoadingMessage;
            SpinnerCharacters = new List<char>() { '-', '/', '|', '\\' };
            AnimationDelay = 75;
            Counter = 0;
        }

        // Constructor
        public LoadingSymbol()
        {
            Name = "Loading Symbol";
            LoadingMessage = "Loading . . .";
            FinishedLoadingMessage = "Finished Loading\n\nPress Any Key To Continue";
            SpinnerCharacters = new List<char>() { '-', '/', '|', '\\' };
            AnimationDelay = 75;
            Counter = 0;
        }


        // Displays Loading Animation
        public void Excute(int loadTimeSeconds)
        {
            Console.Write($"{LoadingMessage} ");

            DateTimeOffset endTime = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(loadTimeSeconds));

            // Hide Cursor (hide the blinking cursor)
            Console.CursorVisible = false;

            while (endTime > DateTimeOffset.UtcNow)
            {
                Turn();
            }

            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine(FinishedLoadingMessage);
        }

        // Gets called within a loop to repeat to the user defined time
        public void Turn()
        {
            if (SpinnerCharacters.Any())
            {
                Console.ForegroundColor = (ConsoleColor)new Random().Next(1, 15);
                Counter++;
                switch (Counter % SpinnerCharacters.Count)
                {
                    case 0:
                        Console.Write(SpinnerCharacters[0]);
                        break;
                    case 1:
                        Console.Write(SpinnerCharacters[1]);
                        break;
                    case 2:
                        Console.Write(SpinnerCharacters[2]);
                        break;
                    case 3:
                        Console.Write(SpinnerCharacters[3]);
                        break;
                }

                // Set cursor position back to the original position to print the char in the same spot
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Thread.Sleep(AnimationDelay);
                Console.ResetColor();
            }
            else
            {
                throw new Exception("You need to have at least 1 char in the spinner list");
            }
        }
    }
}

