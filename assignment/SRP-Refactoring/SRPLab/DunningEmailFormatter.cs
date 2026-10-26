
using System;

namespace SrpLab;

public sealed class DunningEmailFormatter
{
    public string Format(
        string customerName,
        string invoiceNumber,
        decimal amount,
        DateOnly asOf,
        int failedPayments)
    {
        var severity = failedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };

        return $"Subject: {severity} {invoiceNumber}\nHi {customerName},\nBalance {amount:C} as of {asOf:o} ({failedPayments} failures).\n";
    }
}
