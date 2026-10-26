namespace LibrarySystem;

public class LibraryItem
{
    private readonly decimal _lateFeeMultiplier;

    public string CatalogNumber { get; }
    public string Title { get; }

    public int LoanPeriod { get; }

    public decimal BaseLateFee { get; private set; }

    public decimal DailyLateFee => BaseLateFee * _lateFeeMultiplier;

    public bool IsWithdrawn { get; private set; }

    public bool IsOnLoan { get; private set; }

    private static readonly HashSet<string> _usedCatalogNumbers =
    new(StringComparer.OrdinalIgnoreCase);

    protected LibraryItem(
        string catalogNumber,
        string title,
        int loanPeriod,
        decimal baseLateFee,
        decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException("Catalog number cannot be empty.");
        if (_usedCatalogNumbers.Contains(catalogNumber))
            throw new InvalidOperationException("Catalog number already exists.");

       


        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        if (loanPeriod <= 0)
            throw new ArgumentException("Loan period must be positive.");

        if (baseLateFee <= 0)
            throw new ArgumentException("Base late fee must be positive.");

        if (lateFeeMultiplier <= 0)
            throw new ArgumentException("Late fee multiplier must be positive.");

        CatalogNumber = catalogNumber;
        Title = title;
        LoanPeriod = loanPeriod;
        BaseLateFee = baseLateFee;
        _lateFeeMultiplier = lateFeeMultiplier;
        _usedCatalogNumbers.Add(catalogNumber);
    }

    public void ChangeBaseLateFee(decimal newBaseLateFee)
    {
        if (newBaseLateFee <= 0)
            throw new ArgumentException("Base late fee must be positive.");

        BaseLateFee = newBaseLateFee;
    }

    public void Withdraw()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException("Item is already withdrawn.");

        IsWithdrawn = true;
    }

    public void Restore()
    {
        if (!IsWithdrawn)
            throw new InvalidOperationException("Item is not withdrawn.");

        IsWithdrawn = false;
    }

    internal void StartLoan()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException(
                "Withdrawn items cannot be borrowed.");

        if (IsOnLoan)
            throw new InvalidOperationException(
                "Item is already on loan.");

        IsOnLoan = true;
    }

    internal void EndLoan()
    {
        if (!IsOnLoan)
            throw new InvalidOperationException(
                "Item is not currently on loan.");

        IsOnLoan = false;
    }
}