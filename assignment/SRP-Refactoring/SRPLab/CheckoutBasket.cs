
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;


public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private readonly CouponDiscountCalculator _discountCalculator = new();
    private readonly GiftMessageFormatter _giftMessageFormatter = new();
    private readonly PaymentAuthorizer _paymentAuthorizer = new();

    private string? _couponRaw;
    private bool _giftWrap;

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0)
            throw new ArgumentOutOfRangeException(nameof(qty));

        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText)
    {
        _couponRaw = couponText;
    }

    public void EnableGiftWrap()
    {
        _giftWrap = true;
    }

    public decimal SubTotal()
    {
        return _lines.Sum(line => line.Price * line.Qty);
    }

    public decimal DiscountAmount()
    {
        return _discountCalculator.Calculate(_couponRaw, SubTotal());
    }

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();

        if (_giftWrap)
            total += 4.99m;

        return Math.Max(0m, total);
    }

    public string GiftMessageCard(string fromName)
    {
        var items = _lines.Select(line => line.Sku);

        return _giftMessageFormatter.Format(
            fromName,
            items,
            GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentAuthorizer.Authorize(
            GrandTotal(),
            cardLast4,
            _lines.Count);
    }
}
