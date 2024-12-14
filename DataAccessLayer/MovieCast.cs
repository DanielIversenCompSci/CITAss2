using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    [Keyless]
    public class MovieCast
    {
        public string? tconst { get; set; }
        public string? nconst { get; set; }
        public string? role { get; set; }
        public string? primaryname { get; set; }
    }
}
