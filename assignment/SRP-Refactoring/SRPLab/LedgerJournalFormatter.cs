
namespace SrpLab;

public sealed class LedgerJournalFormatter
{
    public string Format(
        string customerId,
        string invoiceNumber,
        decimal amount)
    {
        return $"{customerId},{invoiceNumber},{amount:0.00},AR-SUB";
    }
}
