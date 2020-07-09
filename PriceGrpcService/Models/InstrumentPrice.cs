using System;

namespace PriceGrpcService.Models
{
    public class InstrumentPrice
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public DateTime Time { get; set; }
        public InstrumentPrice(string name, string price)
        {
            Name = name;
            Price = price;
            Time = DateTime.Now;
        }
    }
}
