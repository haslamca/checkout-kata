#nullable disable

using NUnit.Framework;

namespace CheckoutKata.Tests;

public class ItemRepositoryTests
{
    private IItemRepository _itemRepository;

    [SetUp]
    public void Setup()
    {
        _itemRepository = new ItemRepository();
    }

    [Test]
    public void ShouldReturnItemWhenExists()
    {
        var existingSku = "A";

        var item = _itemRepository.GetBySku(existingSku);

        Assert.That(item, Is.Not.Null);
        Assert.That(existingSku, Is.EqualTo(item.Sku));
        Assert.That(50, Is.EqualTo(item.UnitPrice));
    }

    [Test]
    public void ShouldReturnNullWhenItemDoesNotExist()
    {
        var nonExistingSku = "X";

        var item = _itemRepository.GetBySku(nonExistingSku);

        Assert.That(item, Is.Null);
    }
}
