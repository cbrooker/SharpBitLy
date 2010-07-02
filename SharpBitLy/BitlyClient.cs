﻿using System;
using System.Linq;
using Rest4Net;
using SharpBitLy.Responses;
using SharpBitLy.Responses.Implementation;

namespace SharpBitLy
{
    public class BitlyClient : Connector
    {
        private readonly string _login;
        private readonly string _apiKey;
        private const string Format = @"json";

        public BitlyClient(string login, string apiKey)
            : base(@"api.bit.ly")
        {
            _login = login;
            _apiKey = apiKey;
        }

        protected override IRequest Build
        {
            get
            {
                return base.Build.AddQueryParam(@"login", _login)
                    .AddQueryParam(@"apiKey", _apiKey)
                    .AddQueryParam(@"format", Format)
                    .AlwaysUseContentType("application/json");
            }
        }

        /// <summary>
        /// For a long URL, /v3/shorten encodes a URL and returns a short one.
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/shorten
        /// </summary>
        /// <param name="longUrl">is a long URL to be shortened (example: http://betaworks.com/) </param>
        /// <param name="domain">refers to a preferred domain; either bit.ly default or j.mp. This affects the output value of url</param>
        /// <param name="xLogin">is the end-user's login when make requests on behalf of another bit.ly user. This allows application developers to pass along an end user's bit.ly login</param>
        /// <param name="xApiKey">is the end-user's apiKey when making requests on behalf of another bit.ly user. This allows application developers to pass along an end user's bit.ly apiKey</param>
        /// <returns>Info about short url</returns>
        public IBitlyResponse<IShorten> Shorten(string longUrl, string domain = null, string xLogin = null, string xApiKey = null)
        {
            var query = Build.SetPath("/v3/shorten");
            if (!String.IsNullOrEmpty(domain))
                query = query.AddQueryParam("domain", domain);
            if (!String.IsNullOrEmpty(xLogin))
                query = query.AddQueryParam("x_login", xLogin);
            if (!String.IsNullOrEmpty(xApiKey))
                query = query.AddQueryParam("x_apiKey", xApiKey);
            return query.AddQueryParam("longUrl", longUrl).Run<BitlyResponseImpl<IShorten, ShortenImpl>>();
        }

        /// <summary>
        /// Given a bit.ly URL or hash (or multiple), /v3/expand decodes it and returns back the target URL.
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/expand
        /// </summary>
        /// <param name="shortUrl">refers to one or more bit.ly URLs, (e.g.: http://bit.ly/1RmnUT or http://j.mp/1RmnUT) </param>
        /// <param name="shortUrls"></param>
        /// <returns>Info about original url</returns>
        public IBitlyResponse<IExpanded> Expand(string shortUrl, params string[] shortUrls)
        {
            var query = Build.SetPath("/v3/expand").AddQueryParam("shortUrl", shortUrl);
            if (shortUrls != null)
                query = shortUrls.Aggregate(query, (current, url) => current.AddQueryParam("shortUrl", url));
            return query.Run<BitlyResponseImpl<IExpanded, ExpandedImpl>>();
        }

        /// <summary>
        /// Given a bit.ly URL or hash (or multiple), /v3/expand decodes it and returns back the target URL.
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/expand
        /// </summary>
        /// <param name="hash">refers to one or more bit.ly hashes, (e.g.: 2bYgqR or a-custom-name )</param>
        /// <param name="hashes"></param>
        /// <returns>Info about original url</returns>
        public IBitlyResponse<IExpanded> ExpandWithHashes(string hash, params string[] hashes)
        {
            var query = Build.SetPath("/v3/expand").AddQueryParam("hash", hash);
            if (hashes != null)
                query = hashes.Aggregate(query, (current, h) => current.AddQueryParam("hash", h));
            return query.Run<BitlyResponseImpl<IExpanded, ExpandedImpl>>();
        }

        /// <summary>
        /// For any given a bit.ly user login and apiKey, you can validate that the pair is active.
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/validate
        /// </summary>
        /// <param name="xLogin">end users user's bit.ly login (for validation)</param>
        /// <param name="xApiKey">end users bit.ly apiKey (for validation)</param>
        /// <returns>designating whether the x_login and x_apiKey pair is currently valid</returns>
        public IBitlyResponse<IValidate> Validate(string xLogin, string xApiKey)
        {
            return Build.SetPath("/v3/validate").AddQueryParam("x_login", xLogin).AddQueryParam("x_apiKey", xApiKey).Run<BitlyResponseImpl<IValidate, ValidateImpl>>();
        }

        /// <summary>
        /// For one or more bit.ly URL's or hashes, you can generate statistics about the clicks on that link.
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/clicks
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <param name="shortUrls"></param>
        /// <returns></returns>
        public IBitlyResponse<IClicks> Clicks(string shortUrl, params string[] shortUrls)
        {
            var query = Build.SetPath("/v3/clicks").AddQueryParam("shortUrl", shortUrl);
            if (shortUrls != null)
                query = shortUrls.Aggregate(query, (current, url) => current.AddQueryParam("shortUrl", url));
            return query.Run<BitlyResponseImpl<IClicks, ClicksImpl>>();
        }

        /// <summary>
        /// For one or more bit.ly URL's or hashes, you can generate statistics about the clicks on that link.
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/clicks
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="hashes"></param>
        /// <returns></returns>
        public IBitlyResponse<IClicks> ClicksWithHashes(string hash, params string[] hashes)
        {
            var query = Build.SetPath("/v3/clicks").AddQueryParam("hash", hash);
            if (hashes != null)
                query = hashes.Aggregate(query, (current, h) => current.AddQueryParam("hash", h));
            return query.Run<BitlyResponseImpl<IClicks, ClicksImpl>>();
        }

        /// <summary>
        /// This is used to query whether a given short domain is assigned for bitly.Pro, and is
        /// consequently a valid shortUrl parameter for other api calls. keep in mind that bitly.pro
        /// domains are restricted to less than 15 characters in length.
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/bitly_pro_domain
        /// </summary>
        /// <param name="domain">A short domain (ie: nyti.ms)</param>
        /// <returns>designating whether this is a current bitly.Pro domain</returns>
        public IBitlyResponse<IValidDomain> IsBitlyProDomain(string domain)
        {
            return Build.SetPath("/v3/bitly_pro_domain").AddQueryParam("domain", domain).Run<BitlyResponseImpl<IValidDomain, ValidDomainImpl>>();
        }

        /// <summary>
        /// This is used to query for a bit.ly link based on a long URL. For example you would use
        /// /v3/lookup followed by /v3/clicks to find click data when you have a long URL to
        /// start with
        /// </summary>
        /// <param name="url">One or more long URLs to lookup</param>
        /// <param name="urls">One or more long URLs to lookup</param>
        /// <returns></returns>
        public IBitlyResponse<ILookup> Lookup(string url, params string[] urls)
        {
            var query = Build.SetPath("/v3/lookup").AddQueryParam("url", url);
            if (urls != null)
                query = urls.Aggregate(query, (current, u) => current.AddQueryParam("url", u));
            return query.Run<BitlyResponseImpl<ILookup, LookupImpl>>();
        }

        /// <summary>
        /// This is used by applications to lookup a bit.ly API key for a user given a bit.ly username and password.
        /// Access to this endpoint is restricted and must be requested by emailing api@bit.ly. When requesting
        /// access include your application login and apiKey, and a description of your use case and an estimated
        /// volume of requests.
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/authenticate
        /// </summary>
        /// <param name="xLogin">the end-user's bit.ly username or email address</param>
        /// <param name="xPassword">the end-user's bit.ly password</param>
        /// <returns></returns>
        public IBitlyResponse<IAuthenticate> Authenticate(string xLogin, string xPassword)
        {
            return Build.SetMethod(RequestType.Post).SetPath("/v3/authenticate").AddQueryParam("x_login", xLogin).AddQueryParam("x_password", xPassword)
                .Run<BitlyResponseImpl<IAuthenticate, AuthenticateImpl>>();
        }

        /// <summary>
        /// This is used to return the page title for a given bit.ly link
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/info
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <param name="shortUrls"></param>
        /// <returns></returns>
        public IBitlyResponse<IInfo> Info(string shortUrl, params string[] shortUrls)
        {
            var query = Build.SetPath("/v3/info").AddQueryParam("shortUrl", shortUrl);
            if (shortUrls != null)
                query = shortUrls.Aggregate(query, (current, url) => current.AddQueryParam("shortUrl", url));
            return query.Run<BitlyResponseImpl<IInfo, InfoImpl>>();
        }

        /// <summary>
        /// This is used to return the page title for a given bit.ly link
        /// http://code.google.com/p/bitly-api/wiki/ApiDocumentation#/v3/info
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="hashes"></param>
        /// <returns></returns>
        public IBitlyResponse<IInfo> InfoWithHashes(string hash, params string[] hashes)
        {
            var query = Build.SetPath("/v3/info").AddQueryParam("hash", hash);
            if (hashes != null)
                query = hashes.Aggregate(query, (current, h) => current.AddQueryParam("hash", h));
            return query.Run<BitlyResponseImpl<IInfo, InfoImpl>>();
        }
    }
}
