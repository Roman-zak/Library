using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Reviewer { get; set; }
        public Book Book { get; set; }
    }
}