
namespace SrpLab;

public sealed class WardBoard
{
    private readonly BedRegistry _bedRegistry = new();
    private readonly AcuityScorer _acuityScorer = new();
    private readonly PagerAlertPolicy _pagerAlertPolicy = new();
    private readonly PagerAlertLog _pagerAlertLog = new();
    private readonly HandoffNoteFormatter _handoffNoteFormatter = new();
    private readonly CensusCsvExporter _censusCsvExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        // Validate and store the patient and acuity information.
        var acuity = ScoreAcuity(heartRate, spo2);

        _bedRegistry.AssignBed(bed, patientId, acuity);

        // Trigger an alert when the policy requires it.
        if (_pagerAlertPolicy.ShouldTrigger(acuity))
        {
            _pagerAlertLog.AddAlert(bed);
        }
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        return _acuityScorer.Score(heartRate, spo2);
    }

    public string BuildHandoffNote(int bed)
    {
        if (!_bedRegistry.TryGetBed(bed, out var record))
        {
            return $"Bed {bed}: empty";
        }

        return _handoffNoteFormatter.Format(bed, record);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pagerAlertLog.Drain();
    }

    public string ExportCensusCsv()
    {
        return _censusCsvExporter.Export(_bedRegistry.GetAllBeds());
    }
}
