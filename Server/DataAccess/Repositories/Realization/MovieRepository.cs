using DataAccess.EF;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Realization
{
    public class MovieRepository : BaseRepository<Movie>
    {
        public MovieRepository(AppDbContext context) : base(context) { }
    }
}
