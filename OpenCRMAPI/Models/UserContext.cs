using Microsoft.EntityFrameworkCore;

namespace OpenCRMAPI.Models
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<UserDetails> UserDetailsList {get; set;}
    }
}