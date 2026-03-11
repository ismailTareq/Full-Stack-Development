// Question 1
// interface hya t2olak class el folany y2dar ya3mil eh bs mish implemented ezay
// As if it's a signature method
// used because what if i want to upgrade a class that my code depend on it i can't modify it that's it
// but by using interface i can take with the manger direct not the secertory
// three benifts: Easier testing, Multiple interfaces on one class since C# prevents multiple inheritance, Loose coupling depend on the interface not the actual class

// Question 2
// A) 2l 2tnin 3ndohm the same method so i can't tell them a part so both of them end up being shared
// B)
void IEnglishSpeaker.Greet()
{
    Console.WriteLine("Hello");
}

void IArabicSpeaker.Greet()
{
    Console.WriteLine("Ahlan");
}
// b7adid ana 3aiz greet mn 2nhi interface called explicit interface implemntation
// C) I don't think so 3lshan el compiler my3rfsh anhi wa7da 2nta to2sod biha 3aiz anhi mn el 25ir
// bs momkn lw 3amlt explicit casting fa kida b2olo nadi deh ka IEnglishSpeaker or IArabicSpeaker
Translator translator = new Translator();
((IEnglishSpeaker)translator).Greet();  // prints Hello
((IArabicSpeaker)translator).Greet();   // prints Ahlan

// Question 3
// Shallow → howa mn 25ir 3mlt copy mn object, array, list you copy a pointer to the object fa 2i t3dil 3lih 7ysma3 fl tani
// usage: with value-type fields
// Deep → same thing bs dah bya5od copy y5zinha 3ndo fi address mo5talif fa lw 3dilt el object el original mish 7ysma3 fl tani 5las
// usage: with objects, array, lists

// Question 4
// Dev - Testing
// QA - Testing

// ShallowCopy copies references for reference-type fields.
// e1 and e2 share the same Department object.
// Changing e2.Dept.Name modifies the shared object → affects e1.Dept.Name.
// Title is independent because strings behave like values here (immutable, assignment changes reference locally)



