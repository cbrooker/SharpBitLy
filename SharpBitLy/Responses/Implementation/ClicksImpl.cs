﻿using System.Collections.Generic;

namespace SharpBitLy.Responses.Implementation
{
    internal class ClicksImpl : IClicks
    {
        private readonly IList<BitlyItemImpl> _clicks = new List<BitlyItemImpl>();

        public IEnumerable<IBitlyItem> Clicks
        {
            get { return _clicks; }
        }
    }
}
