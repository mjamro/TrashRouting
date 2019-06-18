using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Cluster.Commands
{
    [MessageNamespace("cluster")]
    public class ScheduleClusterAlgorithmCommand : ICommand
    {
        public int PointsNumber { get; set; }
    }
}
