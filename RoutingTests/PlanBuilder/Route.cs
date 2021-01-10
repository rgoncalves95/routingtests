using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingTests.PlanBuilder
{
    class Route
    {
        public Guid From { get; set; }

        public Guid To { get; set; }

        public int Distance { get; set; }
        public int TimeInSeconds { get; set; }
    }
}
