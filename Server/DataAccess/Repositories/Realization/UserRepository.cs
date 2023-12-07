using DataAccess.EF;
using DataAccess.Entities;

namespace DataAccess.Repositories.Realization
{
    public class UserRepository : BaseRepository<AppUser>
    {
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
