namespace SrpLab;

public sealed class HonorRollPolicy
{
    public bool MeetsRequirements(decimal average, string letter)
    {
        return average >= 85 && letter is "A" or "B";
    }
}