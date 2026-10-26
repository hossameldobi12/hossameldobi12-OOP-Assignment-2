
namespace SrpLab;

public sealed class CensusCsvExporter
{
    public string Export(IEnumerable<WardBedRecord> records)
    {
        var lines = new List<string>
        {
            "bed,patient,acuity"
        };

        foreach (var record in records.OrderBy(record => record.Bed))
        {
            lines.Add(
                $"{record.Bed},{record.PatientId},{record.AcuityScore}"
            );
        }

        return string.Join('\n', lines);
    }
}
