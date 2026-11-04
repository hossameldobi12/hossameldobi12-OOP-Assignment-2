using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class GradeCalculator
{
    public decimal CalculateAverage(IEnumerable<decimal> scores)
    {
        var grades = scores.ToList();

        if (grades.Count == 0)
            return 0m;

        return decimal.Round(grades.Average(), 2);
    }
}