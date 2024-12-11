
namespace DataAccessLayer
{
    public class Users
    {
        public int UserId { get; set; }
        
        public string Email { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }

        public string Salt { get; set; }

        // Navigation property for the search history
        public ICollection<SearchHis> SearchHistory { get; set; } = new List<SearchHis>();
        
        public ICollection<UserRating> UserRatings { get; set; } = new List<UserRating>();
        
        public ICollection<UserBookmarks> UserBookmarks { get; set; } = new List<UserBookmarks>();
    }
}