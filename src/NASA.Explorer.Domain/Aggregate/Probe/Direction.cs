using System.ComponentModel;

namespace NASA.Explorer.Domain.Aggregate.Probe
{
    public enum Direction
    {
        [Description("Right")] 
        R,

        [Description("Left")] 
        L
    }
}