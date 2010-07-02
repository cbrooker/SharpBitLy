namespace SharpBitLy.Responses
{
    public interface IAuthenticate
    {
        IAuthenticateItem Authenticate { get; }
    }

    public interface IAuthenticateItem
    {
        bool Successful { get; }
        string Username { get; }
        string ApiKey { get; }
    }
}
