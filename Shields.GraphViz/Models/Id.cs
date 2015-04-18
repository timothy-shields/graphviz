using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shields.GraphViz.Models
{
    public class Id
    {
        private readonly string value;

        public string Value
        {
            get { return value; }
        }

        public Id(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.value = value;
        }

        public static implicit operator Id(string value)
        {
            return new Id(value);
        }

        public void WriteTo(StreamWriter writer)
        {
            const string quote = "\"";
            const string backslash = "\\";
            var escapedValue = quote + value.Replace(quote, backslash + quote) + quote;
            writer.Write(escapedValue);
        }
    }
}
