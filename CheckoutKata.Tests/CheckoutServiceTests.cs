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
    public void ShouldAddValidItemToReceiptWhenScanned()
    {
        var sku = "A";
        _itemRepository.GetBySku(sku).Returns(new Item(sku, 50m));

        _checkoutService.Scan(sku);

        var totalPrice = _checkoutService.GetTotalPrice();
        Assert.That(totalPrice, Is.EqualTo(50m));
    }
}
