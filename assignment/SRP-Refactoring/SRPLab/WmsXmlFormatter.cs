using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class WmsXmlFormatter
{
    public string Format(
        string batchId,
        IEnumerable<(string Sku, int Allocated)> allocations)
    {
        var parts = allocations
            .Select(item =>
                $"<line sku=\"{item.Sku}\" qty=\"{item.Allocated}\" />");

        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}