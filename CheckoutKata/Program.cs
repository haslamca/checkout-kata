#nullable disable

using Microsoft.Extensions.DependencyInjection;

namespace CheckoutKata;

public class Program
{
    private static ICheckoutService _checkoutService;

    public static void Main()
    {
        ConfigureServices();

        Console.WriteLine("Press Enter to finish and pay.");
        string sku;

        do
        {
            Console.Write("SKU: ");

            sku = Console.ReadLine();
            _checkoutService.Scan(sku);
        } while (sku.Length > 0);

        var totalPrice = _checkoutService.GetTotalPrice();
        Console.WriteLine($"Total: {totalPrice}");
    }

    private static void ConfigureServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<ICheckoutService, CheckoutService>()
            .AddSingleton<IItemRepository, ItemRepository>()
            .AddSingleton<ISpecialOfferRepository, SpecialOfferRepository>()
            .BuildServiceProvider();

        _checkoutService = serviceProvider.GetService<ICheckoutService>();
    }
}
