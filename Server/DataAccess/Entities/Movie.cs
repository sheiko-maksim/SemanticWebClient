using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Movie : BaseEntity
    {
        public string? Title { get; set; }
        public string? PublicationDate { get; set; }
        public MovieType MovieType { get; set; }
        public string? Director { get; set; }
        public string? Genres { get; set; }
        public string? Topics { get; set; }
        public string? Imdb { get; set; }
    }
}
