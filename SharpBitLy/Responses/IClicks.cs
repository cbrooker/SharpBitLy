using System.Collections.Generic;

namespace SharpBitLy.Responses
{
    public interface IClicks
    {
        IEnumerable<IBitlyItem> Clicks { get; }
    }
}
