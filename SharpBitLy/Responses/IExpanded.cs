using System.Collections.Generic;

namespace SharpBitLy.Responses
{
    public interface IExpanded
    {
        IEnumerable<IBitlyItem> Expand { get; }
    }
}
