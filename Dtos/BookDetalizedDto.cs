namespace Library.Dtos
{
    public class BookDetalizedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal Rating { get; set; }
        public List<ReviewGetDto> Reviews { get; set; }
    }
}