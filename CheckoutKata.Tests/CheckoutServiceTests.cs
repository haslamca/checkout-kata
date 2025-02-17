#nullable disable

using NSubstitute;
using NUnit.Framework;

namespace CheckoutKata.Tests;

public class CheckoutServiceTests
{
    private IItemRepository _itemRepository;
    private CheckoutService _checkoutService;

    [SetUp]
    public void Setup()
    {
        _itemRepository = Substitute.For<IItemRepository>();
        _checkoutService = new CheckoutService(_itemRepository);
    }

    [Test]
    public void ShouldNotScanInvalidSku()
    {
        _checkoutService.Scan(null);

        _itemRepository.DidNotReceive().GetBySku(Arg.Any<string>());
    }

    [Test]
    public void ShouldAddValidItemToReceiptWhenScanned()
    {
        var validSku = "A";
        _itemRepository.GetBySku(validSku).Returns(new Item(validSku, 50m));

        _checkoutService.Scan(validSku);

        var totalPrice = _checkoutService.GetTotalPrice();
        Assert.That(totalPrice, Is.EqualTo(50m));
    }

    [Test]
    public void ShouldNotAddInvalidItemToReceiptWhenScanned()
    {
        var invalidSku = "X";
        _itemRepository.GetBySku(invalidSku).Returns((Item)null);

        _checkoutService.Scan(invalidSku);

        var totalPrice = _checkoutService.GetTotalPrice();
        Assert.That(totalPrice, Is.EqualTo(0));
    }
}
