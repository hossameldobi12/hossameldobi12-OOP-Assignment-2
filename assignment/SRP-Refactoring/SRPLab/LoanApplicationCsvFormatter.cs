
namespace SrpLab;

public sealed class LoanApplicationCsvFormatter
{
    public string Format(
        string applicationId,
        int creditScore,
        int employmentMonths,
        bool hasCollateral,
        decimal riskScore,
        bool isEligible)
    {
        return $"{applicationId},{creditScore},{employmentMonths},{(hasCollateral ? 1 : 0)},{riskScore:0.00},{(isEligible ? "Y" : "N")}";
    }
}
