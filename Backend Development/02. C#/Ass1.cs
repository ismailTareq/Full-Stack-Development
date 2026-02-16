// Question 1
// Region and endregion directives are compiler directives don't effect the runtime behavior of the code.
// They are used to organize code into collapsible sections in the code editor for better readability.

#region Properties
public string Name { get; set; }
public int Age { get; set; }
#endregion

#region Methods
public void Display() { }
public void Calculate() { }
#endregion

// Question 2
// Exciplit
int age = 25;
string name = "John";
double price = 19.99;

// Implicit
var city = "New York";
var isActive = true;
var score = 95.5;

// Question 3 
// Constants used to define values that should not change throughout the execution of a program.
const double PI = 3.14159;
const int MAX_SIZE = 100;
const string APP_NAME = "Ismail's Application";

// Question 4
// class Variables declared in class (fields) accessible to all methods.
public class MyClass
{
    // Class-level scope
    private int counter = 0;
    
    public void Method1()
    {
        counter = 5;  // Can access
    }
    
    public void Method2()
    {
        counter = 10; // Can access
    }
}

//Method Variables declared within a method, accessible only within that method.
public void MyMethod()
{
    int localVar = 10;
    // Only accessible within MyMethod
}

// Question 5
// Variables declared within a block {}
public void Example()
{
    // Outer scope
    int x = 10;
    
    if (x > 5)
    {
        // Block-level scope
        int y = 20;  // Only accessible in this if block
        Console.WriteLine(y);  // OK
    }
    
    // Console.WriteLine(y);  // ERROR
    // y doesn't exist here anymore
}

// Question 6
// Local variables Live only while the method runs.
// Static variables Live for the entire program.
void Counter()
{
    int count = 0;  // Dies after method ends
    static int total = 0;  // Lives forever
    count++;
    total++;
}

// Question 7
// Garbage collection  Automatically removes unused objects from memory so Objects live until no one uses them

// Question 8
int x = 10;
{
    int x = 20;  // This shadows the outer x
    // inner x = 20, outer x still = 10
}
// Outer x = 10

// Question 9
// Start with letter or _
// Can contain letters, digits, _
// No spaces
// Cannot be C# keywords (like int, class)
// Case-sensitive (age ≠ Age)

// Question 10
// Local variables: camelCase (totalCount)
// Class names: PascalCase (CustomerOrder)
// Constants: ALL_CAPS (MAX_SIZE)

// Question 11
// Syntax error: Wrong code grammar
// Runtime error: Error during execution
// Logical error: Wrong output due to faulty logic 

// Question 12
// Prevents program crashes when errors happen without it the program may terminate unexpectedly.

// Question 13
try
{
    // Try to run this code
    File.Open("test.txt");
}
catch
{
    // Run if error occurs
    Console.WriteLine("Error!");
}
finally
{
    // Always runs, error or not
    Console.WriteLine("Cleanup");
}

// Question 14
// NullReferenceException - Using null object
// DivideByZeroException - Dividing by zero
// FileNotFoundException - File doesn't exist
// IndexOutOfRangeException - Array index too big
// FormatException - Invalid format conversion
// InvalidOperationException - Invalid operation in current state

// Question 15
try
{
    // 2Y code that may throw exceptions
}
catch (FileNotFoundException ex)  // Specific first
{}
catch (Exception ex)  // General last
{}

// Question 16
// throw is used to re-throw the current exception preserving the original stack trace that's why it's preferred in most cases.
catch (Exception ex)
{
    throw;      // Keeps original error details so that stack trace is preserved 
    throw ex;   // Loses original error details 
}

// Question 17
// Stack: Fast memory for simple data (ints, bools). Cleared automatically.
// Heap: Slower memory for complex objects (classes). Garbage Collector cleans it.

int x = 5; // stack
string name = "John"; // heap

// Question 18
// this is called deep copy
int a = 10;
int b = a;  // b gets a copy (10)
a = 20;     // b is still 10

// Question 19
// this is called shallow copy or reference copy
List<int> list1 = new List<int>();
List<int> list2 = list1;  // Both point to same list
list1.Add(5);            // list2 also has 5

// Question 20
// Everything in C# inherits from object
object obj = "Hello"; // string is an object
object num = 42;      // int is an object
object list = new List<int>(); // List is an object
// Every thing is an object
