
namespace SrpLab;

public sealed class PaymentAuthorizer
{
    public string Authorize(decimal total, string cardLast4, int itemCount)
    {
        var payload = $"{total:0.00}|{cardLast4}|{itemCount}";
        var hash = payload.GetHashCode();

        return $"AUTH-{Math.Abs(hash):X8}";
    }
}
