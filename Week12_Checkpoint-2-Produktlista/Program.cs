List<Product> productsList = new List<Product>();
while (true)
{
    Message.AskForNewProduct();
    string productCategory = Console.ReadLine() ?? String.Empty;
    
    if(productCategory.ToLower().Trim() == "q") break;
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
}

Console.WriteLine("-------------------------------------------------------");
Message.PrintAllProducts(productsList);
Console.ReadKey();


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

    public static void PrintAllProducts(List<Product> products)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Category\t\tProduct\t\t\tPrice");
        Console.ResetColor();

        foreach (Product product in products)
        {
            Console.WriteLine($"{product.Category}\t\t\t{product.Name}\t\t\t{product.Price}");
        }

        int amount = Product.SumOfProducts(products);
        Console.WriteLine($"\n\t\t\tTotal amount:\t\t{amount}");
    }
}