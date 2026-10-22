
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class ThermalTicketFormatter
{
    public string Format(
        int orderNumber,
        IEnumerable<string> items,
        int estimatedMinutes,
        IEnumerable<string> allergens)
    {
        var width = 32;
        var line = new string('=', width);

        var body = string.Join('\n', items);
        var allergenList = allergens.ToList();

        var allergyLine = allergenList.Count == 0
            ? "ALLERGENS: none"
            : "ALLERGENS: " + string.Join(",", allergenList);

        return $"{line}\nORDER #{orderNumber}\nETA {estimatedMinutes} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}
