// Part one
using System;

namespace DayOfWeekEnum
{
    enum DayOfWeek
    {
        Saturday = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a day number (0-6): ");
            int dayNumber = int.Parse(Console.ReadLine());

            DayOfWeek day = (DayOfWeek)dayNumber;
            Console.WriteLine($"Day: {day}");

            switch (day)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    Console.WriteLine("It's the Weekend");
                    break;
                default:
                    Console.WriteLine("It's a Workday");
                    break;
            }
        }
    }
}

// Part two
// Question 1
using System;

namespace ArrayStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter array size: ");
            int size = int.Parse(Console.ReadLine());
            
            int[] numbers = new int[size];

            // Read elements
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Enter element [{i}]: ");
                numbers[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine();

            // Calculate sum
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += numbers[i];
            }
            Console.WriteLine($"Sum     = {sum}");

            // Calculate average
            double average = (double)sum / size;
            Console.WriteLine($"Average = {average}");

            // Find maximum
            int max = numbers[0];
            for (int i = 1; i < size; i++)
            {
                if (numbers[i] > max)
                    max = numbers[i];
            }
            Console.WriteLine($"Max     = {max}");

            // Find minimum
            int min = numbers[0];
            for (int i = 1; i < size; i++)
            {
                if (numbers[i] < min)
                    min = numbers[i];
            }
            Console.WriteLine($"Min     = {min}");

            // Print reverse
            Console.Write("Reverse = ");
            for (int i = size - 1; i >= 0; i--)
            {
                Console.Write(numbers[i]);
                if (i > 0) Console.Write(", ");
            }
            Console.WriteLine();
        }
    }
}

// Question 2
using System;

namespace StudentGradesMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] grades = new int[3, 4];
            double classTotal = 0;
            int totalGrades = 0;

            // Read grades from user
            for (int student = 0; student < 3; student++)
            {
                Console.WriteLine($"\nEnter grades for Student {student + 1}:");
                for (int subject = 0; subject < 4; subject++)
                {
                    Console.Write($"Subject {subject + 1}: ");
                    grades[student, subject] = int.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("\n--- Student Averages ---");

            // Calculate and print each student's average
            for (int student = 0; student < 3; student++)
            {
                double studentTotal = 0;
                for (int subject = 0; subject < 4; subject++)
                {
                    studentTotal += grades[student, subject];
                    classTotal += grades[student, subject];
                    totalGrades++;
                }
                double studentAverage = studentTotal / 4;
                Console.WriteLine($"Student {student + 1} Average: {studentAverage:F2}");
            }

            // Calculate and print class average
            double classAverage = classTotal / totalGrades;
            Console.WriteLine($"\nOverall Class Average: {classAverage:F2}");
        }
    }
}

// Part three
// Question 1
using System;

namespace BasicCalculator
{
    class Program
    {
        static double Add(double a, double b) => a + b;
        static double Subtract(double a, double b) => a - b;
        static double Multiply(double a, double b) => a * b;
        
        static double Divide(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("Error: Cannot divide by zero!");
                return double.NaN;
            }
            return a / b;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter first number: ");
            double num1 = double.Parse(Console.ReadLine());

            Console.Write("Enter second number: ");
            double num2 = double.Parse(Console.ReadLine());

            Console.Write("Enter operation (+, -, *, /): ");
            char operation = Console.ReadLine()[0];

            double result = 0;
            bool validOperation = true;

            switch (operation)
            {
                case '+':
                    result = Add(num1, num2);
                    break;
                case '-':
                    result = Subtract(num1, num2);
                    break;
                case '*':
                    result = Multiply(num1, num2);
                    break;
                case '/':
                    result = Divide(num1, num2);
                    break;
                default:
                    Console.WriteLine("Invalid operation!");
                    validOperation = false;
                    break;
            }

            if (validOperation && !double.IsNaN(result))
            {
                Console.WriteLine($"Result: {result}");
            }
        }
    }
}

// Question 2
using System;

namespace CircleCalculator
{
    class Program
    {
        static void CalculateCircle(double radius, out double area, out double circumference)
        {
            area = Math.PI * radius * radius;
            circumference = 2 * Math.PI * radius;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter circle radius: ");
            double radius = double.Parse(Console.ReadLine());

            double area, circumference;
            CalculateCircle(radius, out area, out circumference);

            Console.WriteLine($"Area: {area:F2}");
            Console.WriteLine($"Circumference: {circumference:F2}");
        }
    }
}

// Part four
using System;

namespace StudentGradeManager
{
    // Enum for grades
    enum Grade
    {
        A, B, C, D, F
    }

    class Program
    {
        // Method to get grade enum based on score
        static Grade GetGrade(int score)
        {
            if (score >= 90) return Grade.A;
            if (score >= 80) return Grade.B;
            if (score >= 70) return Grade.C;
            if (score >= 60) return Grade.D;
            return Grade.F;
        }

        // Method to calculate average of all scores
        static double CalculateAverage(int[] scores)
        {
            int sum = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                sum += scores[i];
            }
            return (double)sum / scores.Length;
        }

        // Method to find min and max scores using out parameters
        static void GetMinMax(int[] scores, out int min, out int max)
        {
            min = scores[0];
            max = scores[0];
            
            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i] < min) min = scores[i];
                if (scores[i] > max) max = scores[i];
            }
        }

        static void Main(string[] args)
        {
            int[] scores = new int[5];

            // Read 5 student scores
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Enter score for Student {i + 1}: ");
                scores[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("\n--- Report ---");

            // Print each student's score and grade
            for (int i = 0; i < 5; i++)
            {
                Grade grade = GetGrade(scores[i]);
                Console.WriteLine($"Student {i + 1}: {scores[i]} -> Grade: {grade}");
            }

            // Calculate and print statistics
            double average = CalculateAverage(scores);
            GetMinMax(scores, out int min, out int max);

            Console.WriteLine($"\nAverage: {average:F1}");
            Console.WriteLine($"Highest Score: {max}");
            Console.WriteLine($"Lowest Score:  {min}");
        }
    }
}