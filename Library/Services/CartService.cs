/*
 * Author: Sakthi Santhosh
 * Created on: 24/04/2024
 */
using Challenge1.Library.Exceptions;
using Challenge1.Library.Models;
using Challenge1.Library.Repositories;

namespace Challenge1.Library.Services;

public class CartService(IBaseRepository<CartModel> repository) : BaseService<CartModel>(repository)
{
    public async Task AddToCart(CartModel cart, ProductModel product, int quantityOrdered)
    {
        if (quantityOrdered > product.QuantityInStock)
            throw new InsufficientStockException();

        if (cart.Customer.Age < product.MinAge)
            throw new AgeIneligibilityException();

        if (cart.CartItems.Count > 5)
            throw new OutOfMemoryException();

        _entityRepository.GetById(cart.Id).CartItems.Add(new CartItemModel(cart, product, quantityOrdered));
    }

    public static async Task GenerateInvoice(CartModel cart)
    {
        double totalPrice = 0;
        double discountedPrice;
        ProductModel product;

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"INVOICE: {DateTime.Now}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Invoice ID:    {cart.Id}");
        Console.WriteLine($"Customer name: {cart.Customer.Name}");
        Console.WriteLine($"Contact:       {cart.Customer.Phone}");
        Console.WriteLine("----------------------------------------\n");

        foreach (CartItemModel cartItem in cart.CartItems)
        {
            product = cartItem.Product;
            discountedPrice = product.Price * cartItem.QuantityOrdered - ((product.DiscountExpiryDate > DateTime.Now)
                ? product.Price * product.Discount
                : 0);
            totalPrice += discountedPrice;


            Console.WriteLine("* " + product.Name);
            Console.WriteLine($"    Price:          {product.Price:C}");
            Console.WriteLine($"    Total price:    {product.Price * cartItem.QuantityOrdered:C}");
            Console.WriteLine($"    After discount: {discountedPrice:C}\n");
        }

        if (totalPrice < 1000)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Shipping fee: {100:C}");
        }

        if (totalPrice >= 1500 && cart.CartItems.Count < 4)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Special discount: {totalPrice * 0.05:C}");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Total price: {totalPrice:C}");
        Console.WriteLine("----------------------------------------\n");
    }
}
