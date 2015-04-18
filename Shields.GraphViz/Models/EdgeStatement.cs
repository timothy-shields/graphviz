using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shields.GraphViz.Models
{
    public class EdgeStatement : Statement
    {
        public static EdgeStatement For(NodeId fromId, NodeId toId)
        {
            return new EdgeStatement(fromId, toId, ImmutableDictionary<Id, Id>.Empty);
        }

        private readonly NodeId fromId;
        private readonly NodeId toId;

        public EdgeStatement(NodeId fromId, NodeId toId, IImmutableDictionary<Id, Id> attributes)
            : base(attributes)
        {
            if (fromId == null)
            {
                throw new ArgumentNullException("fromId");
            }
            if (toId == null)
            {
                throw new ArgumentNullException("toId");
            }

            this.fromId = fromId;
            this.toId = toId;
        }

        public EdgeStatement Set(Id key, Id value)
        {
            return new EdgeStatement(fromId, toId, Attributes.Add(key, value));
        }

        private static Dictionary<GraphKinds, string> edgeOp = new Dictionary<GraphKinds, string>
        {
            { GraphKinds.Undirected, "--" },
            { GraphKinds.Directed, "->" }
        };

        public override void WriteTo(StreamWriter writer, GraphKinds graphKind)
        {
            fromId.WriteTo(writer);
            writer.Write(edgeOp[graphKind]);
            toId.WriteTo(writer);
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
