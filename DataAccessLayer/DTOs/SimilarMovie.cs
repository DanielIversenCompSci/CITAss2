using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    [Keyless]
    public class SimilarMovie
    {
        public string similar_tconst { get; set; }
        public string? primarytitle { get; set; }
        public string? plot { get; set; }
        public string? poster { get; set; }
        public long shared_genres { get; set; }
    }
}
