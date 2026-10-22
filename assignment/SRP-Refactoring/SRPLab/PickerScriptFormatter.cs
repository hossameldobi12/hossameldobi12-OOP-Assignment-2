using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class PickerScriptFormatter
{
    public string Format(
        IEnumerable<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder,
        IEnumerable<string> shortfallSkus)
    {
        var steps = walkingOrder
            .Select((item, index) =>
                $"{index + 1}. Go aisle {item.Aisle} bin {item.Bin}: pick {item.Qty} × {item.Sku}");

        var shortfalls = shortfallSkus.ToList();

        var warning = shortfalls.Any()
            ? "SHORTAGES: " + string.Join(", ", shortfalls)
            : "SHORTAGES: none";

        return string.Join('\n', steps) + "\n" + warning;
    }
}