using PhoneBook_generic.Collections.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PhoneBook_generic.Models.Abstractions;

namespace PhoneBook_generic.Collections
{
    public class PhoneBook<T> : IPhoneBook<T> where T : IContact
    {
        private IDictionary<CultureInfo, ICollection<T>> _culturedCollections;
        private IDictionary<CharType, ICollection<T>> _specialCollections;
        private ICultureResolver _cultureResolver;

        public PhoneBook()
        {
            _cultureResolver = new CultureResolver();
            _culturedCollections = new Dictionary<CultureInfo, ICollection<T>>();
            _culturedCollections.Add(CultureInfo.GetCultureInfo("ru-Ru"), new List<T>());
            _culturedCollections.Add(CultureInfo.GetCultureInfo("en-Gb"), new List<T>());
            _specialCollections = new Dictionary<CharType, ICollection<T>>();
            _specialCollections.Add(CharType.Number, new List<T>());
            _specialCollections.Add(CharType.Special, new List<T>());
        }

        public void Add(T contact)
        {
            if (String.IsNullOrEmpty(contact.Name))
            {
                throw new ArgumentException("Name is null or empty");
            }

            var collection = DetermineCollection(contact.Name);
            collection.Add(contact);
        }

        public ICollection<T> DetermineCollection(string name)
        {
            var cultureInfo = _cultureResolver.GetCultureInfo(name);

            if (cultureInfo == null)
            {
                if (Regex.IsMatch(name[0].ToString(), "[0-9]"))
                {
                    return _specialCollections[CharType.Number];
                }
                else
                {
                    return _specialCollections[CharType.Special];
                }
            }
            return _culturedCollections[cultureInfo];
        }

        public IReadOnlyCollection<T> this[string key]
        {
            get
            {
                var collection = DetermineCollection(key);
                var result = new List<T>();

                foreach (var contact in collection)
                {
                    if (contact.Name.StartsWith(key, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result.Add(contact);
                    }
                }
                return result;
            }
        }
    }
}
