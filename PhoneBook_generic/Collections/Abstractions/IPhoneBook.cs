using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook_generic.Models.Abstractions;

namespace PhoneBook_generic.Collections.Abstractions
{
    public interface IPhoneBook<T> where T : IContact
    {
        public void Add(T contact);
        public void Sort();

        public IReadOnlyCollection<T> this[string key] { get; }
    }
}
