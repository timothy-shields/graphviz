using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shields.GraphViz.Models
{
    public class NodeStatement : Statement
    {
        public static NodeStatement For(Id id)
        {
            return new NodeStatement(id, ImmutableDictionary<Id, Id>.Empty);
        }

        private readonly Id id;

        public Id Id
        {
            get { return id; }
        }

        public NodeStatement(Id id, IImmutableDictionary<Id, Id> attributes)
            : base(attributes)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.id = id;
        }

        public NodeStatement Set(Id key, Id value)
        {
            return new NodeStatement(id, Attributes.Add(key, value));
        }

        public override void WriteTo(StreamWriter writer, GraphKinds graphKind)
        {
            id.WriteTo(writer);
            if (Attributes.Any())
            {
                writer.Write('[');
                WriteAttributesTo(writer);
                writer.Write(']');
            }
            writer.Write(';');
        }
    }
}
