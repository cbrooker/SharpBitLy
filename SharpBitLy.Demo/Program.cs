namespace SharpBitLy.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new BitlyClient(@"ioleksiy", @"R_e354e81a3f95be42d77405d3599a45d2"))
            {
                var shorten = client.Shorten("http://google.com.ua/");
                var expanded1 = client.Expand(shorten.Data.Url);
                var expanded2 = client.ExpandWithHashes(shorten.Data.Hash);
                var validate = client.Validate("ioleksiy", "R_e354e81a3f95be42d77405d3599a45d2");
                var validDomain = client.IsBitlyProDomain("nyti.ms");
                var lookup = client.Lookup("http://betaworks.com/");
                var auth = client.Authenticate("ioleksiy", "not_my_password :)))");
                var info = client.Info("http://google.com.ua/");
            }
        }
    }
}
