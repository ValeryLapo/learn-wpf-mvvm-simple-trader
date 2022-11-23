using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public class Asset
    {
        public string Symbol { get; set; }
        public double PricePerShare { get; set; }

    }
}
