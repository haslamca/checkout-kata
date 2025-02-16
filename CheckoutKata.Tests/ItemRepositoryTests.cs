#nullable disable

using NSubstitute;
using NUnit.Framework;

namespace CheckoutKata.Tests;

public class ItemRepositoryTests
{
    private IItemRepository _itemRepository;

    [SetUp]
    public void Setup()
    {
        _itemRepository = Substitute.For<IItemRepository>();
    }

    [Test]
    public void ShouldReturnItemWhenExists()
    {
        var existingSku = "A";
        _itemRepository.GetBySku(existingSku).Returns(new Item(existingSku, 50m));

        var item = _itemRepository.GetBySku(existingSku);

        Assert.That(item, Is.Not.Null);
        Assert.That(existingSku, Is.EqualTo(item.Sku));
        Assert.That(50, Is.EqualTo(item.UnitPrice));
    }

    [Test]
    public void ShouldReturnNullWhenItemDoesNotExist()
    {
        var nonExistingSku = "X";
        _itemRepository.GetBySku(nonExistingSku).Returns((Item)null);

        var item = _itemRepository.GetBySku(nonExistingSku);

        Assert.That(item, Is.Null);
    }
}
