using System.Collections.Generic;

namespace SharpBitLy.Responses
{
    public interface ILookup
    {
        IEnumerable<ILookupItem> Lookup { get; }
    }

    public interface ILookupItem
    {
        string GlobalHash { get; }
        string ShortUrl { get; }
        string Url { get; }
    }
}
