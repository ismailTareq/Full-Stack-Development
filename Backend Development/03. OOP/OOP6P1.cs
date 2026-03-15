// Question 1
// Abstractions: showing what something does not how it does it mn 25ir ya3ni, hiding the implementaion 
// Encapsulation: deh b2a shbha bs mish hya, it's keeping fields private and controlling the access properties
// ATM 3ndha el internal componants b2a we 7gat ktir ana mish 7shofha dah Encapsulation
// bs 2l wagha wel zrayr we kida dah abstractions 

// Question 2
// Abstract class
// * Can have both abstract methods (no body, must be overridden)
// regular methods with a full working body can have implementation. Child classes inherit the implemented ones for free.

// * Can have fields

// * Single inheritance

// * Members can be public, protected, private 

// Interface 
// * only method signatures by default, no body Can have implementation 
 
// * No instance fields, No constructors

// * A class can implement as many as it want.

// * Members are public by default, you can't put private 

// Use an abstract class when child classes share real behavior
// and use an interface when you just want to enforce a contract across unrelated classes

// Question 3
// A) No i guess, Appliance hya abstract fa compiler mish 7ysma7 t3mil mnha instances
// B) 
// PowerConsumption :every appliance uses a different amount of power (abstract)
// Status: El designer 5la fi default 3lshan tshta8l m3 most of Appliance (virtual)
// Label: the formula is the same for every appliance (concrete)

// C) It returns Standby it falls back to the parent virual implementation

// Question4 
// A) hya class ya7d hwa hwa fi kza file pa7ot partial keyword wl complier bygm3hm lma ygy ya3mil build
// why split calculator mish 3arf 2l sra7a

// B) El decleration and the definition are in different files like .h, .c/.c++ 7aga kida lw implementation 
// 2tshal 3adi bs mish 7st5dimha no error 

// C) hya method that add new methods to existing type mn 8ir inheritence i think still need to revise on this part
// #1 : Must be inside a static class
// #2 : The method itself must be static
// #3 : The first parameter must use the this keyword followed by the type you're extending

// D)
// Log: result = 20
// $20.00
// 20 -> stored in Result -> OnCalculated(20) -> ToCurrency() -> string with 2 decimals




