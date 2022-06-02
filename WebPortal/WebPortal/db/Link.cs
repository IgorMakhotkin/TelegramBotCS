
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.db
{
    public class Link
    {
        [Key]
        public int Id { get; set; }

        public long UserId { get; set; }

        public string? Url { get; set; }

        public string? Category { get; set; }
    }
}
