using System;

namespace MovieTicketBookingSystem
{
    // different ticket types
    enum TicketType
    {
        Standard,
        VIP,
        IMAX
    }

    // just holds row and seat together
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

    class Ticket
    {
        private static int totalTickets = 0;  // counts all tickets ever created

        private string movieName;
        private double price;

        public int TicketId { get; private set; }

        // don't want empty movie names
        public string MovieName
        {
            get { return movieName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    movieName = value;
            }
        }

        public TicketType Type { get; set; }
        public SeatLocation Seat { get; set; }

        // price validation cinema needs to make money wla eh
        public double Price
        {
            get { return price; }
            set
            {
                if (value > 0)
                    price = value;
                // else ignore, probably should log this but nah
            }
        }

        // tax is 14% here, could make this configurable but too lazy
        public double PriceAfterTax
        {
            get { return price * 1.14; }
        }

        public Ticket(string movieName, TicketType type, SeatLocation seat, double price)
        {
            totalTickets++;
            TicketId = totalTickets;  // starts at 1

            MovieName = movieName;
            Type      = type;
            Seat      = seat;
            Price     = price;
        }

        public static int GetTotalTicketsSold()
        {
            return totalTickets;
        }

        public void PrintTicket()
        {
            // tried to make this look like a real ticket stub as pictured
            Console.WriteLine("\n+--------------------------------------+");
            Console.WriteLine("|           TICKET STUB               |");
            Console.WriteLine("+--------------------------------------+");
            Console.WriteLine($"| ID:    #{TicketId,-4}                         |");
            Console.WriteLine($"| Movie: {MovieName,-25}   |");
            Console.WriteLine($"| Type:  {Type,-6}                         |");
            Console.WriteLine($"| Seat:  {Seat,-5}                          |");
            Console.WriteLine($"| Price: {Price,6:F2} EGP  (tax inc: {PriceAfterTax,6:F2}) |");
            Console.WriteLine("+--------------------------------------+");
            Console.WriteLine("|    Thank you for your purchase!     |");
            Console.WriteLine("+--------------------------------------+");
        }
    }

    class Cinema
    {
        private Ticket[] tickets = new Ticket[20];
        private int ticketCount = 0;  // keep track of how many we actually have

        // indexer so we can do cinema[0] w kda
        public Ticket this[int index]
        {
            get
            {
                if (index < 0 || index >= tickets.Length)
                {
                    Console.WriteLine($"Warning: index {index} out of range");
                    return null;
                }
                return tickets[index];
            }
            set
            {
                if (index >= 0 && index < tickets.Length)
                {
                    tickets[index] = value;
                    if (value != null && index >= ticketCount)
                        ticketCount = index + 1;
                }
                else
                {
                    Console.WriteLine($"Can't set ticket at index {index}");
                }
            }
        }

        public Ticket GetMovieByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            foreach (Ticket t in tickets)
            {
                if (t != null && t.MovieName.ToLower().Contains(name.ToLower()))
                    return t;
            }
            return null;
        }

        // add to first empty slot
        public bool AddTicket(Ticket t)
        {
            if (t == null)
            {
                Console.WriteLine("Can't add null ticket");
                return false;
            }

            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] == null)
                {
                    tickets[i] = t;
                    if (i >= ticketCount)
                        ticketCount = i + 1;
                    Console.WriteLine($"Ticket added at position {i}");
                    return true;
                }
            }
            Console.WriteLine("Sorry, cinema is full! (max 20 tickets)");
            return false;
        }

        // show all tickets that aren't null
        public void ShowAllTickets()
        {
            Console.WriteLine("\n=== CURRENT BOOKINGS ===");
            bool any = false;
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] != null)
                {
                    Console.WriteLine($"[{i}] Ticket #{tickets[i].TicketId}: {tickets[i].MovieName} - {tickets[i].Seat}");
                    any = true;
                }
            }
            if (!any)
                Console.WriteLine("No tickets booked yet.");
        }
    }

    static class BookingHelper
    {
        private static int refCounter = 0;  // starts at 0 so first ref is BK-1

        public static double CalcGroupDiscount(int numberOfTickets, double pricePerTicket)
        {
            double total = numberOfTickets * pricePerTicket;

            if (numberOfTickets >= 5)
            {
                Console.WriteLine("  Group discount: 10% off");
                return total * 0.90;
            }

            return total;
        }

        // BK-1, BK-2, BK-3 format as required
        public static string GenerateBookingReference()
        {
            refCounter++;
            return $"BK-{refCounter}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║    CINEMA BOOKING SYSTEM          ║");
            Console.WriteLine("╚════════════════════════════════════╝");

            Cinema cinema = new Cinema();

            // all 3 tickets are user input now
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"\n--- Ticket {i} of 3 ---");

                string name;
                do
                {
                    Console.Write("Movie name: ");
                    name = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(name))
                        Console.WriteLine("  C'mon, enter something! don't be like that");
                }
                while (string.IsNullOrWhiteSpace(name));

                // menu
                Console.WriteLine("\nTicket types:");
                Console.WriteLine("  1. Standard (80 EGP)");
                Console.WriteLine("  2. VIP (150 EGP)");
                Console.WriteLine("  3. IMAX (120 EGP)");
                Console.WriteLine("  we 2rab 2rab 2raaaaaaaaaab");
                
                int typeChoice;
                do
                {
                    Console.Write("Pick type (1-3): ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out typeChoice) && typeChoice >= 1 && typeChoice <= 3)
                        break;
                    Console.WriteLine("  Just 1, 2, or 3 please");
                }
                while (true);

                TicketType selectedType = typeChoice switch
                {
                    1 => TicketType.Standard,
                    2 => TicketType.VIP,
                    3 => TicketType.IMAX,
                    _ => TicketType.Standard
                };

                // seat row
                char row;
                do
                {
                    Console.Write("Row (A-J only, we only have 10 rows): ");
                    string input = Console.ReadLine().ToUpper().Trim();
                    if (input.Length == 1 && input[0] >= 'A' && input[0] <= 'J')
                    {
                        row = input[0];
                        break;
                    }
                    Console.WriteLine("  Rows A through J only");
                }
                while (true);

                // seat number
                int seatNum;
                do
                {
                    Console.Write("Seat number (1-15): ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out seatNum) && seatNum >= 1 && seatNum <= 15)
                        break;
                    Console.WriteLine("  Numbers 1-15 only");
                }
                while (true);

                // price with a suggested default based on type
                double suggestedPrice = selectedType switch
                {
                    TicketType.Standard => 80.0,
                    TicketType.VIP      => 150.0,
                    TicketType.IMAX     => 120.0,
                    _                   => 80.0
                };

                double basePrice;
                Console.Write($"Price (suggested {suggestedPrice} EGP): ");
                while (!double.TryParse(Console.ReadLine(), out basePrice) || basePrice <= 0)
                {
                    Console.Write("  Invalid price, try again: ");
                }

                Ticket ticket = new Ticket(name, selectedType, new SeatLocation(row, seatNum), basePrice);
                cinema.AddTicket(ticket);

                Console.WriteLine("\nHere's your ticket:");
                ticket.PrintTicket();
            }

            // show everything booked l7d dlw2ti
            cinema.ShowAllTickets();

            // access by index 0, 1, 2 as required
            Console.WriteLine("\n--- Tickets by index ---");
            for (int i = 0; i < 3; i++)
            {
                Ticket t = cinema[i];
                if (t != null)
                    Console.WriteLine($"cinema[{i}] -> #{t.TicketId} {t.MovieName} | {t.Type} | {t.Seat} | {t.Price:F2} EGP | Tax: {t.PriceAfterTax:F2} EGP");
            }

            // search by movie name
            Console.Write("\nSearch for a movie (partial name ok): ");
            string search = Console.ReadLine().Trim();
            Ticket found = cinema.GetMovieByName(search);

            if (found != null)
            {
                Console.WriteLine("  Found it!");
                found.PrintTicket();
            }
            else
            {
                Console.WriteLine($"  No movie matching \"{search}\"");
            }

            // total tickets sold
            Console.WriteLine($"\nTotal tickets sold overall: {Ticket.GetTotalTicketsSold()}");

            // 2 booking references in BK-x format
            Console.WriteLine("\nSample booking references:");
            Console.WriteLine($"  {BookingHelper.GenerateBookingReference()}");
            Console.WriteLine($"  {BookingHelper.GenerateBookingReference()}");

            // group discount - 5 tickets at 80 EGP
            Console.WriteLine("\n--- Group Booking (5 tickets x 80 EGP) ---");
            double groupPrice = BookingHelper.CalcGroupDiscount(5, 80.0);
            Console.WriteLine($"  Total: {groupPrice:F2} EGP");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║        Thanks for visiting!       ║");
            Console.WriteLine("║       (don't forget your snacks)  ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            
        }
    }
}