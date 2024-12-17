using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    [Keyless]
    public class CoPlayer
    {
        public string nconst { get; set; }
        public string primaryname { get; set; }
        public int frequency { get; set; }
    }
}
