using System;

namespace MovieTicketBookingSystem
{
    // 1. ENUM — restricts ticket type to exactly these three values
    enum TicketType
    {
        Standard,
        VIP,
        IMAX
    }
    // 2. STRUCT — simple value type to hold seat info
    struct SeatLocation
    {
        public char Row;
        public int  Number;

        public SeatLocation(char row, int number)
        {
            Row    = row;
            Number = number;
        }

        public override string ToString() => $"{Row}{Number}";
    }

    // 3. TICKET CLASS
    class Ticket
    {
        public string       MovieName;
        public TicketType   Type;
        public SeatLocation Seat;

        private double price;

        public Ticket(string movieName, TicketType type, SeatLocation seat, double price)
        {
            Init(movieName, type, seat, price);
        }

        public Ticket(string movieName)
        {
            Init(movieName, TicketType.Standard, new SeatLocation('A', 1), 50);
        }

        private void Init(string movieName, TicketType type, SeatLocation seat, double initialPrice)
        {
            MovieName = movieName;
            Type      = type;
            Seat      = seat;
            price     = initialPrice;
        }

        public double CalcTotal(double taxPercent)
        {
            return price + (price * taxPercent / 100);
        }

        public void ApplyDiscount(ref double discountAmount)
        {
            if (discountAmount > 0 && discountAmount <= price)
            {
                price         -= discountAmount;
                discountAmount  = 0;   // consumed
            }
            // Invalid mt3milsh 7aga
        }

        public void PrintTicket()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("          MOVIE TICKET SUMMARY          ");
            Console.WriteLine("========================================");
            Console.WriteLine($"  Movie Name  : {MovieName}");
            Console.WriteLine($"  Ticket Type : {Type}");
            Console.WriteLine($"  Seat        : {Seat}");
            Console.WriteLine($"  Price       : ${price:F2}");
            Console.WriteLine("========================================");
        }
    }

    // 4. CONSOLE APPLICATION
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("   Movie Ticket Booking System     ");
            Console.WriteLine("====================================\n");
            string movieName;
            do
            {
                Console.Write("Enter movie name: ");
                movieName = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(movieName))
                    Console.WriteLine("  Movie name cannot be empty. Please try again.");
            }
            while (string.IsNullOrWhiteSpace(movieName));

            int typeChoice;
            Console.WriteLine("\nSelect ticket type:");
            Console.WriteLine("  0 - Standard");
            Console.WriteLine("  1 - VIP");
            Console.WriteLine("  2 - IMAX");
            do
            {
                Console.Write("Your choice (0-2): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out typeChoice) && typeChoice >= 0 && typeChoice <= 2)
                    break;
                Console.WriteLine("  Invalid choice. Please enter 0, 1, or 2.");
            }
            while (true);
            TicketType type = (TicketType)typeChoice;

            char row;
            do
            {
                Console.Write("\nEnter seat row (A-Z): ");
                string input = Console.ReadLine().ToUpper().Trim();
                if (input.Length == 1 && input[0] >= 'A' && input[0] <= 'Z')
                {
                    row = input[0];
                    break;
                }
                Console.WriteLine("  Invalid row. Please enter a single letter A through Z.");
            }
            while (true);

            int seatNumber;
            do
            {
                Console.Write("Enter seat number: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out seatNumber) && seatNumber > 0)
                    break;
                Console.WriteLine("  Invalid seat number. Please enter a positive number.");
            }
            while (true);

            SeatLocation seat = new SeatLocation(row, seatNumber);

            double basePrice;
            do
            {
                Console.Write("\nEnter base ticket price: $");
                string input = Console.ReadLine();
                if (double.TryParse(input, out basePrice) && basePrice > 0)
                    break;
                Console.WriteLine("  Invalid price. Please enter a positive number.");
            }
            while (true);

            Ticket ticket = new Ticket(movieName, type, seat, basePrice);

            double discount;
            do
            {
                Console.Write("\nEnter discount amount ($0 for none): $");
                string input = Console.ReadLine();
                if (double.TryParse(input, out discount) && discount >= 0)
                    break;
                Console.WriteLine("  Invalid discount. Please enter 0 or a positive number.");
            }
            while (true);

            ticket.ApplyDiscount(ref discount);

            if (discount > 0)
                Console.WriteLine($"  Note: ${discount:F2} discount was invalid (exceeds price or is zero) — not applied.");
            else
                Console.WriteLine("  Discount applied successfully!");

            double taxPercent;
            do
            {
                Console.Write("\nEnter tax percentage (e.g. 14): ");
                string input = Console.ReadLine();
                if (double.TryParse(input, out taxPercent) && taxPercent >= 0)
                    break;
                Console.WriteLine("  Invalid tax. Please enter 0 or a positive number.");
            }

            double total     = ticket.CalcTotal(taxPercent);
            double taxAmount = total - ticket.CalcTotal(0);   // tax portion only

            ticket.PrintTicket();  
            Console.WriteLine($"  Tax ({taxPercent}%)      : +${taxAmount:F2}");
            Console.WriteLine($"  TOTAL DUE    : ${total:F2}");
            Console.WriteLine("========================================");
            Console.WriteLine("\nThank you for booking with us!");
        }
    }
}