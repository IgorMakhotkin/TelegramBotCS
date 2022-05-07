using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class BotFunction : ICommand
    {
        public static bool SaveLinksFlag { get; private set; } = false;

        public static bool SaveLinksStage1 { get; private set; } = false;

        public static bool SaveLinksStage2 { get; private set; } = false;

        public static bool GetLinksFlag { get; private set; } = false;

        public static bool GetLinksStage1 { get; private set; } = false;

        public static string tempLinks { get; private set; } = string.Empty;

        public static string tempCategory { get; private set; } = string.Empty;

        


        public static async Task<string> SaveLinks( string message)
        {
            IStorage storage = new Storage();
            SaveLinksFlag = true;

            if (SaveLinksFlag && !SaveLinksStage1 && !SaveLinksStage2)
            { 
                SaveLinksStage1 = true;
                return "Введите ссылку";
            }

            if (SaveLinksStage1)
            {
                tempLinks = message!;
                SaveLinksStage1 = false;
                SaveLinksStage2 = true;
                return "Введите категорию";
            }

            if (SaveLinksStage2)
            {
                tempCategory = message;

                if (await storage.AddLinksToStorage(tempCategory, tempLinks))
                {
                    SaveLinksStage2 = false;
                    SaveLinksFlag = false;
                    return "Запись добавлена";
                }
                else
                {
                    SaveLinksStage2 = false;
                    SaveLinksFlag = false;
                    return "Запись не добавлена";
                }
            }
            return "Повторите запрос";
        }

        public static async Task <string> GetLinks( string message)
        {
            IStorage storage = new Storage();
            string tempCategory = null;
            GetLinksFlag = true;

            if (GetLinksFlag && !GetLinksStage1)
            {
                GetLinksStage1 = true;
                return "Какую категорию получить";
            }

            if (GetLinksStage1)
            {
                tempCategory = message!;
                string answer = storage.ReturnLinks(tempCategory);
                GetLinksFlag = false;
                GetLinksStage1 = false;
                return $"Ссылка:{answer}";
            }
            return "Повторите запрос";
        }
    }
}
