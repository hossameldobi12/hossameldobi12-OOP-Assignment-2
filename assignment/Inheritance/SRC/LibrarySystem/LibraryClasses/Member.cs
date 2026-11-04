namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new();
    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

    public int MaxLoans { get; }
    public decimal DiscountPercentage { get; }


    public int ActiveLoanCount
    {
        get
        {
            int count = 0;

            foreach (Loan loan in _loans)
            {
                if (loan.Status == LoanStatus.Borrowed)
                    count++;
            }

            return count;
        }
    }

    protected Member(
        int personId,
        string fullName,
        string phoneNumber,
        int maxLoans,
        decimal discountPercentage)
        : base(personId, fullName, phoneNumber)
    {
        if (maxLoans <= 0)
            throw new ArgumentException("Maximum loans must be positive.");

        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException(
                "Discount percentage must be between 0 and 100.");

        MaxLoans = maxLoans;
        DiscountPercentage = discountPercentage;
    }

    public Loan BorrowItem(
    int loanId,
    LibraryItem item,
    DateTime borrowDate)
    {
        if (ActiveLoanCount >= MaxLoans)
            throw new ArgumentException("Member has reached the maximum number of active loans.");

        if (item is null)
            throw new ArgumentNullException(nameof(item));

        if (borrowDate == default)
            throw new ArgumentException("Borrow date must be valid.");

        Loan loan = new Loan(loanId, this, item, borrowDate);

        _loans.Add(loan);

        return loan;
    }
}