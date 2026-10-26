
namespace SrpLab;

public sealed class BedRegistry
{
    private readonly Dictionary<int, WardBedRecord> _beds = new();

    public void AssignBed(int bed, string patientId, int acuityScore)
    {
        if (bed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bed));
        }

        if (string.IsNullOrWhiteSpace(patientId))
        {
            throw new ArgumentException("patient required");
        }

        var normalizedPatientId = patientId.Trim().ToUpperInvariant();

        _beds[bed] = new WardBedRecord(
            bed,
            normalizedPatientId,
            acuityScore
        );
    }

    public bool TryGetBed(int bed, out WardBedRecord record)
    {
        return _beds.TryGetValue(bed, out record!);
    }

    public IReadOnlyList<WardBedRecord> GetAllBeds()
    {
        return _beds.Values
            .OrderBy(record => record.Bed)
            .ToList();
    }
}
