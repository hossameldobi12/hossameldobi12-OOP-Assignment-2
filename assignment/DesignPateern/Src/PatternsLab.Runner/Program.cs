using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;
using System.Diagnostics;
using static PatternsLab.Problems.Builder.CourseRegistrationBuilder;


Console.WriteLine("=== SINGLETON: BEFORE ===\n");
var db = new DatabaseService();
var ui = new UiService();
Console.WriteLine();
db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");
ui.Render();
Console.WriteLine($"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");



Console.WriteLine("\n=== PROTOTYPE PATTERN ===\n");

// R1: Traditional creation
Console.WriteLine("--- Traditional Creation ---");

var sw = Stopwatch.StartNew();
var army = new List<Enemy>();

for (int i = 1; i <= 5; i++)
{
    var orc = new Orc();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}

sw.Stop();
Console.WriteLine($"Created 5 orcs using new in {sw.ElapsedMilliseconds} ms\n");

// R1: Prototype cloning
Console.WriteLine("--- Prototype Cloning ---");

EnemyRegistry.Register("orc", new Orc());
EnemyRegistry.Register("elf", new Elf());

sw.Restart();

var prototypeArmy = new List<Enemy>();

for (int i = 1; i <= 5; i++)
{
    var orc = EnemyRegistry.Create("orc");
    orc.Name = $"Orc-{i}";
    prototypeArmy.Add(orc);
}

sw.Stop();
Console.WriteLine($"Created 5 orcs using Prototype in {sw.ElapsedMilliseconds} ms\n");

// R2: Client uses the base type Enemy
Console.WriteLine("--- Base Type Test ---");

Enemy original = EnemyRegistry.Create("orc");
original.Name = "Boss Orc";

Enemy copy = EnemyCopyHelper.CopyEnemy(original);

Console.WriteLine($"Original type: {original.GetType().Name}");
Console.WriteLine($"Copy type:     {copy.GetType().Name}");

// R3: Private state is copied
Console.WriteLine("\n--- Private State Test ---");

Console.WriteLine($"Original model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");
Console.WriteLine($"Same model id:     {original.ModelId == copy.ModelId}");

// R4: Deep copy test
Console.WriteLine("\n--- Deep Copy Test ---");

copy.Weapon.Damage = 999;
copy.Abilities.Add("Fire Breath");

Console.WriteLine($"Original weapon damage: {original.Weapon.Damage}");
Console.WriteLine($"Copy weapon damage:     {copy.Weapon.Damage}");

Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");
Console.WriteLine($"Copy abilities:     {string.Join(", ", copy.Abilities)}");

Console.WriteLine($"Same Weapon object:    {ReferenceEquals(original.Weapon, copy.Weapon)}");
Console.WriteLine($"Same Abilities list:   {ReferenceEquals(original.Abilities, copy.Abilities)}");

// R5: Registry returns independent clones
Console.WriteLine("\n--- Registry Test ---");

Enemy firstOrc = EnemyRegistry.Create("orc");
Enemy secondOrc = EnemyRegistry.Create("orc");

Console.WriteLine($"Same model id: {firstOrc.ModelId == secondOrc.ModelId}");
Console.WriteLine($"Same object:   {ReferenceEquals(firstOrc, secondOrc)}");




Console.WriteLine("\n=== BUILDER: BEFORE ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudentUgly());
Console.WriteLine(RegistrationCallSites.CreateVideosOnlyUgly());

Console.WriteLine("\n=== BUILDER: INVALID CASES ===\n");


try
{
    new CourseRegistrationBuilder()
        .WithStudentEmail("test@mail.com")
        .ForCourse("SEF-101")
        .AsLiveGroup("")
        .Build();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"LiveGroup error: {ex.Message}");
}

try
{
    new CourseRegistrationBuilder()
        .WithStudentEmail("test@mail.com")
        .ForCourse("SEF-101")
        .AsLiveGroup("G1")
        .AsVideosOnly()
        .Build();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"VideosOnly error: {ex.Message}");
}