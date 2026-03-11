using System;

namespace MovieTicketBookingSystem
{
    // contract for anything that can print itself
    interface IPrintable
    {
        void Print();
    }

    // contract for booking and cancellation
    interface IBookable
    {
        bool IsBooked { get; }
        void Book();
        void Cancel();
    }

    class Ticket : IPrintable, IBookable, ICloneable
    {
        private static int totalTickets = 0;

        private string movieName;
        private decimal price;

        public int TicketId { get; private set; }

        // don't want empty movie names bdihi ya3ni
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

        // 14% tax as usual 7rmya wallahi
        public decimal PriceAfterTax
        {
            get { return price * 1.14m; }
        }

        // booking status, starts as not booked
        public bool IsBooked { get; private set; } = false;

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

        public void SetPrice(decimal newPrice)
        {
            Price = newPrice;
        }

        public void SetPrice(decimal basePrice, decimal multiplier)
        {
            Price = basePrice * multiplier;
        }

        // can't book twice
        public void Book()
        {
            if (IsBooked)
            {
                Console.WriteLine($"  Ticket #{TicketId} already booked, relax");
                return;
            }
            IsBooked = true;
            Console.WriteLine($"  Ticket #{TicketId} booked!");
        }

        // can't cancel what's not booked wad7a
        public void Cancel()
        {
            if (!IsBooked)
            {
                Console.WriteLine($"  Ticket #{TicketId} isn't even booked lol");
                return;
            }
            IsBooked = false;
            Console.WriteLine($"  Ticket #{TicketId} cancelled.");
        }

        // deep copy
        public virtual object Clone()
        {
            Ticket copy = (Ticket)MemberwiseClone();
            totalTickets++;
            copy.TicketId = totalTickets;
            copy.IsBooked = false;
            return copy;
        }

        // IPrintable 
        public virtual void Print()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            string status = IsBooked ? "Yes" : "No";
            return $"[Ticket #{TicketId}] {MovieName} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {status}";
        }

        public virtual void PrintTicket()
        {
            Console.WriteLine("\n+--------------------------------------+");
            Console.WriteLine("|           TICKET STUB               |");
            Console.WriteLine("+--------------------------------------+");
            Console.WriteLine($"| ID:    #{TicketId,-4}                         |");
            Console.WriteLine($"| Movie: {MovieName,-25}   |");
            Console.WriteLine($"| Price: {Price,6:F2} EGP  (tax inc: {PriceAfterTax,6:F2}) |");
            Console.WriteLine($"| Booked:{(IsBooked ? "Yes" : "No"),-5}                          |");
            Console.WriteLine("+--------------------------------------+");
        }
    }

    // standard ticket
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
            string status = IsBooked ? "Yes" : "No";
            return $"[Ticket #{TicketId}] {MovieName} | Standard | Seat: {SeatNumber} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {status}";
        }

        public override void Print()
        {
            Console.WriteLine(ToString());
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

        // deep copy 
        public override object Clone()
        {
            VIPTicket copy    = (VIPTicket)base.Clone();
            copy.LoungeAccess = this.LoungeAccess;
            return copy;
        }

        public override string ToString()
        {
            string lounge = LoungeAccess ? "Yes" : "No";
            string status = IsBooked ? "Yes" : "No";
            return $"[Ticket #{TicketId}] {MovieName} | VIP | Lounge: {lounge} | Fee: {ServiceFee} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {status}";
        }

        public override void Print()
        {
            Console.WriteLine(ToString());
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
        public bool Is3D { get; private set; }

        public IMAXTicket(string movieName, decimal price, bool is3D)
            : base(movieName, price)
        {
            Is3D = is3D;
            if (is3D)
                Price += 30; // 3D surcharge applied once at creation, that's it
        }

        public override string ToString()
        {
            string format = Is3D ? "Yes" : "No";
            string status = IsBooked ? "Yes" : "No";
            return $"[Ticket #{TicketId}] {MovieName} | IMAX | 3D: {format} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {status}";
        }

        public override void Print()
        {
            Console.WriteLine(ToString());
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

    // projector class, cinema be like i created you and i will destroy you (composition)
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

        // uses IPrintable so it doesn't care what type each ticket is
        public void PrintAllTickets()
        {
            Console.WriteLine($"\n--- All Tickets ---");
            bool any = false;

            foreach (Ticket t in tickets)
            {
                if (t != null)
                {
                    t.Print();
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

        public static void PrintAll(IPrintable[] items)
        {
            Console.WriteLine("\n--- BookingHelper.PrintAll ---");
            foreach (IPrintable item in items)
            {
                if (item != null)
                    item.Print();
            }
        }

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
            Console.WriteLine("║    CINEMA BOOKING SYSTEM v3.0     ║");
            Console.WriteLine("╚════════════════════════════════════╝");

            // open the cinema
            Cinema cinema = new Cinema("CineMax Alexandria");
            cinema.OpenCinema();

            // create one of each, book all three
            StandardTicket std = new StandardTicket("Inception", 80m, "A5");
            VIPTicket vip      = new VIPTicket("Avengers", 200m, loungeAccess: true);
            IMAXTicket imax    = new IMAXTicket("Dune", 100m, is3D: true); // becomes 130 after 3D

            std.Book();
            vip.Book();
            imax.Book();

            cinema.AddTicket(std);
            cinema.AddTicket(vip);
            cinema.AddTicket(imax);

            // print all through cinema
            cinema.PrintAllTickets();

            // clone the vip ticket, change movie name, prove they're independent
            Console.WriteLine("\n--- Clone Test ---");
            VIPTicket vipClone   = (VIPTicket)vip.Clone();
            vipClone.MovieName   = "Interstellar";
            Console.WriteLine("Original : " + vip.ToString());
            Console.WriteLine("Clone    : " + vipClone.ToString());

            // cancel std and reprint to show status changed
            Console.WriteLine("\n--- After Cancellation ---");
            std.Cancel();
            std.Print();

            // utility method - takes IPrintable array, prints all
            BookingHelper.PrintAll(new IPrintable[] { std, vip, imax });

            //close up
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