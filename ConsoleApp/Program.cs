/*
 * Author: Sakthi Santhosh
 * Created on: 24/04/2024
 */
using Challenge1.Library.Models;
using Challenge1.Library.Repositories;
using Challenge1.Library.Services;

namespace Challenge1.ConsoleApp;

internal class Program
{
    public async static Task Main()
    {
        CartService cartService = new(new CartRepository());

        CustomerModel newCustomer = new("Sakthi Santhosh", "+91 637-9921036", 15);
        CartModel newCart = new(newCustomer);

        ProductModel newProduct = new("FairBud Earphones", 150, 100.00, 0.1, new DateTime(2024, 05, 01));
        ProductModel newProduct2 = new("iPhone 15 Pro", 10, 1000.00, 0.05, new DateTime(2024, 08, 01));

        cartService.Add(newCart);
        cartService.AddToCart(newCart, newProduct, 5);
        cartService.AddToCart(newCart, newProduct2, 2);

        Console.WriteLine(cartService[0]);
        CartService.GenerateInvoice(newCart);
        cartService.FlashMessage(message: "Your invoice has been generated successfully!");
    }
}
