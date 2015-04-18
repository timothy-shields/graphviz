using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shields.GraphViz.Models
{
    public abstract class Statement
    {
        private readonly IImmutableDictionary<Id, Id> attributes;

        public IImmutableDictionary<Id, Id> Attributes
        {
            get { return attributes; }
        }

        protected Statement(IImmutableDictionary<Id, Id> attributes)
        {
            if (attributes == null)
            {
                throw new ArgumentNullException("attributes");
            }

            this.attributes = attributes;
        }

        protected void WriteAttributesTo(StreamWriter writer)
        {
            var i = 0;
            foreach (var pair in attributes)
            {
                if (i > 0)
                {
                    writer.Write(',');
                }
                pair.Key.WriteTo(writer);
                writer.Write('=');
                pair.Value.WriteTo(writer);
                i++;
            }
        }

        public abstract void WriteTo(StreamWriter writer, GraphKinds graphKind);
    }
}
