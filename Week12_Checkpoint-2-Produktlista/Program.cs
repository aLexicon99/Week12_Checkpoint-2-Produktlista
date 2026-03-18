using System.ComponentModel;

Console.Clear();
List<Product> productsList = new List<Product>();

while (true)
{
    AskForNewProduct();
    Console.Write("Enter a Category: ");
    string productCategory = Console.ReadLine();
    
    if (productCategory.ToLower().Trim() == "q")
    {
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
            newProduct.ShowSuccessMessage();
        }
    }
}

Console.WriteLine("-------------------------------------------------------");
PrintAllProducts(productsList);

void AskForNewProduct()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("To enter a new product - follow the steps | To quit - enter: \"Q\"");
    Console.ResetColor();
}

void PrintAllProducts(List<Product> allproducts)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Category\t\tProduct\t\t\tPrice");
    Console.ResetColor();

    int sum = 0;

    foreach (Product product in allproducts)
    {
        Console.WriteLine($"{product.Category}\t\t\t{product.Name}\t\t\t{product.Price}");

        sum += product.Price;
    }
    Console.WriteLine($"\n\t\t\tTotal amount:\t\t{ sum }");
}


class Product
{

    public string Category { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public Product(string category)
    {
        Category = category;
    }
    public Product(string category, string name, int price)
    {
        Category = category;
        Name = name;
        Price = price;
    }

    public void ShowSuccessMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The product was successfully added!");
        Console.ResetColor();
        Console.WriteLine("-------------------------------------------------------");
    }
}