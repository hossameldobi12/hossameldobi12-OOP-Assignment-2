
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

public sealed class LoanDecisionLetterFormatter
{
    public string Format(
        string applicantName,
        decimal requestedAmount,
        decimal riskScore,
        bool isEligible,
        IEnumerable<string> requiredDocuments)
    {
        if (isEligible)
        {
            return $"Dear {applicantName},\nYour request for {requestedAmount:C} is pre-approved (risk {riskScore:0}).\n" +
                   $"Please upload: {string.Join("; ", requiredDocuments)}.\n";
        }

        return $"Dear {applicantName},\nWe are unable to approve {requestedAmount:C} at this time.\n" +
               $"Reference risk={riskScore:0}. You may reapply after improving documentation.\n";
    }
}
