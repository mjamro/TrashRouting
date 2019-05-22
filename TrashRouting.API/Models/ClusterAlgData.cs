using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrashRouting.API.Models
{
    public class ClusterAlgData
    {
        public int ClusterId { get; set; }
        public string CompanyName { get; set; }
        public int TrucksCount { get; set; }
        public IEnumerable<Point> Points { get; set; }
    }
}
