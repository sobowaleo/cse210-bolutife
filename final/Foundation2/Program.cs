using System;
using System.Collections.Generic;
using System.Linq;

public class Address
{
    private string _street;
    private string _city;
    private string _state;
    private string _country;

    public Address(string street, string city, string state, string country)
    {
        _street = street ?? throw new ArgumentNullException(nameof(street));
        _city = city ?? throw new ArgumentNullException(nameof(city));
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _country = country ?? throw new ArgumentNullException(nameof(country));
    }

    public string Street => _street;
    public string City => _city;
    public string State => _state;
    public string Country => _country;

    public bool IsUSAddress => string.Equals(Country, "USA", StringComparison.OrdinalIgnoreCase);

    public string GetFullAddress() => $"{Street}, {City}, {State}, {Country}";
}

public class Customer
{
    private readonly string _name;
    private readonly Address _address;

    public Customer(string name, Address address)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _address = address ?? throw new ArgumentNullException(nameof(address));
    }

    public string Name => _name;
    public Address Address => _address;

    public bool IsUSCustomer => Address.IsUSAddress;
}

public class Product
{
    private readonly int _productId;
    private readonly string _name;
    private readonly decimal _price;
    private int _quantity;

    public Product(int productId, string name, decimal price, int quantity)
    {
        if (productId <= 0) throw new ArgumentException("Product ID must be positive", nameof(productId));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name cannot be empty", nameof(name));
        if (price <= 0) throw new ArgumentException("Price must be positive", nameof(price));
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive", nameof(quantity));

        _productId = productId;
        _name = name;
        _price = price;
        _quantity = quantity;
    }

    public int ProductId => _productId;
    public string Name => _name;
    public decimal Price => _price;
    public int Quantity => _quantity;

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0) throw new ArgumentException("Quantity must be positive", nameof(newQuantity));
        _quantity = newQuantity;
    }

    public decimal CalculateTotalPrice() => Price * Quantity;
}

public class Order
{
    private readonly Customer _customer;
    private readonly List<Product> _products = new List<Product>();
    private readonly DateTime _orderDate = DateTime.UtcNow;

    public Order(Customer customer)
    {
        _customer = customer ?? throw new ArgumentNullException(nameof(customer));
    }

    public string OrderNumber { get; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    public DateTime OrderDate => _orderDate;
    public Customer Customer => _customer;
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public void AddProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        
        var existingProduct = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
        if (existingProduct != null)
        {
            existingProduct.UpdateQuantity(existingProduct.Quantity + product.Quantity);
        }
        else
        {
            _products.Add(product);
        }
    }

    public bool RemoveProduct(int productId)
    {
        var product = _products.FirstOrDefault(p => p.ProductId == productId);
        if (product != null)
        {
            return _products.Remove(product);
        }
        return false;
    }

    public decimal CalculateShippingCost() => Customer.IsUSCustomer ? 5.00m : 35.00m;

    public decimal CalculateTotalCost() => _products.Sum(p => p.CalculateTotalPrice()) + CalculateShippingCost();

    public string GeneratePackingLabel()
    {
        var label = $"PACKING LABEL - Order #{OrderNumber}\n";
        label += $"Date: {OrderDate:yyyy-MM-dd}\n\n";
        label += "Products:\n";
        
        foreach (var product in _products)
        {
            label += $"- {product.Name} (ID: {product.ProductId}), Qty: {product.Quantity}\n";
        }
        
        return label;
    }

    public string GenerateShippingLabel()
    {
        var label = $"SHIPPING LABEL - Order #{OrderNumber}\n";
        label += $"Customer: {Customer.Name}\n";
        label += $"Address: {Customer.Address.GetFullAddress()}\n";
        label += $"Shipping: {(Customer.IsUSCustomer ? "Domestic" : "International")}\n";
        return label;
    }
}

public class Program
{
    public static void Main()
    {
        // Create addresses
        var usAddress = new Address("123 Main St", "New York", "NY", "USA");
        var canadaAddress = new Address("456 Maple Ave", "Toronto", "ON", "Canada");

        // Create customers
        var usCustomer = new Customer("John Smith", usAddress);
        var canadaCustomer = new Customer("Alice Johnson", canadaAddress);

        // Create products
        var laptop = new Product(101, "Laptop", 899.99m, 1);
        var phone = new Product(102, "Smartphone", 499.99m, 2);
        var tablet = new Product(103, "Tablet", 299.99m, 1);

        // Create and process orders
        var order1 = new Order(usCustomer);
        order1.AddProduct(laptop);
        order1.AddProduct(phone);

        var order2 = new Order(canadaCustomer);
        order2.AddProduct(tablet);
        order2.AddProduct(new Product(104, "Headphones", 99.99m, 3));

        // Display order information
        DisplayOrderDetails(order1);
        DisplayOrderDetails(order2);
    }

    private static void DisplayOrderDetails(Order order)
    {
        Console.WriteLine("====================================");
        Console.WriteLine(order.GenerateShippingLabel());
        Console.WriteLine();
        Console.WriteLine(order.GeneratePackingLabel());
        Console.WriteLine($"Total Cost: {order.CalculateTotalCost():C2}");
        Console.WriteLine("====================================\n");
    }
}