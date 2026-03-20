using System.Linq;

List<Product> productsList = new List<Product>();
bool addNewProduct = true;
AddProduct(productsList, addNewProduct);

static void AddProduct(List<Product> productsList, bool addNewProduct, bool shouldExit = false)
{
    if (!addNewProduct || shouldExit) return;

    do
    {
        Message.AskForNewProduct();
        string productCategory = Console.ReadLine() ?? String.Empty;

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
        Message.ShowAllProducts(productsList);
        string command = Message.ManageCommands(true);

        // ADD NEW PRODUCT
        if(command == "p")
        {
            AddProduct(productsList, true, false);
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
                Message.ShowAllProducts(productsList, true, productToFind);
                Message.ManageCommands(true);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nThe Product : '{productToFind}' was not found not");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
        // QUIT
        else if(command == "q") return;
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
            sum += product.Price;
        return sum;
    }
}

class Message
{
    public static void WriteLine()
    {
        Console.WriteLine("-------------------------------------------------------");
    }

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
        WriteLine();
    }

    public static string ManageCommands(bool returnCommand = true)
    {
        WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("To enter a new product - enter: \"P\" | To search for a product - enter: \"S\" | To quit - enter: \"Q\"");
        Console.ResetColor();

        string command = Console.ReadLine() ?? String.Empty;
        char key = command.ToLower().Trim()[0];

        if (!returnCommand)
            return "q";
        else return key.ToString();
    }

    public static void ShowAllProducts(List<Product> products, bool hightlight = false, string productToHightlight = "")
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