
namespace SrpLab;

public sealed class HandoffNoteFormatter
{
    public string Format(int bed, WardBedRecord record)
    {
        var tone = record.AcuityScore >= 8
            ? "ESCALATE"
            : record.AcuityScore >= 4
                ? "WATCH"
                : "STABLE";

        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] " +
               $"Bed {bed} · {record.PatientId} · " +
               $"acuity={record.AcuityScore} · {tone}";
    }
}
