using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public double PricePerShare { get; set; }

    }
}
