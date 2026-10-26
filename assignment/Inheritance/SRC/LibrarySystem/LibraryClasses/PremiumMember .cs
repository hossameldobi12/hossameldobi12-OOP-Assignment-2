namespace LibrarySystem;

public class PremiumMember : Member
{
    public int ReadingPoints
    {
        get
        {
            int points = 0;

            foreach (Loan loan in Loans)
            {
                if (loan.Status == LoanStatus.Returned)
                    points += 5;
            }

            return points;
        }
    }

    public PremiumMember(
        int personId,
        string fullName,
        string phoneNumber,
        decimal discountPercentage)
        : base(
            personId,
            fullName,
            phoneNumber,
            maxLoans: 10,
            discountPercentage: discountPercentage)
    {
    }
}