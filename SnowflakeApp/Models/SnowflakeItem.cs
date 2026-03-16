namespace SnowflakeApp.Models
{
    public partial class SnowflakeItem 
    {
        public string Name { get; set; }
        public float Complexity { get; set; }
        public float BranchAngle { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsFavorite { get; set; }
        public string FavoriteIcon => IsFavorite ? "❤️" : "🤍";
    }
}