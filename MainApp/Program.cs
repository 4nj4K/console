using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContactListApp
{
    
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Contact()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Id}\n{FirstName} {LastName}\nEmail: {Email}\nPhone: {PhoneNumber}\nAddress: {StreetAddress}, {PostalCode} {City}\n";
        }
    }

    
    public class ContactManager
    {
        private readonly List<Contact> _contacts;

        public ContactManager()
        {
            _contacts = new List<Contact>();
        }

        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _contacts;
        }

        // Kod genererad från  chatgpt  för att spara kontakter i json fil
        public void LoadContacts(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var contacts = JsonSerializer.Deserialize<List<Contact>>(json);
                if (contacts != null)
                {
                    _contacts.Clear();
                    _contacts.AddRange(contacts);
                }
            }
        }

        public void SaveContacts(string filePath)
        {
            string json = JsonSerializer.Serialize(_contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }

    
    class Program
    {
        private const string FilePath = "contacts.json";
        private static ContactManager _contactManager;

        static void Main(string[] args)
        {
            _contactManager = new ContactManager();
            _contactManager.LoadContacts(FilePath);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Kontaktlista");
                Console.WriteLine("1. Lista alla kontakter");
                Console.WriteLine("2. Lägg till en kontakt");
                Console.WriteLine("3. Avsluta");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ListContacts();
                        break;
                    case "2":
                        AddContact();
                        break;
                    case "3":
                        _contactManager.SaveContacts(FilePath);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Tryck på valfri tangent för att fortsätta.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ListContacts()
        {
            Console.Clear();
            var contacts = _contactManager.GetAllContacts();
            if (!contacts.Any())
            {
                Console.WriteLine("Inga kontakter hittades.");
            }
            else
            {
                foreach (var contact in contacts)
                {
                    Console.WriteLine(contact);
                }
            }
            Console.WriteLine("Tryck på valfri tangent för att gå tillbaka till menyn.");
            Console.ReadKey();
        }

        static void AddContact()
        {
            Console.Clear();
            Console.WriteLine("Lägg till en ny kontakt:");

            var contact = new Contact();

            Console.Write("Förnamn: ");
            contact.FirstName = Console.ReadLine();

            Console.Write("Efternamn: ");
            contact.LastName = Console.ReadLine();

            Console.Write("E-postadress: ");
            contact.Email = Console.ReadLine();

            Console.Write("Telefonnummer: ");
            contact.PhoneNumber = Console.ReadLine();

            Console.Write("Gatuadress: ");
            contact.StreetAddress = Console.ReadLine();

            Console.Write("Postnummer: ");
            contact.PostalCode = Console.ReadLine();

            Console.Write("Ort: ");
            contact.City = Console.ReadLine();

            _contactManager.AddContact(contact);

            Console.WriteLine("Kontakt tillagd! Tryck på valfri tangent för att gå tillbaka till menyn.");
            Console.ReadKey();
        }
    }
}





















