namespace LibrarySystem;

public class HeadLibrarian : Staff
{
    public HeadLibrarian(
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
            monthlySalary,
            responsibilityAllowance: 400)
    {
    }

    public void ChangeLateFee(LibraryItem item, decimal newBaseLateFee)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        item.ChangeBaseLateFee(newBaseLateFee);
    }

    public void WithdrawItem(LibraryItem item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        item.Withdraw();
    }

    public void RestoreItem(LibraryItem item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        item.Restore();
    }
}