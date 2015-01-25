using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shields.GraphViz.Models
{
    public class Graph
    {
        public static Graph OfKind(GraphKinds kind)
        {
            return new Graph(kind, null, ImmutableList<Statement>.Empty);
        }

        public static Graph Undirected
        {
            get { return OfKind(GraphKinds.Undirected); }
        }

        public static Graph Directed
        {
            get { return OfKind(GraphKinds.Directed); }
        }

        private readonly GraphKinds graphKind;
        private readonly Id name;
        private readonly IImmutableList<Statement> statements;

        public GraphKinds GraphKind
        {
            get { return graphKind; }
        }

        public Id Name
        {
            get { return name; }
        }

        public IImmutableList<Statement> Statements
        {
            get { return statements; }
        }

        public Graph(GraphKinds graphKind, Id name, IImmutableList<Statement> statements)
        {
            if (statements == null)
            {
                throw new ArgumentNullException("statements");
            }

            this.graphKind = graphKind;
            this.name = name;
            this.statements = statements;
        }

        public Graph Named(Id name)
        {
            return new Graph(graphKind, name, statements);
        }

        public Graph Add(Statement statement)
        {
            return new Graph(graphKind, name, statements.Add(statement));
        }

        public Graph AddRange(IEnumerable<Statement> statements)
        {
            return new Graph(graphKind, name, this.statements.AddRange(statements));
        }

        private static readonly IReadOnlyDictionary<GraphKinds, string> graphKindName = new Dictionary<GraphKinds, string>
        {
            { GraphKinds.Undirected, "graph" },
            { GraphKinds.Directed, "digraph" }
        };

        public void WriteTo(StreamWriter writer)
        {
            writer.Write(graphKindName[graphKind]);
            if (name != null)
            {
                writer.Write(' ');
                name.WriteTo(writer);
            }
            writer.WriteLine('{');
            foreach (var statement in statements)
            {
                statement.WriteTo(writer, graphKind);
                writer.WriteLine();
            }
            writer.WriteLine('}');
        }
    }
}
