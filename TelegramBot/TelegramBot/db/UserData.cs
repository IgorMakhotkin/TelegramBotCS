namespace TelegramBot
{
    public class UserData
    {

        public string? TempUrl { get; set; }

        public string? TempCategory { get; set; }

        public bool GetLinksFlag { get; set; } = false;

        public bool GetLinksStage1 { get; set; } = false;

        public bool SaveLinksFlag { get; set; } = false;

        public bool SaveLinksStage1 { get; set; } = false;

        public bool SaveLinksStage2 { get; set; } = false;


    }
}
