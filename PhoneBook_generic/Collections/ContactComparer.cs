using PhoneBook_generic.Models.Abstractions;
using System.Collections.Generic;

namespace PhoneBook_generic.Collections
{
    public class ContactComparer : IComparer<IContact>
    {
        public int Compare(IContact x, IContact y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
