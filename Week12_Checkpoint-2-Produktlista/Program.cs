//using System.Xml.Linq;
using System.Linq;
using System.Runtime.CompilerServices;

List<Product> productsList = new List<Product>();
bool addNewProduct = true;
AddProduct(addNewProduct);


static void AddProduct(bool addNewProduct, bool shouldExit = false)
{
    //List<Product> products = productsList;
    List<Product> productsList = new List<Product>();
    string lastInput = string.Empty;

    if (shouldExit) return;

    do
    {
        Message.AskForNewProduct();
        string productCategory = Console.ReadLine() ?? String.Empty;
        lastInput = productCategory;

        if (productCategory.ToLower().Trim() == "q")
        {
            addNewProduct = false;
            break;
        }

        if(productCategory.ToLower().Trim().Length > 0)
        {
            Console.Write("Enter a Product Name: ");
            string productName = Console.ReadLine() ?? String.Empty;

            int priceAsNumber;
            Console.Write("Enter a Price: ");
            string productPrice = Console.ReadLine() ?? String.Empty;
            bool isPrice = int.TryParse(productPrice, out priceAsNumber);

            if(isPrice || priceAsNumber > 0)
            {
                Product newProduct = new Product(productCategory, productName, priceAsNumber);
                productsList.Add(newProduct);
                Message.ShowSuccessMessage();
            }
        }
    } while (addNewProduct);


    if (!addNewProduct)
    {
        Console.WriteLine("-------------------------------------------------------");
        Message.PrintAllProducts(productsList);
        string command = Message.FollowUpMessage(true);

        // ADD NEW PRODUCT
        if(command == "p")
        {
            AddProduct(true);
        }
        // SEARCH
        else if (command == "s")
        {
            Console.Write("Enter a Product Name: ");

            string productToFind = Console.ReadLine() ?? String.Empty; 
            productToFind = productToFind.Trim();
            
            List<Product> foundProducts = Product.Find(productsList, productToFind);

            if(foundProducts.Count > 0)
            {
                Message.PrintAllProducts(productsList, true, productToFind);
                Message.FollowUpMessage(true);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nThe Product : '{productToFind}' was not found not");
                Console.ResetColor();

            }
        }
        // QUIT
        else if(command == "q")
        {
            AddProduct(false, true);
            //Console.ReadKey();
        }

    }
}



class Product
{
    public string Category { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public Product(string category, string name, int price)
    {
        Category = category;
        Name = name;
        Price = price;
    }
    public static int SumOfProducts(List<Product> products) {
        Calculation Calculate = new Calculation();
        return Calculate.SumOfAllProductPrices(products);
    }

    public static List<Product> Find(List<Product> products, string name)
    {
        List<Product> matchingProduct = products.Where(p => p.Name == name).ToList();
        return matchingProduct.OrderBy(p => p.Price).ToList();
    }
}

class Calculation
{
    public int SumOfAllProductPrices(List<Product> products)
    {
        int sum = 0;
        foreach (Product product in products)
        {
            sum += product.Price;
        }
        return sum;
    }
}

class Message
{
    public static void AskForNewProduct()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("To enter a new product - follow the steps | To quit - enter: \"Q\"");
        Console.ResetColor();
        Console.Write("Enter a Category: ");
    }

    public static void ShowSuccessMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The product was successfully added!");
        Console.ResetColor();
        Console.WriteLine("-------------------------------------------------------");
    }

    public static string FollowUpMessage(bool returnCommand = true)
    {
        Console.WriteLine("-------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("To enter a new product - enter: \"P\" | To search for a product - enter: \"S\" | To quit - enter: \"Q\"");
        Console.ResetColor();

        string command = Console.ReadLine() ?? String.Empty;
        char key = command.ToLower().Trim()[0];

        //string action = "";
        //switch (key.ToString())
        //{
        //    case "p": // Add new - resuse 
        //        action = "p";
        //        break;
        //    case "s": // Search
        //        action = "s";
        //        break;
        //    case "q": // Quit
        //        action = "q";
        //        break;
        //    default:
        //        action = "q";
        //        break;
        //}
        //return action;

        if (!returnCommand)
        {
            return "q";
        }
        else return key.ToString();
    }


    public static void PrintAllProducts(List<Product> products, bool hightlight = false, string productToHightlight = "")
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Category\t\tProduct\t\t\tPrice");
        Console.ResetColor();

        List<Product> sortedProducts = products.OrderBy(p => p.Price).ToList();

        foreach (Product product in sortedProducts)
        {
            if(hightlight && productToHightlight.Trim() != "" && product.Name == productToHightlight)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{product.Category}\t\t\t{product.Name}\t\t\t{product.Price}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"{product.Category}\t\t\t{product.Name}\t\t\t{product.Price}");
            }
        }

        int amount = Product.SumOfProducts(products);
        Console.WriteLine($"\n\t\t\tTotal amount:\t\t{amount}");
        //Message.FollowUpMessage();
    }
}




//static void FindProductByName()
//{

//    Console.WriteLine("---------------------- FIND ---------------------------------");
//    Console.Write("\nFind product: ");

//    string findProduct = Console.ReadLine() ?? String.Empty;

//    List<Product> foundProducts = Product.Find(productsList, findProduct);
//    int count = foundProducts.Count;

//    Console.Write($"Items with name '{findProduct}' - Found : {count}");
//    foreach (Product product in foundProducts)
//    {
//        Console.WriteLine("\nFound: " + product.Name + " - " + product.Price);
//    }
//    Console.ReadKey();
//}
