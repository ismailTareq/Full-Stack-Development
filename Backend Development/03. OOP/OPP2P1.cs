// Part1

// Question1

// A)
// 1. fih moshkila fih public string Owner and balance, 
// anyone can change them directly without any check. 
// 2. No validation logic Since the fields are public, there's no place to add rules.
// mish 73rf 2mna3 7d from setting invalid values

// B)
// fix is simple make them private and provide public methods to access and modify them with validation logic.
// like a setter and getter for balance and owner.

// C)
// if the fields are public, any code anywhere in the program can change them directly,
// which can lead to bugs and security issues fa kida ana bwzt fikt el encapsulation 2sasn.


// Question2
// A field is a variable declared inside a class 2w struct that holds data
// A property is a member that provides a flexible mechanism to read, write, or compute the value of a private field.
// fa hia shbh el field bs btdi control 3la el access w el validation logic.
// Readonly example:
private double _price;
private double _taxRate = 0.14;

public double PriceAfterTax
{
    get { return _price + (_price * _taxRate); }
}

// Question3
// A) 
// Indexer bt5lina nst5dim square bracket notation on an object zy el array.

// B)
// IndexOutOfRangeException and crash the program 3lshan el c# by default does not handle out-of-bounds access gracefully zy C++, Rust.
// 3lshan 25liha safer add a bounds check in the indexer
public class MyCollection
{
    private int[] data = new int[10];

    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= data.Length)
                throw new IndexOutOfRangeException("Index out of range");
            return data[index];
        }
        set
        {
            if (index < 0 || index >= data.Length)
                throw new IndexOutOfRangeException("Index out of range");
            data[index] = value;
        }
    }
}
// C)
// class can have multiple indexers as long as they have different parameter types
// dih 2smha overloading zy el C++ w Java, fa momken a3ml indexer b int w indexer b string w kda.

// Question4
// A) Static members belong to the class itself fah lw 8irt fiha 7tsma3 fi kol el objects.
// fah TotalOrder shared by all of the objuect of the same class, bs Item is specific to each object.

// B) A static method doesn't belong to any specific object so it access only static members,
// fah mish 7a access non-static members (fields or methods) of the class because they belong to specific instances of the class. 







