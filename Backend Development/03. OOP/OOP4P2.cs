using System;

namespace MovieTicketBookingSystem
{
    class Ticket
    {
        private static int totalTickets = 0;

        private string movieName;
        private decimal price;

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

        // switched to decimal, double does weird stuff with money
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value > 0)
                    price = value;
                // else ignore, cinema needs to make money wla eh
            }
        }

        // 14% tax as usual
        public decimal PriceAfterTax
        {
            get { return price * 1.14m; }
        }

        public Ticket(string movieName, decimal price)
        {
            totalTickets++;
            TicketId = totalTickets;

            MovieName = movieName;
            Price     = price;
        }

        public static int GetTotalTickets()
        {
            return totalTickets;
        }

        // two versions - one sets price directly, one calculates it from base * multiplier
        public void SetPrice(decimal newPrice)
        {
            Price = newPrice;
        }

        public void SetPrice(decimal basePrice, decimal multiplier)
        {
            Price = basePrice * multiplier;
        }

        // child classes override this
        public override string ToString()
        {
            return $"[#{TicketId}] {MovieName} | Price: {Price:F2} EGP | After Tax: {PriceAfterTax:F2} EGP";
        }

        public virtual void PrintTicket()
        {
            Console.WriteLine("\n+--------------------------------------+");
            Console.WriteLine("|           TICKET STUB               |");
            Console.WriteLine("+--------------------------------------+");
            Console.WriteLine($"| ID:    #{TicketId,-4}                         |");
            Console.WriteLine($"| Movie: {MovieName,-25}   |");
            Console.WriteLine($"| Price: {Price,6:F2} EGP  (tax inc: {PriceAfterTax,6:F2}) |");
            Console.WriteLine("+--------------------------------------+");
        }
    }

    // standard ticket, just adds a seat number
    class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }

        public StandardTicket(string movieName, decimal price, string seatNumber)
            : base(movieName, price)
        {
            SeatNumber = seatNumber;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Seat: {SeatNumber} | Type: Standard";
        }

        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($"| Seat:  {SeatNumber,-5}                          |");
            Console.WriteLine($"| Type:  Standard                      |");
            Console.WriteLine("+--------------------------------------+");
            Console.WriteLine("|    Thank you for your purchase!     |");
            Console.WriteLine("+--------------------------------------+");
        }
    }

    // vip ticket, lounge access + 50 EGP service fee always
    class VIPTicket : Ticket
    {
        public bool    LoungeAccess { get; set; }
        public decimal ServiceFee   { get; private set; } = 50;

        public VIPTicket(string movieName, decimal price, bool loungeAccess)
            : base(movieName, price)
        {
            LoungeAccess = loungeAccess;
        }

        public override string ToString()
        {
            string lounge = LoungeAccess ? "Yes" : "No";
            return base.ToString() + $" | Type: VIP | Lounge: {lounge} | Service Fee: {ServiceFee:F2} EGP";
        }

        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($"| Type:  VIP                           |");
            Console.WriteLine($"| Lounge:{(LoungeAccess ? "Yes" : "No"),-5}                          |");
            Console.WriteLine($"| Fee:   {ServiceFee,6:F2} EGP                    |");
            Console.WriteLine("+--------------------------------------+");
            Console.WriteLine("|    Thank you for your purchase!     |");
            Console.WriteLine("+--------------------------------------+");
        }
    }

    // imax ticket, if 3D then +30 EGP on the price
    class IMAXTicket : Ticket
    {
        private bool is3D;

        public bool Is3D
        {
            get { return is3D; }
            set
            {
                if (value && !is3D)
                    Price += 30;
                else if (!value && is3D)
                    Price -= 30;
                is3D = value;
            }
        }

        public IMAXTicket(string movieName, decimal price, bool is3D)
            : base(movieName, price)
        {
            Is3D = is3D; 
        }

        public override string ToString()
        {
            string format = Is3D ? "IMAX 3D" : "IMAX";
            return base.ToString() + $" | Type: {format}";
        }

        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine($"| Type:  {(Is3D ? "IMAX 3D" : "IMAX"),-6}                         |");
            Console.WriteLine("+--------------------------------------+");
            Console.WriteLine("|    Thank you for your purchase!     |");
            Console.WriteLine("+--------------------------------------+");
        }
    }

    // projector class, cinema creates it so it dies with it (composition)
    class Projector
    {
        public bool IsRunning { get; private set; } = false;

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Console.WriteLine("  Projector is on, we're good to go");
            }
            else
            {
                Console.WriteLine("  Already running yaba");
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                Console.WriteLine("  Projector off. that's a wrap");
            }
            else
            {
                Console.WriteLine("  Wasn't even on lol");
            }
        }
    }

    class Cinema
    {
        public string CinemaName { get; private set; }

        private Projector projector = new Projector(); // created inside, dies with cinema
        private Ticket[] tickets    = new Ticket[20];

        public Cinema(string name)
        {
            CinemaName = name;
        }

        public void OpenCinema()
        {
            Console.WriteLine($"\n[ {CinemaName} opening... ]");
            projector.Start();
        }

        public void CloseCinema()
        {
            Console.WriteLine($"\n[ {CinemaName} closing... ]");
            projector.Stop();
        }

        public bool AddTicket(Ticket t)
        {
            if (t == null)
            {
                Console.WriteLine("Can't add null ticket, enta bt3mil eh");
                return false;
            }

            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] == null)
                {
                    tickets[i] = t;
                    Console.WriteLine($"  Ticket added at slot {i}");
                    return true;
                }
            }

            Console.WriteLine("Cinema is full! (max 20 tickets)");
            return false;
        }

        public void PrintAllTickets()
        {
            Console.WriteLine($"\n=== TICKETS AT {CinemaName.ToUpper()} ===");
            bool any = false;

            foreach (Ticket t in tickets)
            {
                if (t != null)
                {
                    t.PrintTicket();
                    any = true;
                }
            }

            if (!any)
                Console.WriteLine("  No tickets yet.");
        }
    }

    static class BookingHelper
    {
        private static int refCounter = 0;

        // takes any ticket and calls its own PrintTicket (polymorphism)
        public static void ProcessTicket(Ticket t)
        {
            if (t == null)
            {
                Console.WriteLine("no ticket to process lol");
                return;
            }
            Console.WriteLine("\n--- Processing ticket ---");
            t.PrintTicket();
        }

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
            Console.WriteLine("║    CINEMA BOOKING SYSTEM v2.0     ║");
            Console.WriteLine("╚════════════════════════════════════╝");

            // open the cinema
            Cinema cinema = new Cinema("CineMax Alexandria");
            cinema.OpenCinema();

            // add one of each type
            Console.WriteLine("\n--- Adding tickets ---");

            StandardTicket std = new StandardTicket("Dune 2", 80m, "B7");
            VIPTicket vip      = new VIPTicket("Poor Things", 150m, loungeAccess: true);
            IMAXTicket imax    = new IMAXTicket("Oppenheimer", 120m, is3D: true); // price becomes 150 because of 3D 3lshan 7rmya

            // testing both SetPrice versions on std before adding it
            Console.WriteLine("\n--- Testing SetPrice ---");
            std.SetPrice(90m);                  // direct set
            Console.WriteLine($"  After SetPrice(90): {std.Price:F2} EGP");
            std.SetPrice(100m, 0.85m);          // base * multiplier = 85 EGP
            Console.WriteLine($"  After SetPrice(100, 0.85): {std.Price:F2} EGP");

            cinema.AddTicket(std);
            cinema.AddTicket(vip);
            cinema.AddTicket(imax);

            // print everything
            cinema.PrintAllTickets();

            Console.WriteLine($"\nTotal tickets created: {Ticket.GetTotalTickets()}");

            // ProcessTicket - polymorphism, passes vip but works with any Ticket
            BookingHelper.ProcessTicket(vip);

            // close up
            cinema.CloseCinema();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║        Thanks for visiting!       ║");
            Console.WriteLine("║       (don't forget your snacks)  ║");
            Console.WriteLine("╚════════════════════════════════════╝");
        }
    }
}