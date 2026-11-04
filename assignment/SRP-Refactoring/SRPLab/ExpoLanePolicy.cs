
namespace SrpLab;

public sealed class ExpoLanePolicy
{
    public string GetLane(bool hasAllergens, int estimatedMinutes)
    {
        if (hasAllergens)
            return "LANE-ALLERGY";

        return estimatedMinutes > 20
            ? "LANE-SLOW"
            : "LANE-FAST";
    }
}
