using System.ComponentModel;

namespace NASA.Explorer.Domain.Aggregate.Probe
{
    public enum Orientation
    {
        [Description("North")] 
        N = 1,

        [Description("East")] 
        E = 2,

        [Description("South")] 
        S = 3,

        [Description("West")] 
        W = 4
    }
}