using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepuloJaratok
{
    class NegativKesesException : Exception
    {
        public NegativKesesException() : base("A teljes késés nem lehet negatív!")
        {
        }
    }
}
