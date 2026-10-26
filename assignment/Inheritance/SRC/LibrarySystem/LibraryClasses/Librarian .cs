namespace LibrarySystem;

public class Librarian : Staff
{
    public Librarian(
        int personId,
        string fullName,
        string phoneNumber,
        DateTime hireDate,
        decimal monthlySalary)
        : base(
            personId,
            fullName,
            phoneNumber,
            hireDate,
            monthlySalary)
    {
    }

    public void ProcessReturn(Loan loan, DateTime returnDate)
    {
        if (loan is null)
            throw new ArgumentNullException(nameof(loan));

        loan.Return(returnDate);
    }

    public void MarkItemAsLost(Loan loan)
    {
        if (loan is null)
            throw new ArgumentNullException(nameof(loan));

        loan.MarkAsLost();
    }
}