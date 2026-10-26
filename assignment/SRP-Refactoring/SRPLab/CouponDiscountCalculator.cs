
using System;

namespace SrpLab;

public sealed class CouponDiscountCalculator
{
    public decimal Calculate(string? couponText, decimal subtotal)
    {
        if (string.IsNullOrWhiteSpace(couponText))
            return 0m;

        var coupon = couponText.Trim().ToUpperInvariant();

        if (coupon.StartsWith("SAVE") &&
            int.TryParse(coupon[4..], out var percentage) &&
            percentage is > 0 and <= 50)
        {
            return Math.Round(subtotal * percentage / 100m, 2);
        }

        if (coupon.Contains("FREESHIP"))
            return 0m;

        if (coupon == "WELCOME10")
            return Math.Min(10m, subtotal);

        return 0m;
    }
}
