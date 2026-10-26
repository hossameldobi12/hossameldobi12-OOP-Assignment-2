namespace LibrarySystem;

public class StudentMember : Member
{
    public StudentMember(
        int personId,
        string fullName,
        string phoneNumber)
        : base(
            personId,
            fullName,
            phoneNumber,
            maxLoans: 3,
            discountPercentage: 0)
    {
    }
}