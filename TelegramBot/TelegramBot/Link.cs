using System.ComponentModel.DataAnnotations;

namespace TelegramBot
{
    // Модель данных для базы данных
    public class Link
    {
        [Key]
        public int id { get; set; }

        public string? Url { get; set; }

        public string? Category { get; set; }
    }
}
