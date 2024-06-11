using TestingTracking.Infrastructure.InfrastructureDbContext;
using TestingTracking.Domain.Entities;
using TestingTracking.Utilities;
using System.Diagnostics;

namespace TestingTracking.Console.Commands
{
    public class MenuItems
    {
        public readonly ItemsDbContext _context;

        public MenuItems(ItemsDbContext context)
        {
            _context = context;
        }

        public void RunProgram()
        {
            bool run = true;
            System.Console.Clear();
            while (run)
            {
                System.Console.Clear(); 
                DisplayMenu();
                int userChoice = InterfaceMethods.CheckInput(7);

                switch (userChoice)
                {
                    case 1:
                        RequiredList();
                        break;
                    case 2:
                        AddItems();
                        break;
                    case 3:
                        DeleteItems();
                        break;
                    case 4:
                        EditItems();
                        break;
                    case 5:
                        run = false;
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            System.Console.WriteLine("Interface Placeholder\n" +
                              "[1] Required Ordered List for assignment\n" +
                              "[2] Add items to database\n" +
                              "[3] Delete items from database\n" +
                              "[4] Edit items from database\n" +
                              "[5] Exit application\n" +
                              "Choose a number + enter: ");
        }

        private void RequiredList()
        {
            System.Console.WriteLine("Displaying required ordered list for assignment:");
            ListAllItems();
        }
        private void AddItems()
        {
            System.Console.WriteLine("Enter details for new item:");
            string type = getNotNullParametersFromUser("Type");
            string brand = getNotNullParametersFromUser("Brand");
            string model = getNotNullParametersFromUser("Model");
            string office = getNotNullParametersFromUser("Office");
            DateTime purchaseDate = DateTime.Parse(getNotNullParametersFromUser("Purchase Date (yyyy-mm-dd)"));
            decimal price = getNotNullDecimalParametersFromUser("Price in USD");
            string currency = getNotNullParametersFromUser("Currency");

            decimal localPrice = CalculateLocalPrice(price, currency);

            var newItem = new Asset
            {
                Model = model,
                Type = type,
                Brand = brand,
                Office = office,
                PurchaseDate = purchaseDate,
                Currency = currency,
                PriceInUSD = price,
                LocalPriceToday = localPrice
            };

            _context.Assets.Add(newItem);
            _context.SaveChanges();
            System.Console.WriteLine("Item added successfully!");
        }

        private decimal CalculateLocalPrice(decimal priceInUSD, string currency)
        {
            return currency.ToUpper() switch
            {
                "EUR" => priceInUSD * 0.93m,
                "SEK" => priceInUSD * 10.5m,
                _ => priceInUSD
            };
        }

        private string getNotNullParametersFromUser(string type)
        {
            string value = null;

            while (value == null)
            {
                System.Console.Write($"{type}: ");
                value = System.Console.ReadLine();
                if (value != null)
                {
                    return value;
                } else
                {
                   System.Console.Write("Parameter not optional, please insert it: ");
                }
            }
            return value;
        }

        private decimal getNotNullDecimalParametersFromUser(string type)
        {
            string value = null;

            while (value == null)
            {
                System.Console.Write($"{type}: ");
                value = System.Console.ReadLine();
                if (value != null)
                {
                    return decimal.Parse(value);
                }
                else
                {
                    System.Console.Write("Parameter not optional, please insert it: ");
                }
            }
            return decimal.Parse(value);
        }

        private void DeleteItems()
        {
            System.Console.Write("Enter Item ID to delete: ");
            int itemId = int.Parse(System.Console.ReadLine());

            var item = _context.Assets.Find(itemId);
            if (item != null)
            {
                _context.Assets.Remove(item);
                _context.SaveChanges();
                System.Console.WriteLine("Item deleted successfully!");
            }
            else
            {
                System.Console.WriteLine("Item not found!");
            }
        }


        private void EditItems()
        {
            System.Console.Write("Enter Item ID to edit: ");
            int itemId = int.Parse(System.Console.ReadLine());
            var item = _context.Assets.Find(itemId);

            System.Console.Clear();
            if (item != null)
            {
                System.Console.WriteLine("Enter new details (leave blank to keep current value):");

                System.Console.Write("New Brand (current: " + item.Brand + "): ");
                string newBrand = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(newBrand))
                {
                    item.Brand = newBrand;
                }

                System.Console.Write("New Model (current: " + item.Model + "): ");
                string newModel = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(newModel))
                {
                    item.Model = newModel;
                }

                System.Console.Write("New Office (current: " + item.Office + "): ");
                string newOffice = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(newOffice))
                {
                    item.Office = newOffice;
                }

                System.Console.Write("New Purchase Date (yyyy-mm-dd, current: " + item.PurchaseDate.ToShortDateString() + "): ");
                string newPurchaseDateString = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(newPurchaseDateString))
                {
                    item.PurchaseDate = DateTime.Parse(newPurchaseDateString);
                }

                System.Console.Write("New Currency (current: " + item.Currency + "): ");
                string newCurrency = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(newCurrency))
                {
                    item.Currency = newCurrency;
                }

                System.Console.Write("New Price in USD (current: " + item.PriceInUSD + "): ");
                string newPriceInUSDString = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(newPriceInUSDString))
                {
                    item.PriceInUSD = decimal.Parse(newPriceInUSDString);
                }

              
                item.LocalPriceToday = CalculateLocalPrice(item.PriceInUSD, item.Currency);

                _context.SaveChanges();
                System.Console.WriteLine("Item updated successfully!");
            }
            else
            {
                System.Console.WriteLine("Item not found!");
            }
        }
        private void ListAllItems()
        {
            var items = _context.Assets
                .OrderBy(i => i.Office)
                .ThenBy(i => i.PurchaseDate)
                .ToList();
            System.Console.Clear();
            System.Console.WriteLine("Displaying required ordered list for assignment:\n");

            int widthType = 10;
            int widthBrand = 10;
            int widthModel = 15;
            int widthOffice = 10;
            int widthPurchaseDate = 15;
            int widthPriceUSD = 15;
            int widthCurrency = 10;
            int widthLocalPrice = 15;

            System.Console.WriteLine(
                $"{"Type".PadRight(widthType)} {"Brand".PadRight(widthBrand)} {"Model".PadRight(widthModel)} {"Office".PadRight(widthOffice)} {"Purchase Date".PadRight(widthPurchaseDate)} {"Price in USD".PadRight(widthPriceUSD)} {"Currency".PadRight(widthCurrency)} {"Local Price".PadRight(widthLocalPrice)}");

            System.Console.WriteLine(
                $"{new string('-', widthType).PadRight(widthType)} {new string('-', widthBrand).PadRight(widthBrand)} {new string('-', widthModel).PadRight(widthModel)} {new string('-', widthOffice).PadRight(widthOffice)} {new string('-', widthPurchaseDate).PadRight(widthPurchaseDate)} {new string('-', widthPriceUSD).PadRight(widthPriceUSD)} {new string('-', widthCurrency).PadRight(widthCurrency)} {new string('-', widthLocalPrice).PadRight(widthLocalPrice)}");

            DateTime threeYearsBeforeToday = DateTime.Now.AddYears(-3);
            DateTime threeYearsBeforeTodayplus3M = threeYearsBeforeToday.AddMonths(3);
            DateTime threeYearsBeforeTodayplus6M = threeYearsBeforeToday.AddMonths(6);

            foreach (var item in items)
            {
                if (threeYearsBeforeToday < item.PurchaseDate && item.PurchaseDate <= threeYearsBeforeTodayplus3M)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (threeYearsBeforeTodayplus3M < item.PurchaseDate && item.PurchaseDate <= threeYearsBeforeTodayplus6M)
                {
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    System.Console.ResetColor();
                }

                System.Console.WriteLine(
                    $"{item.Type.PadRight(widthType)} {item.Brand.PadRight(widthBrand)} {item.Model.PadRight(widthModel)} {item.Office.PadRight(widthOffice)} {item.PurchaseDate.ToShortDateString().PadRight(widthPurchaseDate)} {item.PriceInUSD.ToString().PadRight(widthPriceUSD)} {item.Currency.PadRight(widthCurrency)} {item.LocalPriceToday.ToString().PadRight(widthLocalPrice)}");

                System.Console.ResetColor();
            }

            getNotNullParametersFromUser("\n\nWrite any key to continue");
        }


    }
}
