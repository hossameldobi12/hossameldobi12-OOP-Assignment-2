namespace LibrarySystem;

public class Person
{
    public int PersonId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }
    private static readonly HashSet<int> _usedPersonIds = new();

    protected Person(int personId, string fullName, string phoneNumber)
    {
        if (personId <= 0)
            throw new ArgumentException("Person ID must be positive.");

        if (_usedPersonIds.Contains(personId))
            throw new InvalidOperationException("Person ID already exists.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be empty.");



        PersonId = personId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        _usedPersonIds.Add(personId);
    }
}