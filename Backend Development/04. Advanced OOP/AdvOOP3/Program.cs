// Question 1
// using System;
// using System.Collections.Generic;
// using System.Linq;

// class StudentGradeManager
// {
//     static void Main()
//     {
//         // 1. Create the list of grades
//         List<int> grades = new List<int> { 85, 92, 78, 95, 88, 70, 100, 65 };

//         // 2. Print collection info
//         Console.WriteLine("Grades: " + string.Join(", ", grades));
//         Console.WriteLine("Count: " + grades.Count);
//         Console.WriteLine("First: " + grades.First());
//         Console.WriteLine("Last: " + grades.Last());

//         // 3. Sort ascending + print
//         grades.Sort();
//         Console.WriteLine("\nSorted grades: " + string.Join(", ", grades));

//         // 4. First grade above 90
//         int firstAbove90 = grades.First(g => g > 90);
//         Console.WriteLine("\nFirst grade above 90: " + firstAbove90);

//         // 5. All failing grades
//         List<int> failing = grades.Where(g => g < 75).ToList();
//         Console.WriteLine("Failing grades: " + string.Join(", ", failing));

//         // 6. Remove failing grades
//         grades.RemoveAll(g => g < 75);
//         Console.WriteLine("After removing failing: " + string.Join(", ", grades));

//         // 7. Check if any grade equals 100
//         bool hasPerfect = grades.Any(g => g == 100);
//         Console.WriteLine("Has a 100? " + hasPerfect);

//         // 8. Convert each grade to "Grade: X"
//         List<string> gradeLabels = grades.Select(g => $"Grade: {g}").ToList();
//         Console.WriteLine("Labeled grades: " + string.Join(", ", gradeLabels));
//     }
// }

// Question 2
// using System;
// using System.Collections.Generic;

// class Leaderboard
// {
//     static void Main()
//     {
//         // SortedDictionary
//         SortedDictionary<int, string> leaderboard = new SortedDictionary<int, string>();

//         // 1. Add players
//         leaderboard[500] = "Ahmed";
//         leaderboard[200] = "Sara";
//         leaderboard[800] = "Ali";
//         leaderboard[350] = "Mona";

//         // 2. Print all entries
//         Console.WriteLine("Leaderboard:");
//         foreach (var entry in leaderboard)
//             Console.WriteLine($"  Score {entry.Key} → {entry.Value}");

//         // 3. First key and first value
//         Console.WriteLine("\nFirst score: " + leaderboard.Keys.First());
//         Console.WriteLine("First player: " + leaderboard.Values.First());

//         // 4. Check if score 500 exists
//         Console.WriteLine("\nScore 500 exists? " + leaderboard.ContainsKey(500));

//         // 5. Safely get player with score 999
//         leaderboard.TryGetValue(999, out string? player999);
//         Console.WriteLine("Player at 999: " + (player999 ?? "Not found"));

//         // 6. Remove score 200 and print updated leaderboard
//         leaderboard.Remove(200);
//         Console.WriteLine("\nUpdated leaderboard:");
//         foreach (var entry in leaderboard)
//             Console.WriteLine($"  Score {entry.Key} → {entry.Value}");
//     }
// }

// Question 3
// using System;
// using System.Collections.Generic;

// class PhoneBook
// {
//     static void Main()
//     {
//         // 1. Create phone book with 4 contacts
//         Dictionary<string, string> phoneBook = new Dictionary<string, string>
//         {
//             { "Ahmed", "010-1111-2222" },
//             { "Sara",  "010-3333-4444" },
//             { "Ali",   "010-5555-6666" },
//             { "Mona",  "010-7777-8888" }
//         };

//         // 2. Add new contact using [] syntax
//         phoneBook["Yara"] = "010-9999-0000";
//         Console.WriteLine("Added Yara.");

//         // 3. Try adding a duplicate with .Add() deh 7trmi exception
//         try
//         {
//             phoneBook.Add("Ahmed", "010-0000-1111");
//         }
//         catch (ArgumentException ex)
//         {
//             Console.WriteLine("Error with .Add(): " + ex.Message);
//         }

//         // 4. Try adding a duplicate with .TryAdd()
//         bool added = phoneBook.TryAdd("Ahmed", "010-0000-1111");
//         Console.WriteLine("TryAdd for Ahmed succeeded? " + added);

//         // 5. Search for a contact that doesn't exist
//         bool found = phoneBook.ContainsKey("Khaled");
//         Console.WriteLine("Khaled found? " + found);

//         // 6. Get a contact with a fallback
//         phoneBook.TryGetValue("Khaled", out string? num);
//         Console.WriteLine("Khaled's number: " + (num ?? "Not Found"));

//         // 7. Print all keys on one line, all values on another
//         Console.WriteLine("\nNames:   " + string.Join(", ", phoneBook.Keys));
//         Console.WriteLine("Numbers: " + string.Join(", ", phoneBook.Values));
//     }
// }

//Question 4
// using System;
// using System.Collections.Generic;

// class EmailValidator
// {
//     static void Main()
//     {
//         // 1. HashSet with case-insensitive comparer
//         HashSet<string> emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

//         // 2. Add emails (some are duplicates in different cases)
//         emails.Add("ahmed@test.com");
//         emails.Add("AHMED@test.com");   // hwa hwa 2l fo2
//         emails.Add("sara@test.com");
//         emails.Add("Sara@Test.Com");    // hwa hwa 2l fo2

//         // 3. Count should be 2, 3lshan upper case is ignored 2sasn
//         Console.WriteLine("Count: " + emails.Count);

//         // 4. Two sets for set operations
//         HashSet<int> setA = new HashSet<int> { 1, 2, 3, 4, 5 };
//         HashSet<int> setB = new HashSet<int> { 4, 5, 6, 7, 8 };

//         // 5a. UnionWith all elements from both sets
//         HashSet<int> union = new HashSet<int>(setA);
//         union.UnionWith(setB);
//         Console.WriteLine("\nUnion (A U B): " + string.Join(", ", union));

//         // 5b. IntersectWith only elements in both
//         HashSet<int> intersect = new HashSet<int>(setA);
//         intersect.IntersectWith(setB);
//         Console.WriteLine("Intersect (A | B): " + string.Join(", ", intersect));

//         // 5c. ExceptWith elements in A but NOT in B
//         HashSet<int> except = new HashSet<int>(setA);
//         except.ExceptWith(setB);
//         Console.WriteLine("Except (A - B): " + string.Join(", ", except));

//         // 6. Check if {1, 2} is a subset of Set A
//         HashSet<int> small = new HashSet<int> { 1, 2 };
//         Console.WriteLine("\n{1,2} is subset of A? " + small.IsSubsetOf(setA));
//     }
// }

// Question 5
// using System;
// using System.Collections.Generic;

// class PrintQueueSimulator
// {
//     static void Main()
//     {
//         // Create queue and enqueue 5 documents
//         Queue<string> printQueue = new Queue<string>();
//         printQueue.Enqueue("Report.pdf");
//         printQueue.Enqueue("Invoice.pdf");
//         printQueue.Enqueue("Letter.docx");
//         printQueue.Enqueue("Resume.pdf");
//         printQueue.Enqueue("Photo.jpg");

//         // 1. Print contents and count
//         Console.WriteLine("Print Queue: " + string.Join(", ", printQueue));
//         Console.WriteLine("Count: " + printQueue.Count);

//         // 2. Peek see what's next without removing
//         Console.WriteLine("\nNext to print (Peek): " + printQueue.Peek());

//         // 3. Dequeue and print each document
//         Console.WriteLine("\nProcessing queue:");
//         while (printQueue.Count > 0)
//         {
//             string doc = printQueue.Dequeue();
//             Console.WriteLine("Printing: " + doc);
//         }

//         // 4. TryDequeue on empty queue
//         bool success = printQueue.TryDequeue(out string? result);
//         Console.WriteLine("\nTryDequeue on empty queue:");
//         Console.WriteLine("  Succeeded? " + success);
//         Console.WriteLine("  Value: " + (result ?? "null"));
//     }
// }

// Question 6
using System;
using System.Collections.Generic;

class Exercise6_BrowserHistory
{
    static void Main()
    {
        // Create stack for browser history
        Stack<string> history = new Stack<string>();

        // 1. Push 5 URLs
        history.Push("google.com");
        history.Push("github.com");
        history.Push("stackoverflow.com");
        history.Push("youtube.com");
        history.Push("claude.ai");

        // 2. Peek current page (top of stack)
        Console.WriteLine("Current page (Peek): " + history.Peek());

        // 3. Go back 3 times using Pop
        Console.WriteLine("\nGoing back 3 pages:");
        for (int i = 0; i < 3; i++)
        {
            string leaving = history.Pop();
            Console.WriteLine("Leaving: " + leaving);
        }

        // 4. Print current page after going back
        Console.WriteLine("\nCurrent page now: " + history.Peek());

        // 5. Keep popping until empty then TryPop
        history.Pop(); // 7yshel "github.com"
        history.Pop(); // 7yshel "google.com"

        bool success = history.TryPop(out string? page);
        Console.WriteLine("\nTryPop on empty stack:");
        Console.WriteLine("  Succeeded? " + success);
        Console.WriteLine("  Value: " + (page ?? "null"));
    }
}