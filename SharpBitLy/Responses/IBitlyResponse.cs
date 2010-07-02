﻿namespace SharpBitLy.Responses
{
    public interface IBitlyResponse<out TDataInterface>
    {
        int StatusCode { get; }
        string StatusTxt { get; }
        TDataInterface Data { get; }
    }
}
