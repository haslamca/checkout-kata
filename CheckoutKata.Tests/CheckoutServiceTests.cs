#nullable disable

using NUnit.Framework;

namespace CheckoutKata.Tests;

public class CheckoutServiceTests
{
    private ICheckoutService _checkoutService;

    [SetUp]
    public void Setup()
    {
        _checkoutService = new CheckoutService();
    }

    [Test]
    public void ShouldAddValidItemToReceiptWhenScanned()
    {
        _checkoutService.Scan("A");

        var totalPrice = _checkoutService.GetTotalPrice();
        Assert.That(totalPrice, Is.GreaterThan(0m));
    }
}
