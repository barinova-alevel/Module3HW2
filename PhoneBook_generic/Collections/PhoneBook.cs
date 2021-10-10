using PhoneBook_generic.Collections.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using PhoneBook_generic.Models.Abstractions;
using System.Linq;

namespace PhoneBook_generic.Collections
{
    public class PhoneBook<T> : IPhoneBook<T> where T : IContact
    {
        private IDictionary<CultureInfo, ICollection<T>> _culturedCollections;
        private IDictionary<CharType, ICollection<T>> _specialCollections;
        private ICultureResolver _cultureResolver;
        private readonly IComparer<T> _comparer;

        public PhoneBook(IComparer<T> comparer)
        {
            _cultureResolver = new CultureResolver();
            _culturedCollections = new Dictionary<CultureInfo, ICollection<T>>();
            _culturedCollections.Add(CultureInfo.GetCultureInfo("ru-Ru"), new List<T>());
            _culturedCollections.Add(CultureInfo.GetCultureInfo("en-Gb"), new List<T>());
            _specialCollections = new Dictionary<CharType, ICollection<T>>();
            _specialCollections.Add(CharType.Number, new List<T>());
            _specialCollections.Add(CharType.Special, new List<T>());
            _comparer = comparer;
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

        public void Sort() 
        {
            Sort<CultureInfo>(_culturedCollections);
            Sort<CharType>(_specialCollections);
        }
            

        public void Sort<TKey>(IDictionary<TKey, ICollection<T>> dictionary)
        {
            foreach (var pair in dictionary)
            {
                var arr = pair.Value.ToArray();
                Array.Sort(arr, _comparer);
                dictionary[pair.Key] = arr;
            }
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
