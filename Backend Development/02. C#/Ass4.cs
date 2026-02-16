using System;
using System.Diagnostics;
using System.Text;

namespace AssignmentSolutions
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Question 01
/*
a)  The problem with string concatenation:
    Strings in C# can't be changed once created — they're immutable.
    So every time we write productList += something, C# doesn't
    just add to the end. It creates a completely new string,
    copies everything from the old one, then adds the new part.
    Do that 5000 times and you've wasted a huge amount of memory
*/
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string result = "";
            for (int i = 1; i <= 5000; i++)
            {
                result += "PROD-" + i + ",";
            }

            sw.Stop();
            long stringTime = sw.ElapsedMilliseconds;

            sw.Restart();

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 5000; i++)
            {
                sb.Append("PROD-").Append(i).Append(",");
            }
            string efficientResult = sb.ToString();

            sw.Stop();
            long sbTime = sw.ElapsedMilliseconds;

            Console.WriteLine("b) StringBuilder keeps one buffer and just adds to it — no copying.");
            Console.WriteLine("   Much more efficient for repeated concatenations.\n");

            Console.WriteLine("c) Timing results:");
            Console.WriteLine($"   Regular string += : {stringTime} ms");
            Console.WriteLine($"   StringBuilder     : {sbTime} ms");
            if (sbTime < stringTime)
                Console.WriteLine($"   StringBuilder was faster by {stringTime - sbTime} ms — that's a big deal at scale.\n");
            else
                Console.WriteLine("   Both were very fast at this scale, but the difference grows significantly with more data.\n");
            #endregion

            #region Question 02

            Console.Write("How old are you? ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("What day is it? (1=Sun, 2=Mon, 3=Tue, 4=Wed, 5=Thu, 6=Fri, 7=Sat): ");
            int day = int.Parse(Console.ReadLine());

            Console.Write("Do you have a student ID? (yes/no): ");
            bool hasStudentId = Console.ReadLine().Trim().ToLower() == "yes";

            double basePrice;
            string priceReason;

            if (age < 5)
            {
                basePrice = 0;
                priceReason = "Free — kids under 5 get in for free!";
            }
            else if (age <= 12)
            {
                basePrice = 30;
                priceReason = "30 LE — child ticket (ages 5–12)";
            }
            else if (age <= 59)
            {
                basePrice = 50;
                priceReason = "50 LE — standard adult ticket";
            }
            else
            {
                basePrice = 25;
                priceReason = "25 LE — senior discount (60+)";
            }

            double finalPrice = basePrice;

            Console.WriteLine("\nHere's your ticket price breakdown:");
            Console.WriteLine($"  Base price      : {priceReason}");

            if ((day == 6 || day == 7) && basePrice > 0)
            {
                finalPrice += 10;
                Console.WriteLine("  Weekend surcharge: +10 LE (Fri/Sat are busier days)");
            }

            if (hasStudentId && basePrice > 0)
            {
                double discount = finalPrice * 0.20;
                finalPrice -= discount;
                Console.WriteLine($"  Student discount : -{discount:F2} LE (20% off for students)");
            }

            Console.WriteLine($"  --------------------------------");
            Console.WriteLine($"  Total you pay   : {finalPrice:F2} LE\n");
            #endregion

            #region Question 03

            string fileExtension = ".pdf";
            string fileType;

            // Original if-else
            if (fileExtension == ".pdf")
                fileType = "PDF Document";
            else if (fileExtension == ".docx" || fileExtension == ".doc")
                fileType = "Word Document";
            else if (fileExtension == ".xlsx" || fileExtension == ".xls")
                fileType = "Excel Spreadsheet";
            else if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif")
                fileType = "Image File";
            else
                fileType = "Unknown File Type";

            Console.WriteLine($"Original if-else result  : {fileType}");

            // a) Traditional switch
            switch (fileExtension)
            {
                case ".pdf":
                    fileType = "PDF Document";
                    break;
                case ".docx":
                case ".doc":
                    fileType = "Word Document";
                    break;
                case ".xlsx":
                case ".xls":
                    fileType = "Excel Spreadsheet";
                    break;
                case ".jpg":
                case ".png":
                case ".gif":
                    fileType = "Image File";
                    break;
                default:
                    fileType = "Unknown File Type";
                    break;
            }
            Console.WriteLine($"Traditional switch result : {fileType}");

            // b) Switch expression
            string fileType2 = fileExtension switch
            {
                ".pdf"                     => "PDF Document",
                ".docx" or ".doc"          => "Word Document",
                ".xlsx" or ".xls"          => "Excel Spreadsheet",
                ".jpg" or ".png" or ".gif" => "Image File",
                _                          => "Unknown File Type"
            };
            Console.WriteLine($"Switch expression result  : {fileType2}");
            Console.WriteLine("(All three produce the same output — the switch expression is just cleaner to write)\n");
            #endregion

            #region Question 04

            int temperature = 35;
            string weatherAdvice;

            // Original if-else
            if (temperature < 0)
                weatherAdvice = "Freezing! Stay indoors.";
            else if (temperature < 15)
                weatherAdvice = "Cold. Wear a jacket.";
            else if (temperature < 25)
                weatherAdvice = "Pleasant weather.";
            else if (temperature < 35)
                weatherAdvice = "Warm. Stay hydrated.";
            else
                weatherAdvice = "Hot! Avoid sun exposure.";

            Console.WriteLine($"If-else  : {weatherAdvice}");

            // Ternary version
            string weatherTernary =
                temperature < 0  ? "Freezing! Stay indoors." :
                temperature < 15 ? "Cold. Wear a jacket."    :
                temperature < 25 ? "Pleasant weather."        :
                temperature < 35 ? "Warm. Stay hydrated."     :
                                   "Hot! Avoid sun exposure.";

            Console.WriteLine($"Ternary  : {weatherTernary}");
            Console.WriteLine("\nIs ternary more readable here?");
            Console.WriteLine("   Honestly, for two options it's great — short and clean.");
            Console.WriteLine("   But with 5 conditions chained like this, if-else is easier");
            Console.WriteLine("   to read and debug. Use ternary when it fits on one line cleanly.\n");
            #endregion

            #region Question 05
            Console.WriteLine("Let's set up your password. It must be:");
            Console.WriteLine("  - At least 8 characters");
            Console.WriteLine("  - One uppercase letter");
            Console.WriteLine("  - One number");
            Console.WriteLine("  - No spaces\n");

            int attempts = 0;
            const int maxAttempts = 5;
            bool validPassword = false;

            do
            {
                attempts++;
                Console.Write($"Enter password (attempt {attempts}/{maxAttempts}): ");
                string password = Console.ReadLine();

                bool hasMinLength = password.Length >= 8;
                bool hasUppercase = false;
                bool hasDigit = false;
                bool hasNoSpaces = !password.Contains(" ");

                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUppercase = true;
                    if (char.IsDigit(c)) hasDigit = true;
                }

                if (hasMinLength && hasUppercase && hasDigit && hasNoSpaces)
                {
                    validPassword = true;
                    Console.WriteLine("Password accepted!\n");
                }
                else
                {
                    Console.WriteLine("Not quite. Here's what needs fixing:");
                    if (!hasMinLength) Console.WriteLine("  - Too short, needs at least 8 characters");
                    if (!hasUppercase) Console.WriteLine("  - Missing an uppercase letter");
                    if (!hasDigit)     Console.WriteLine("  - Needs at least one number");
                    if (!hasNoSpaces)  Console.WriteLine("  - Remove the spaces");

                    if (attempts < maxAttempts)
                        Console.WriteLine($"  ({maxAttempts - attempts} attempt(s) left)\n");
                }

            } while (!validPassword && attempts < maxAttempts);

            if (!validPassword)
                Console.WriteLine("Account locked. Too many failed attempts.\n");
            #endregion

            #region Question 06

            int[] scores = { 85, 42, 91, 67, 55, 78, 39, 88, 72, 95, 60, 48 };
            Console.WriteLine($"Working with scores: [{string.Join(", ", scores)}]\n");

            // a) Failing scores
            Console.Write("a) Students who failed (scored below 50): ");
            bool anyFailing = false;
            foreach (int score in scores)
            {
                if (score < 50) { Console.Write(score + " "); anyFailing = true; }
            }
            if (!anyFailing) Console.Write("None — everyone passed!");
            Console.WriteLine("\n");

            // b) First score above 90
            Console.Write("b) First score above 90: ");
            bool found = false;
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > 90)
                {
                    Console.WriteLine($"{scores[i]} (found at index {i}, stopped searching there)");
                    found = true;
                    break;
                }
            }
            if (!found) Console.WriteLine("None found.");

            // c) Class average excluding absent (below 40)
            int sum = 0, count = 0;
            foreach (int score in scores)
            {
                if (score >= 40) { sum += score; count++; }
            }
            double average = count > 0 ? (double)sum / count : 0;
            Console.WriteLine($"\nc) Class average (absent students excluded): {average:F2} across {count} students");

            // d) Grade breakdown
            int aCount = 0, bCount = 0, cCount = 0, dCount = 0, fCount = 0;
            foreach (int score in scores)
            {
                if      (score >= 90) aCount++;
                else if (score >= 80) bCount++;
                else if (score >= 70) cCount++;
                else if (score >= 60) dCount++;
                else                  fCount++;
            }

            Console.WriteLine("\nd) Grade breakdown:");
            Console.WriteLine($"   A (90–100) : {aCount} student(s)");
            Console.WriteLine($"   B (80–89)  : {bCount} student(s)");
            Console.WriteLine($"   C (70–79)  : {cCount} student(s)");
            Console.WriteLine($"   D (60–69)  : {dCount} student(s)");
            Console.WriteLine($"   F (< 60)   : {fCount} student(s)");
            #endregion
        }
    }
}