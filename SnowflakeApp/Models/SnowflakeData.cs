namespace SnowflakeApp.Models
{
    public class SnowflakeData
    {
        public string Name { get; set; }
        public int FormulaType { get; set; }
        public float ParameterA { get; set; } //ползунок 1
        public float ParameterB { get; set; } //ползунок 2
        public DateTime CreatedAt { get; set; }
    }
}