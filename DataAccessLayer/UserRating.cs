
namespace DataAccessLayer
{
    public class UserRating
    {
        public string UserId { get; set; }
        
        public string TConst { get; set; } = string.Empty;
        
        public float Rating { get; set; }
    }
}