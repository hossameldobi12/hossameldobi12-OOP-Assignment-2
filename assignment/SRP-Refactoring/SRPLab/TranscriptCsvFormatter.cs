using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class TranscriptCsvFormatter
{
    public string Format(
        IEnumerable<(string StudentId, decimal Average, string Letter, bool Honor)> students)
    {
        var rows = new List<string>
        {
            "studentId,average,letter,honor"
        };

        foreach (var student in students.OrderBy(student => student.StudentId))
        {
            rows.Add(
                $"{student.StudentId},{student.Average},{student.Letter},{(student.Honor ? 1 : 0)}");
        }

        return string.Join('\n', rows);
    }
}