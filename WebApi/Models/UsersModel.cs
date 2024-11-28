using DataAccessLayer;

namespace WebApi.Models;

public class UsersModel
{
    public string Url { get; set; }
    
    public string UserId { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public ICollection<SearchHis> SearchHistory { get; set; } = new List<SearchHis>();
}