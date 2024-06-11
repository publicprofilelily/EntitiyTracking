using System;

namespace TestingTracking.Utilities
{
    public static class InterfaceMethods
    {
        public static int CheckInput(int maxOption)
        {
            int userChoice;
            while (true)
            {
                if (int.TryParse(System.Console.ReadLine(), out userChoice) && userChoice >= 1 && userChoice <= maxOption)
                {
                    return userChoice;
                }
                System.Console.WriteLine($"Please enter a valid number between 1 and {maxOption}.");
            }
        }
    }
}
