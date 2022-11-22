
namespace SimpleTrader.Domain.Models
{
    public enum MajorIndexType
    {
        Apple,
        Facebook,
        Google
    }
    public class MajorIndex
    {
        public double Price { get; set; }
        public double Change { get; set; }
        public MajorIndexType Type { get; set; }
    }
}
