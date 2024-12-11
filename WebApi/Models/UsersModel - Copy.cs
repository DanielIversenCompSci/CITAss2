using DataAccessLayer;

namespace WebApi.Models;

public class UsersModel
{
    public string Url { get; set; }
    
    public int UserId { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}