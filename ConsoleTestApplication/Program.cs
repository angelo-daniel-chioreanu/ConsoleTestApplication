// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Text.Json;
using ConsoleTestApplication.Models;
using ConsoleTestApplication.Services;

Console.WriteLine("Console Test Web API");
Console.WriteLine();

using HttpClient client = new();

client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "Test Web API");

await ProcessRepositoriesAsync(client);

Console.WriteLine();
Console.WriteLine("Program End.");
Console.WriteLine();

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    string separator = new string('-', 110);

    Contact? contact = null;
    IEnumerable<Contact>? contacts = null;

    Console.WriteLine(separator);

    Console.WriteLine("Get Contacts");
    Console.WriteLine();
    contacts = await Service.GetContactsAsync(client);
    PrintContacts(contacts);
    Console.WriteLine(separator);

    Console.WriteLine($"Get Contact Id: {1}");
    Console.WriteLine();
    contact = await Service.GetContactAsync(client, 1);
    PrintContact(contact);
    Console.WriteLine(separator);

    if (contact != null)
    {
        contact.Name += " [M]";
        // if you need to update the Emails collection of a contact then you can choose from the below solutions:
        // 1. get the contact in a reference with its owned Emails, delete the contact in the database (it deletes its related Emails in cascade), modify the Emails collection of the contact object, add the contact object to the database with its new Emails
        // 2. change the Email model to have a ContactId property and implement a Web API for Emails with CRUD operations on Emails table (best solution)
        // 3. implement a workaround for Entity Framework limitations with manual changes of EntityState to its child entities (not worth)
        Console.WriteLine($"Update Contact: {JsonSerializer.Serialize(contact)}");
        Console.WriteLine();
        await Service.UpdateContactAsync(client, contact);
        Console.WriteLine(separator);
    }

    contact = new Contact
    {
        Name = "George Parker",
        BirthDate = new DateOnly(1973, 3, 3),
        Emails = new List<Email> {
                        new Email
                        {
                            IsPrimary = true,
                            Address = "george.parker.e1@gmail.com"
                        },
                        new Email
                        {
                            IsPrimary = false,
                            Address = "george.parker.e2@gmail.com"
                        }
                    }
    };
    Console.WriteLine($"Add Contact: {JsonSerializer.Serialize(contact)}");
    Console.WriteLine();
    await Service.AddContactAsync(client, contact);
    Console.WriteLine(separator);

    contact = new Contact
    {
        Name = "Jack Box",
        BirthDate = new DateOnly(1974, 4, 4),
        Emails = new List<Email> {
                        new Email
                        {
                            IsPrimary = true,
                            Address = "jack.box.e1@gmail.com"
                        },
                        new Email
                        {
                            IsPrimary = true,
                            Address = "jack.box.e2@gmail.com"
                        }
                    }
    };
    Console.WriteLine($"Add Contact: {JsonSerializer.Serialize(contact)}");
    Console.WriteLine();
    await Service.AddContactAsync(client, contact);
    Console.WriteLine(separator);

    Console.WriteLine("Get Contacts By Date");
    Console.WriteLine();
    contacts = await Service.GetContactsByDateAsync(client, "1972-02-02", "1973-03-03");
    PrintContacts(contacts);
    Console.WriteLine(separator);

    Console.WriteLine("Get Contacts By Name");
    Console.WriteLine();
    contacts = await Service.GetContactsByNameAsync(client, "George Parker");
    PrintContacts(contacts);
    Console.WriteLine(separator);

    foreach(var c in contacts ?? Enumerable.Empty<Contact>())
    {
        Console.WriteLine($"Delete Contact Id: {c.Id}");
        Console.WriteLine();
        await Service.DeleteContactAsync(client, c.Id);
        Console.WriteLine(separator);

    }

    Console.WriteLine("Get Contacts");
    Console.WriteLine();
    contacts = await Service.GetContactsAsync(client);
    PrintContacts(contacts);
    Console.WriteLine(separator);
}

static void PrintContacts(IEnumerable<Contact>? contacts)
{
    foreach (var contact in contacts ?? Enumerable.Empty<Contact>())
    {
        PrintContact(contact);
        Console.WriteLine();
    }
}

static void PrintContact(Contact? contact)
{
    if (contact != null)
    {
        Console.WriteLine($"Contact Id:\t{contact.Id}\tBirthDate:\t{contact.BirthDate}\tName:\t\t{contact.Name}");
        foreach (var email in contact.Emails ?? Enumerable.Empty<Email>())
            Console.WriteLine($"  Email Id:\t{email.Id}\tIsPrimary:\t{email.IsPrimary}\t\tAddress:\t{email.Address}");
    }
    else
    {
        Console.WriteLine($"Contact is null.");
    }
}
