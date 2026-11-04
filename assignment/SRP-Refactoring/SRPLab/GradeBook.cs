
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly GradeCalculator _gradeCalculator = new();
    private readonly LetterGradePolicy _letterGradePolicy = new();
    private readonly HonorRollPolicy _honorRollPolicy = new();
    private readonly TranscriptFormatter _transcriptFormatter = new();
    private readonly TranscriptCsvFormatter _csvFormatter = new();

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(score));

        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }

        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list) || list.Count == 0)
            return 0m;

        return _gradeCalculator.CalculateAverage(list);
    }

    public string Letter(string studentId)
    {
        var average = Average(studentId);
        return _letterGradePolicy.GetLetter(average);
    }

    public bool MeetsHonorRoll(string studentId)
    {
        var average = Average(studentId);
        var letter = Letter(studentId);

        return _honorRollPolicy.MeetsRequirements(average, letter);
    }

    public string TranscriptPlain(string studentId, string fullName)
    {
        var average = Average(studentId);
        var letter = Letter(studentId);
        var honorRoll = MeetsHonorRoll(studentId);

        return _transcriptFormatter.Format(
            studentId,
            fullName,
            average,
            letter,
            honorRoll);
    }

    public string ExportCsv()
    {
        var students = _scores.Keys.Select(id =>
        {
            var average = Average(id);
            var letter = Letter(id);
            var honor = MeetsHonorRoll(id);

            return (StudentId: id, Average: average, Letter: letter, Honor: honor);
        });

        return _csvFormatter.Format(students);
    }
}
