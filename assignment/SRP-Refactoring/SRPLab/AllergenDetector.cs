
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class AllergenDetector
{
    public IReadOnlyList<string> Detect(IEnumerable<string> ingredients)
    {
        var allergens = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var ingredient in ingredients)
        {
            var ing = ingredient.Trim().ToLowerInvariant();

            if (ing.Contains("milk") || ing.Contains("cheese") || ing.Contains("butter"))
                allergens.Add("dairy");

            if (ing.Contains("wheat") || ing.Contains("flour") || ing.Contains("bread"))
                allergens.Add("gluten");

            if (ing.Contains("peanut") || ing.Contains("almond") || ing.Contains("cashew"))
                allergens.Add("nuts");

            if (ing.Contains("shrimp") || ing.Contains("prawn") || ing.Contains("crab"))
                allergens.Add("shellfish");
        }

        return allergens.OrderBy(x => x).ToList();
    }
}
