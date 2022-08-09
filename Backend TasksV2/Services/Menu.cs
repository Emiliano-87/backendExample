using System;
using Backend.Helpers;

namespace Backend.Services
{
    public class Menu : IMenu
    {
        //We could use reflection instead but can be slow, so we use a feature included in 4.5 or beyond using CallerFilePath attribute
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public int DisplayMenu()
        {
            //Clear Console
            Console.Clear();

            //Write menu to consle
            Console.WriteLine("Main Menu" +
            "\n---------\n" +
            "\n1. View Movie Stars List" +
            "\n2. Calculate Net Salary" +
            "\n3. Exit");

            log.Debug("Console is cleared and Menu is displayed for user");
            
            //opt is the return variable
            int opt;

            //correctSelection 
            bool correctSelection = false;
            do
            {
                int.TryParse(Console.ReadLine(), out opt);

                correctSelection = (opt == 1 || opt == 2 || opt == 3);

                //If no correct menu item was specified ask user to select a correct one
                if (!correctSelection)
                {
                    Console.WriteLine("Please select a valid option from the menu...");
                    log.Error("User selected a non-valid menu option.");
                }

            } while (!correctSelection);

            log.Debug($"Menu option {opt} selected by user.");
            return opt;
        }
    }
}
