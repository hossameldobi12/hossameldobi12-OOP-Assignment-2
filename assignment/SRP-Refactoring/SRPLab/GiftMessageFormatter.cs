
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class GiftMessageFormatter
{
    public string Format(
        string fromName,
        IEnumerable<string> items,
        decimal total)
    {
        var itemNames = string.Join(", ", items);

        return $"Dear friend,\nA gift from {fromName} awaits ({itemNames}).\nTotal surprise value: {total:C}\n";
    }
}
