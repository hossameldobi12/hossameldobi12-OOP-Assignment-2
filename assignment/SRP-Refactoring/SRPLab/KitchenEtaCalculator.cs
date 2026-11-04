
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class KitchenEtaCalculator
{
    public int Calculate(
        IEnumerable<int> prepMinutes,
        int openStations,
        bool hasAllergens)
    {
        if (openStations <= 0)
            openStations = 1;

        var durations = prepMinutes.ToList();
        var sequential = durations.Sum();
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);

        if (hasAllergens)
            parallel += 3;

        var longest = durations.Count == 0 ? 0 : durations.Max();

        return Math.Max(parallel, longest);
    }
}
