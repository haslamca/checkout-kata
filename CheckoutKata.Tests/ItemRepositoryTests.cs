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
        var item = _itemRepository.GetBySku("A");
        Assert.That(item, Is.Not.Null);
    }
}
