namespace LibrarySystem;

public class Staff : Person
{
    private readonly decimal _responsibilityAllowance;

    public DateTime HireDate { get; }

    public decimal MonthlySalary { get; private set; }

    public decimal MonthlyPay => MonthlySalary + _responsibilityAllowance;

    protected Staff(
        int personId,
        string fullName,
        string phoneNumber,
        DateTime hireDate,
        decimal monthlySalary,
        decimal responsibilityAllowance = 0)
        : base(personId, fullName, phoneNumber)
    {
        if (hireDate.Date > DateTime.Today)
            throw new ArgumentException("Hire date cannot be in the future.");

        if (monthlySalary < 0)
            throw new ArgumentException("Monthly salary cannot be negative.");

        if (responsibilityAllowance < 0)
            throw new ArgumentException(
                "Responsibility allowance cannot be negative.");

        HireDate = hireDate;
        MonthlySalary = monthlySalary;
        _responsibilityAllowance = responsibilityAllowance;
    }

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0)
            throw new ArgumentException(
                "Raise percentage must be greater than zero.");

        MonthlySalary += MonthlySalary * percentage / 100;
    }
}