using System.ComponentModel.DataAnnotations;
namespace WebPortal.db
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }

        public long UserId { get; set; }

        public string? Password { get; set; }
    }
}
