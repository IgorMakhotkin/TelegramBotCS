using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    {
        public static bool SaveLinksFlag { get; private set; } = false;

        public static bool SaveLinksStage1 { get; private set; } = false;

        public static bool SaveLinksStage2 { get; private set; } = false;

        public static bool GetLinksFlag { get; private set; } = false;

        public static bool GetLinksStage1 { get; private set; } = false;

        public static string tempLinks { get; private set; } = string.Empty;

        public static string tempCategory { get; private set; } = string.Empty;


        public static async Task SaveLinks(ITelegramBotClient botClient, Message message)
        {
        }

        public static async Task GetLinks(ITelegramBotClient botClient, Message message)
        {
        }
    }
}
