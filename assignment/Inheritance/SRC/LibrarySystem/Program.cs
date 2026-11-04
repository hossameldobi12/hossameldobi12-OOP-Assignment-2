using LibrarySystem;

StudentMember student = new StudentMember( 1, "Ahmed Ali", "01000000001");

PremiumMember premium = new PremiumMember(2, "Sara Mohamed", "01000000002", 20m);

Librarian librarian = new Librarian(3, "Mona Hassan", "01000000003", new DateTime(2024, 1, 1), 10000m);

Shelver shelver = new Shelver(4, "Omar Adel", "01000000004", new DateTime(2024, 2, 1), 8000m, "Science");

HeadLibrarian head = new HeadLibrarian(5, "Nour Ahmed", "01000000005",new DateTime(2023, 1, 1), 12000m);

List<LibraryItem> items = new()
{
    new Book("B01", "C# Basics", 10m),
    new DVD("D01", "C# Videos", 10m),
    new Magazine("M01", "Tech Monthly", 10m)
};

foreach (LibraryItem item in items)
{
    Console.WriteLine(
        $"{item.Title} | Loan Period: {item.LoanPeriod} days | " +
        $"Daily Late Fee: {item.DailyLateFee}");
}

List<Staff> staffMembers = new()
{
    librarian,
    shelver,
    head
};

foreach (Staff staff in staffMembers)
{
    Console.WriteLine(
        $"{staff.FullName} | Monthly Pay: {staff.MonthlyPay}");
}
Console.WriteLine("\n=== LOAN TEST ===");

DVD dvd = new DVD("D02", "C# Advanced", 10m);

DateTime borrowDate = new DateTime(2026, 10, 1);

Loan loan = premium.BorrowItem(
    loanId: 1,
    item: dvd,
    borrowDate: borrowDate);

Console.WriteLine($"Loan ID: {loan.LoanId}");
Console.WriteLine($"Borrow Date: {loan.BorrowDate:d}");
Console.WriteLine($"Due Date: {loan.DueDate:d}");
Console.WriteLine($"Status: {loan.Status}");
Console.WriteLine($"Is On Loan: {dvd.IsOnLoan}");

librarian.ProcessReturn(
    loan,
    new DateTime(2026, 10, 13));

Console.WriteLine($"Return Date: {loan.ReturnDate:d}");
Console.WriteLine($"Status: {loan.Status}");
Console.WriteLine($"Late Fee: {loan.LateFee}");
Console.WriteLine($"Reading Points: {premium.ReadingPoints}");
Console.WriteLine($"Is On Loan: {dvd.IsOnLoan}");


Console.WriteLine("\n=== DOUBLE RETURN TEST ===");

try
{
    librarian.ProcessReturn(
        loan,
        new DateTime(2026, 10, 14));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}
Console.WriteLine("\n=== DOUBLE BORROW TEST ===");

Book book = new Book("B02", "OOP Basics", 5m);

Loan firstLoan = student.BorrowItem(
    2, book, new DateTime(2026, 10, 1));

try
{
    premium.BorrowItem(
        3, book, new DateTime(2026, 10, 2));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}
Console.WriteLine("\n=== STUDENT LOAN LIMIT TEST ===");

student.BorrowItem(
    4,
    new DVD("D03", "DVD 3", 5m),
    new DateTime(2026, 10, 1));

student.BorrowItem(
    5,
    new Magazine("M02", "Magazine 2", 5m),
    new DateTime(2026, 10, 1));

try
{
    student.BorrowItem(
        6,
        new Book("B03", "Book 3", 5m),
        new DateTime(2026, 10, 1));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

Console.WriteLine($"Active Loans: {student.ActiveLoanCount}");


Console.WriteLine("\n=== WITHDRAWN ITEM TEST ===");

Book withdrawnBook = new Book("B04", "Design Patterns", 10m);
head.WithdrawItem(withdrawnBook);

try
{
    premium.BorrowItem(
        7,
        withdrawnBook,
        new DateTime(2026, 10, 26));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

Console.WriteLine("\n=== MARK RETURNED LOAN AS LOST TEST ===");

try
{
    librarian.MarkItemAsLost(loan);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

Console.WriteLine("\n=== STAFF ACTIONS TEST ===");

// Give Raise
librarian.GiveRaise(10);
Console.WriteLine($"Salary After Raise: {librarian.MonthlySalary}");

// Reassign Shelver
Console.WriteLine($"Old Section: {shelver.Section}");
shelver.Reassign("Math");
Console.WriteLine($"New Section: {shelver.Section}");

// Change Late Fee
Book testBook = new Book("B05", "C# Advanced", 10m);
Console.WriteLine($"Old Daily Late Fee: {testBook.DailyLateFee}");

head.ChangeLateFee(testBook, 15m);
Console.WriteLine($"New Daily Late Fee: {testBook.DailyLateFee}");

Console.WriteLine("\n=== INVALID INPUT TEST ===");

// Raise must be greater than zero
try
{
    librarian.GiveRaise(0);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Raise Rejected: {ex.Message}");
}

// Section cannot be empty
try
{
    shelver.Reassign("");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Section Rejected: {ex.Message}");
}

// Late fee must be positive
try
{
    head.ChangeLateFee(testBook, 0);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Late Fee Rejected: {ex.Message}");
}

try
{
    StudentMember dateTestStudent = new StudentMember(
        100,
        "Date Test Student",
        "01000000000");

    Loan invalidDateLoan = dateTestStudent.BorrowItem(
        100,
        new Book("B100", "Testing Dates", 10m),
        new DateTime(2026, 10, 20));

    librarian.ProcessReturn(
        invalidDateLoan,
        new DateTime(2026, 10, 19));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Return Date Rejected: {ex.Message}");
}

Console.WriteLine("\n=== DUPLICATE PERSON ID TEST ===");

try
{
    StudentMember duplicateStudent = new StudentMember(
        student.PersonId,
        "Another Student",
        "01099999999");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

Console.WriteLine("\n=== DUPLICATE CATALOG NUMBER TEST ===");

try
{
    Book duplicateBook = new Book(
        "B01",
        "Another Book",
        10m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}
Console.WriteLine("\n=== DUPLICATE LOAN ID TEST ===");

try
{
    premium.BorrowItem(
        1,
        new Book("B200", "Another Book", 10m),
        new DateTime(2026, 10, 26));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}




// === MUST NOT COMPILE ===

// Parent classes cannot be instantiated directly:
// Person person = new Person(10, "Test", "01000000000");
// Member member = new Member(11, "Test", "01000000001", 3, 0m);
// Staff staff = new Staff(12, "Test", "01000000002", DateTime.Today, 5000m);
// LibraryItem item = new LibraryItem("X01", "Test", 7, 10m, 1m);

// Identity properties are read-only:
// student.FullName = "New Name";

// Loans cannot be modified from outside:
// student.Loans.Add(loan);
// student.Loans.Remove(loan);

// IsOnLoan cannot be set from outside:
// dvd.IsOnLoan = true;