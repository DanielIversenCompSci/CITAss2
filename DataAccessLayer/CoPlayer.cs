using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    [Keyless] // Since the result is a projection and doesn't have a primary key
    public class CoPlayer
    {
        public string nconst { get; set; } // Unique identifier for the co-actor
        public string primaryname { get; set; } // Name of the co-actor
        public int frequency { get; set; } // Number of times they worked together
    }
}
