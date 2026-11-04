
using System.Collections.Generic;

namespace SrpLab;

public sealed class LoanDesk
{
    private readonly LoanRiskCalculator _riskCalculator = new();
    private readonly LoanDocumentPolicy _documentPolicy = new();
    private readonly LoanDecisionLetterFormatter _letterFormatter = new();
    private readonly LoanApplicationCsvFormatter _csvFormatter = new();

    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    public LoanDesk(
        decimal requestedAmount,
        int creditScore,
        int employmentMonths,
        bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }

    public decimal RiskScore()
    {
        return _riskCalculator.CalculateRiskScore(
            RequestedAmount,
            CreditScore,
            EmploymentMonths,
            HasCollateral);
    }

    public bool IsEligible()
    {
        return _riskCalculator.IsEligible(
            RequestedAmount,
            CreditScore,
            EmploymentMonths,
            HasCollateral);
    }

    public IReadOnlyList<string> RequiredDocuments()
    {
        return _documentPolicy.GetRequiredDocuments(
            RequestedAmount,
            HasCollateral,
            EmploymentMonths,
            IsEligible());
    }

    public string DecisionLetter(string applicantName)
    {
        return _letterFormatter.Format(
            applicantName,
            RequestedAmount,
            RiskScore(),
            IsEligible(),
            RequiredDocuments());
    }

    public string UnderwriterCsvRow(string applicationId)
    {
        return _csvFormatter.Format(
            applicationId,
            CreditScore,
            EmploymentMonths,
            HasCollateral,
            RiskScore(),
            IsEligible());
    }
}
