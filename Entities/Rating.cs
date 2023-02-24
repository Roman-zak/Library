using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public Book Book { get; set; }
    }
}