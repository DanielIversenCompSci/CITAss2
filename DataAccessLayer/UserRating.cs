
namespace DataAccessLayer
{
    public class UserRating
    {
        public int UserId { get; set; }
        
        public string TConst { get; set; }
        
        public float Rating { get; set; }
        
        public int UserRatingId { get; set; }
        
        //NAV
        public Users User { get; set; }
        
    }
}