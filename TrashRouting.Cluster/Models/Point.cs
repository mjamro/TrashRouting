using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrashRouting.Cluster.Models
{
    public class Point
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
