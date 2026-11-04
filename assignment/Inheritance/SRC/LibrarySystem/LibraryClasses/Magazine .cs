namespace LibrarySystem;

public class Magazine : LibraryItem
{
    public Magazine(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            loanPeriod: 3,
            baseLateFee: baseLateFee,
            lateFeeMultiplier: 0.5m)
    {
    }
}