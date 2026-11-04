
namespace SrpLab;

public sealed class PagerAlertPolicy
{
    public bool ShouldTrigger(int acuityScore)
    {
        return acuityScore >= 8;
    }
}
