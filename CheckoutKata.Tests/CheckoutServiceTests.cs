#nullable disable

using System;
using NSubstitute;
using NUnit.Framework;

namespace CheckoutKata.Tests;

public class CheckoutServiceTests
{
    private IItemRepository _itemRepository;
    private ISpecialOfferRepository _specialOfferRepository;
    private CheckoutService _checkoutService;

    [SetUp]
    public void Setup()
    {
        _itemRepository = Substitute.For<IItemRepository>();
        _specialOfferRepository = Substitute.For<ISpecialOfferRepository>();
        _checkoutService = new CheckoutService(_itemRepository, _specialOfferRepository);
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

    [Test]
    public void ShouldApplySpecialOfferWhenOfferQuantityReached()
    {
        var sku = "A";
        var item = new Item(sku, 50m); // A = 50 each
        var offer = new SpecialOffer(sku, 3, 130m); // 3x A (50 each) for 130
        _itemRepository.GetBySku(sku).Returns(item);
        _specialOfferRepository.GetBySku(sku).Returns(offer);
        
        _checkoutService.Scan(sku);
        _checkoutService.Scan(sku);
        _checkoutService.Scan(sku);

        var totalPrice = _checkoutService.GetTotalPrice();
        Assert.That(totalPrice, Is.EqualTo(130m));
    }

    [Test]
    public void ShouldNotApplySpecialOfferWhenOfferQuantityNotReached()
    {
        var sku = "A";
        var item = new Item(sku, 50m); // A = 50 each
        var offer = new SpecialOffer(sku, 3, 130m); // 3x A (50 each) for 130
        _itemRepository.GetBySku(sku).Returns(item);
        _specialOfferRepository.GetBySku(sku).Returns(offer);

        _checkoutService.Scan(sku);
        _checkoutService.Scan(sku);

        var totalPrice = _checkoutService.GetTotalPrice();
        Assert.That(totalPrice, Is.EqualTo(100m));
    }

    [Test]
    public void ShouldHandleMultipleItemsAndOffersCorrectly()
    {
        var skuA = "A";
        var itemA = new Item(skuA, 50m); // A = 50 each
        var offerA = new SpecialOffer(skuA, 3, 130m); // 3x A (50 each) for 130
        _itemRepository.GetBySku(skuA).Returns(itemA);
        _specialOfferRepository.GetBySku(skuA).Returns(offerA);

        var skuB = "B";
        var itemB = new Item(skuB, 30m); // B = 30 each
        var offerB = new SpecialOffer(skuB, 2, 45m); // 2x B (30 each) for 45
        _itemRepository.GetBySku(skuB).Returns(itemB);
        _specialOfferRepository.GetBySku(skuB).Returns(offerB);

        var skuC = "C";
        var itemC = new Item(skuC, 20m); // C = 20 each
        _itemRepository.GetBySku(skuC).Returns(itemC);

        // 5x A (3x for 130, 2x for 100) = 230
        // 5x B (2x for 45, 2x for 45, 1x for 30) = 120
        // 2x C (2x for 40) = 40
        _checkoutService.Scan(skuA);
        _checkoutService.Scan(skuA);
        _checkoutService.Scan(skuB);
        _checkoutService.Scan(skuA);
        _checkoutService.Scan(skuB);
        _checkoutService.Scan(skuB);
        _checkoutService.Scan(skuC);
        _checkoutService.Scan(skuB);
        _checkoutService.Scan(skuA);
        _checkoutService.Scan(skuB);
        _checkoutService.Scan(skuA);
        _checkoutService.Scan(skuC);

        var totalPrice = _checkoutService.GetTotalPrice();
        Assert.That(totalPrice, Is.EqualTo(390m));
    }

    [Test]
    public void ShouldReturnZeroWhenNoItemsScanned()
    {
        var totalPrice = _checkoutService.GetTotalPrice();
        Assert.That(totalPrice, Is.EqualTo(0m));
    }
}
