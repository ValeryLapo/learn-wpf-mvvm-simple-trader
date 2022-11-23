
namespace SimpleTrader.Domain.Models
{
    public enum MajorIndexType
    {
        Apple,
        Amazon,
        Google
    }
    public class MajorIndex
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }
        public MajorIndexType Type { get; set; }
    }
}
