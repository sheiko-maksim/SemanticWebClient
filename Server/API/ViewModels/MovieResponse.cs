namespace API.ViewModels
{
    public class MovieResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? PublicationDate { get; set; }
        public string? Director { get; set; }
        public string? Genres { get; set; }
        public string? Topics { get; set; }
        public string? Imdb { get; set; }
    }
}
