using Microsoft.Extensions.DependencyInjection;
using PhoneBook_generic.Collections;
using PhoneBook_generic.Collections.Abstractions;
using PhoneBook_generic.Models;
using System;
using System.Collections.Generic;

namespace PhoneBook_generic
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IPhoneBook<Contact>, PhoneBook<Contact>>()
                .AddTransient<IComparer<Contact>, ContactComparer>()
                .BuildServiceProvider();

            var phoneBook = serviceProvider.GetService<IPhoneBook<Contact>>();

            phoneBook.Add(new Contact() { Name = "Oksana", LastName = "Barinova" });
            phoneBook.Add(new Contact() { Name = "Oasana", LastName = "Borokh" });
            phoneBook.Add(new Contact() { Name = "Olena", LastName = "Олійник" });
            phoneBook.Add(new Contact() { Name = "*&^", LastName = "Олійник" });
            phoneBook.Add(new Contact() { Name = "прпр", LastName = "Олійник" });
            phoneBook.Add(new Contact() { Name = " uui", LastName = "Олійник" });
            phoneBook.Add(new Contact() { Name = "12345", LastName = "Олійник" });
            phoneBook.Sort();
            
            foreach (var contact in phoneBook["O"])
            {
                Console.WriteLine(contact.Name);
            }

            Console.ReadKey();
        }
    }
}
