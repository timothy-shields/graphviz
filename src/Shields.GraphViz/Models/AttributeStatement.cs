using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shields.GraphViz.Models
{
    public class AttributeStatement : Statement
    {
        public static AttributeStatement OfKind(AttributeKinds kind)
        {
            return new AttributeStatement(kind, ImmutableDictionary<Id, Id>.Empty);
        }

        public static AttributeStatement Graph
        {
            get { return OfKind(AttributeKinds.Graph); }
        }

        public static AttributeStatement Node
        {
            get { return OfKind(AttributeKinds.Node); }
        }

        public static AttributeStatement Edge
        {
            get { return OfKind(AttributeKinds.Edge); }
        }

        private readonly AttributeKinds attributeKind;

        public AttributeKinds AttributeKind
        {
            get { return attributeKind; }
        }

        public AttributeStatement(AttributeKinds attributeKind, IImmutableDictionary<Id, Id> attributes)
            : base(attributes)
        {
            this.attributeKind = attributeKind;
        }

        public AttributeStatement Set(Id key, Id value)
        {
            return new AttributeStatement(attributeKind, Attributes.Add(key, value));
        }

        private static readonly IReadOnlyDictionary<AttributeKinds, string> attributeKindName = new Dictionary<AttributeKinds, string>
        {
            { AttributeKinds.Graph, "graph" },
            { AttributeKinds.Node, "node" },
            { AttributeKinds.Edge, "edge" },
        };

        public override void WriteTo(StreamWriter writer, GraphKinds graphKind)
        {
            if (Attributes.Any())
            {
                writer.Write(attributeKindName[attributeKind]);
                writer.Write('[');
                WriteAttributesTo(writer);
                writer.Write(']');
                writer.Write(';');
            }
        }
    }
}
