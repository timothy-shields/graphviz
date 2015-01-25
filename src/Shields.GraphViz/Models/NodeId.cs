using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shields.GraphViz.Models
{
    public class NodeId
    {
        private readonly Id id;
        private readonly Port port;

        public Id Id
        {
            get { return id; }
        }

        public Port Port
        {
            get { return port; }
        }

        public NodeId(Id id, Port port = null)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.id = id;
            this.port = port;
        }

        public static implicit operator NodeId(string value)
        {
            return new NodeId(new Id(value));
        }

        public void WriteTo(StreamWriter writer)
        {
            id.WriteTo(writer);
            if (port != null)
            {
                port.WriteTo(writer);
            }
        }
    }
}
