using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PhoneBook_generic.Collections.Abstractions
{
    public interface ICultureResolver
    {
        public CultureInfo GetCultureInfo(string name);
    }
}
