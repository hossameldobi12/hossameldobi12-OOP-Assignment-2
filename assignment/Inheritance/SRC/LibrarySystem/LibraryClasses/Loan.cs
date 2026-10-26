namespace LibrarySystem;

public class Loan
{
    public int LoanId { get; }
    public Member Member { get; }
    public LibraryItem Item { get; }
    public DateTime BorrowDate { get; }
    public DateTime DueDate => BorrowDate.AddDays(Item.LoanPeriod);

    public LoanStatus Status { get; private set; }

    public DateTime? ReturnDate { get; private set; }

    private static readonly HashSet<int> _usedLoanIds = new();

    public decimal LateFee
    {
        get
        {
            if (Status != LoanStatus.Returned || ReturnDate is null)
                return 0;

            int lateDays = (ReturnDate.Value.Date - DueDate.Date).Days;

            if (lateDays <= 0)
                return 0;

            decimal fee = lateDays * Item.DailyLateFee;

            return fee * (1 - Member.DiscountPercentage / 100m);
        }
    }

    internal Loan(
        int loanId,
        Member member,
        LibraryItem item,
        DateTime borrowDate)
    {
        if (loanId <= 0)
            throw new ArgumentException("Loan ID must be positive.");

        if (_usedLoanIds.Contains(loanId))
            throw new InvalidOperationException("Loan ID already exists.");

        if (member is null)
            throw new ArgumentNullException(nameof(member));

        if (item is null)
            throw new ArgumentNullException(nameof(item));

        if (borrowDate == default)
            throw new ArgumentException("Borrow date must be valid.");

        LoanId = loanId;
        Member = member;
        Item = item;
        BorrowDate = borrowDate;
        Status = LoanStatus.Borrowed;

        Item.StartLoan();
        _usedLoanIds.Add(loanId);
    }

    public void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only borrowed loans can be returned.");

        if (returnDate == default || returnDate.Date < BorrowDate.Date)
            throw new ArgumentException(
                "Return date cannot be before borrow date.");

        Item.EndLoan();

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only borrowed loans can be marked as lost.");

        Item.EndLoan();

        Status = LoanStatus.Lost;
    }
}