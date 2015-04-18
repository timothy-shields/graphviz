using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shields.GraphViz.Models
{
    public class Port
    {
        public static Port North
        {
            get { return new Port(null, CompassPoints.North); }
        }

        private readonly Id id;
        private readonly CompassPoints? compassPoint;

        public Id Id
        {
            get { return id; }
        }

        public CompassPoints? CompassPoint
        {
            get { return compassPoint; }
        }

        public Port(Id id, CompassPoints? compassPoint)
        {
            if (id == null && compassPoint == null)
            {
                throw new ArgumentException("A port must specify either an ID, a compass point, or both.");
            }

            this.id = id;
            this.compassPoint = compassPoint;
        }

        private static readonly IReadOnlyDictionary<CompassPoints, string> compassPointName = new Dictionary<CompassPoints, string>
        {
            { CompassPoints.North, "n" },
            { CompassPoints.NorthEast, "ne" },
            { CompassPoints.East, "e" },
            { CompassPoints.SouthEast, "se" },
            { CompassPoints.South, "s" },
            { CompassPoints.SouthWest, "sw" },
            { CompassPoints.West, "w" },
            { CompassPoints.NorthWest, "nw" },
        };

        public void WriteTo(StreamWriter writer)
        {
            if (id != null)
            {
                writer.Write(':');
                id.WriteTo(writer);
            }
            if (compassPoint != null)
            {
                writer.Write(':');
                writer.Write(compassPointName[compassPoint.Value]);
            }
        }
    }
}
