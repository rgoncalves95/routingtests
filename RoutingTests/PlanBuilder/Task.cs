using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingTests.PlanBuilder
{
    class Task
    {
        public string Display { get; set; }

        public Guid Id { get; set; }

        public DateTime DueDate { get; set; }

        public int Duration { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
