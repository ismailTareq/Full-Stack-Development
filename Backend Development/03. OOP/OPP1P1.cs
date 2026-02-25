// PART 1
// Question 1
// In C#, both class and struct let you group related data and behavior, but they work very differently under the hood.
// The critical distinction is that a class is a reference type while a struct is a value type.
// When you assign a class instance to another variable, both variables point to the same object in memory a change through one variable is seen by the other.
// Structs are copied entirely when assigned, so each variable is completely independent.
// in a nut-shell Use struct for small, simple data (like coordinates or colors) and class for complex objects that need to be shared or have identity across your program.
class PlayerClass
{
    public string Name;
    public int Score;
}
 
// STRUCT (Value Type)
struct PlayerStruct
{
    public string Name;
    public int Score;
}
 
class Program
{
    static void Main()
    {
        // Class behavior: reference copy
        PlayerClass p1 = new PlayerClass { Name = "Alice", Score = 100 };
        PlayerClass p2 = p1;          // p2 points to the same object
        p2.Score = 999;
        Console.WriteLine(p1.Score);  // Output: 999 (changed via p2)
 
        // Struct behavior: value copy
        PlayerStruct s1 = new PlayerStruct { Name = "Bob", Score = 100 };
        PlayerStruct s2 = s1;         // s2 is a SEPARATE copy
        s2.Score = 999;
        Console.WriteLine(s1.Score);  // Output: 100 (s1 unchanged)
    }
}

// Question 2
// Access modifiers control which parts of your code can see and use a class member.
// public means it's accessible from anywhere, while private restricts access to code inside the same class.
// This is the foundation of encapsulation protecting internal state and only exposing what's necessary.
// A practical rule: keep data private and expose it through public methods or properties.
// This prevents outside code from corrupting your object's state.
class BankAccount
{
    public string OwnerName;     // anyone can read/write this
    private double balance;      // only this class can touch this
 
    public BankAccount(string name, double initialAmount)
    {
        OwnerName = name;
        balance = initialAmount;
    }
 
    public void Deposit(double amount)
    {
        if (amount > 0)
            balance += amount;
    }
 
    public double GetBalance()
    {
        return balance;
    }
}
 
class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount("Sara", 500);
        account.Deposit(200);
        Console.WriteLine(account.GetBalance());  // 700
        // account.balance = -99999;  <- ERROR: private
    }
}
// By keeping balance private, it's impossible for external code to set it to an invalid value.
// Every modification goes through Deposit(), which enforces our business rules.

// Question 3
// A class library compiles to a .dll file 
// 1. Create the Library
// 2. Write Your Classes
// 3. Build the Library
// 4. Add a Console App
// 5. Reference the Library
// 6. Use it by using using the namespace

// Question 4
// A class library is a compiled, reusable collection of types classes, interfaces, enums that can be shared across multiple projects without copying code.
// It's compiled into a .dll that other applications simply reference.
// it matters 3lshan Reusability, Easier in testing, and Separation of Concerns.






