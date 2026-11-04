namespace SrpLab;

public sealed class TranscriptFormatter
{
    public string Format(
        string studentId,
        string fullName,
        decimal average,
        string letter,
        bool honorRoll)
    {
        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {average}\nLetter: {letter}\nHonor: {honorRoll}\n";
    }
}