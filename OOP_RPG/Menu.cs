using System;
using System.Collections.Generic;

namespace CarRentalSystem
{
    public class Menu
    {
        private List<MenuOption> ListOfOptions { get; set; }
        private string OptionMenuHeader { get; set; }
        private string InputErrorHint { get; set; }
        private string OptionMenu { get; set; }

        public Menu(List<MenuOption> listOfOptions, string optionMenuHeader, string inputErrorHint)
        {
            ListOfOptions = listOfOptions;
            OptionMenuHeader = optionMenuHeader;
            InputErrorHint = inputErrorHint;
            OptionMenu = BuildOptionMenu();
        }

        public Menu(List<MenuOption> listOfOptions, string optionMenuHeader)
        {
            ListOfOptions = listOfOptions;
            OptionMenuHeader = optionMenuHeader;
            InputErrorHint = "Invalid Input...";
            OptionMenu = BuildOptionMenu();
        }

        public Menu(List<MenuOption> listOfOptions)
        {
            ListOfOptions = listOfOptions;
            OptionMenuHeader = "Please Choose A Option From The Following List Of Options:";
            InputErrorHint = "Invalid Input...";
            OptionMenu = BuildOptionMenu();
        }



        private string BuildOptionMenu()
        {
            string menu = $"{OptionMenuHeader}\n";

            for (int i = 0; i < ListOfOptions.Count; i++)
            {
                menu += $"- {ListOfOptions[i].Title} ({i})\n";
            }
            return menu;
        }




        public void RunAndDisplayMenu()
        {
            Console.WriteLine(OptionMenu + "\n");
            string userInput = Console.ReadLine().Trim() + "\n";

            bool isValidInput = int.TryParse(userInput.Replace("\n", ""), out int userNumber);

            while (!isValidInput || userNumber < 0 || userNumber > (ListOfOptions.Count - 1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + InputErrorHint);
                Console.ResetColor();
                Console.WriteLine(BuildOptionMenu());
                userInput = Console.ReadLine().Trim() + "\n";
                isValidInput = int.TryParse(userInput.Replace("\n", ""), out userNumber);
            }
            Console.ResetColor();


            if (isValidInput)
            {
                for (int i = 0; i < ListOfOptions.Count; i++)
                {
                    if (userNumber == i)
                    {
                        ListOfOptions[i].Callback();
                        break;
                    }
                }
            }
            else
            {
                throw new Exception("While Loop Broke Or Something (loop that prevents invalid input for the Menu Class)");
            }
        }



    }
}
