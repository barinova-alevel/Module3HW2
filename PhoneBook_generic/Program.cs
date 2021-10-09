using PhoneBook_generic.Collections;
using PhoneBook_generic.Models;
using System;

namespace PhoneBook_generic
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneBook = new PhoneBook<Contact>();
            phoneBook.Add(new Contact() { Name = "Oksana", LastName = "Barinova" });
            phoneBook.Add(new Contact() { Name = "Oksana", LastName = "Borokh" });
            phoneBook.Add(new Contact() { Name = "Olena", LastName = "Олійник" });

            var p = phoneBook["Ok"];
        }
    }
}
