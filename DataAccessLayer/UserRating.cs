
namespace DataAccessLayer
{
    public class UserRating
    {
        public string UserId { get; set; }
        
        public string TConst { get; set; }
        
        public float Rating { get; set; }
        
        public byte[] RowVersion { get; set; }
    }
}