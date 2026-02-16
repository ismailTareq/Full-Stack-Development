// Question 1
double d = 9.99;
int x = (int)d;
Console.WriteLine($" {x}");
Console.WriteLine("Explanation: Casting double to int truncates the decimal portion (no rounding).");
Console.WriteLine("Result: 9\n");

// Question 2
int n = 5;
double d2 = n / 2.0;  
Console.WriteLine($"{d2}");  // Prints 2.5
Console.WriteLine("Explanation: Integer division returns integer. Changing to 2.0 forces floating point division.\n");

// Question 3
Console.Write("Enter age: ");
string input = Console.ReadLine();
int age = int.Parse(input);
Console.WriteLine($"Age entered: {age}\n");

// Question 4
try
{
    string s = "12a";
    int x = int.Parse(s);
    Console.WriteLine($"{x}");
}
catch (FormatException)
{
    Console.WriteLine("FormatException occurs!");
    Console.WriteLine("Explanation: '12a' contains non-numeric characters 'a'. int.Parse() requires a valid numeric string.\n");
}

// Question 5
string s = "12a";
if (int.TryParse(s, out int x))
{
    Console.WriteLine($"{x}");
}
else
{
    Console.WriteLine("Invalid\n");
}

// Question 6
object o = 10;
int a = (int)o;
Console.WriteLine($"{a + 1}");  // Prints 11
Console.WriteLine("Explanation: Boxing stores int in object. Unboxing converts back to int. Then 10 + 1 = 11.\n");

// Question 7
try
{
    object o = 10;
    long x = (long)o;  // This line throws InvalidCastException
    Console.WriteLine($"{x}");
}
catch (InvalidCastException)
{
    Console.WriteLine("InvalidCastException occurs!");
    Console.WriteLine("Explanation: Cannot directly unbox int to long. Must unbox to int first.");
    
    // Fixed version:
    object o2 = 10;
    long x2 = (int)o2;  
    Console.WriteLine($"Fixed: {x2}\n");
}

// Question 8
object o = "not a number";  // Test case - will fail conversion
long x = 0;

if (o is int intVal)
{
    x = intVal;
}
else if (o is string strVal && long.TryParse(strVal, out long parsedVal))
{
    x = parsedVal;
}
else
{
    x = -1;  // Conversion not possible
}

Console.WriteLine($"Q8: {x}\n");

// Question 9
string? name = null;
Console.WriteLine(name?.Length);  // Prints blank line
Console.WriteLine("Explanation: ?. returns null when object is null. Console.WriteLine writes nothing for null.\n");

// Question 10
string? name2 = null;
int length = name2?.Length ?? 0;
Console.WriteLine($"{length}");  // Prints 0
Console.WriteLine("Explanation: name2?.Length returns null, ?? returns right operand (0) since left is null.\n");

// Question 11
string? s = null;
int x = int.Parse(s ?? "0");
Console.WriteLine($"{x}");  // Prints 0
Console.WriteLine("Explanation: Nothing is wrong! Code works correctly. ?? provides '0' when s is null.\n");

// Question 12
try
{
    string? s = null;
    // Console.WriteLine(s!Length);  // Syntax error - missing dot
    Console.WriteLine(s!.Length);  // NullReferenceException at runtime
}
catch (NullReferenceException)
{
    Console.WriteLine("NullReferenceException occurs!");
    Console.WriteLine("Explanation: ! suppresses null warning but doesn't prevent runtime exception.");
    
    // Fixed version:
    string? s2 = null;
    Console.WriteLine($"{s2?.Length ?? 0}\n");
}

// Question 13
string? s = null;
int x = Convert.ToInt32(s);  // Note: Tolnt32 is typo, should be ToInt32
Console.WriteLine($"{x}");  // Prints 0
Console.WriteLine("Explanation: Convert.ToInt32(null) returns 0 (doesn't throw exception).\n");

// Question 14
string? s = null;
Console.WriteLine("Q14: Comparison:");
try
{
    int a = int.Parse(s);  // Throws ArgumentNullException
    Console.WriteLine($"A: {a}");
}
catch (ArgumentNullException)
{
    Console.WriteLine("A: int.Parse(null) - Throws ArgumentNullException");
}
int b = Convert.ToInt32(s);  // Returns 0
Console.WriteLine($"B: Convert.ToInt32(null) - Returns {b}");
Console.WriteLine("Summary: Convert.ToInt32() handles null gracefully, int.Parse() does not.\n");

// Question 15
string? user = null;
Console.WriteLine($"{user?.ToUpper() ?? "Guest"}");

user = "john";
Console.WriteLine($"(with user): {user?.ToUpper() ?? "Guest"}");

user = "";
Console.WriteLine($"(with empty): {user?.ToUpper() ?? "Guest"}");

