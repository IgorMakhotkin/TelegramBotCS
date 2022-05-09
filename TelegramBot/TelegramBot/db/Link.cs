using System.ComponentModel.DataAnnotations;

namespace TelegramBot
{
    // Модель данных для базы данных
    public class Link
    {
        [Key]
        public int Id { get; set; }

        public string? Url { get; set; }

        public string? Category { get; set; }
    }
}
