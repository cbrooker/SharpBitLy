using System.Collections.Generic;

namespace SharpBitLy.Responses
{
    public interface IInfo
    {
        IEnumerable<IInfoItem> Info { get; }
    }

    public interface IInfoItem
    {
        string GlobalHash { get; }
        string ShortUrl { get; }
        string Hash { get; }
        string UserHash { get; }
        string Error { get; }
        string Title { get; }
        string CreatedBy { get; }
    }
}
