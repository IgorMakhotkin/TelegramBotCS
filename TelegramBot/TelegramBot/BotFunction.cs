namespace TelegramBot
{
    public class BotFunction
    {
        public static bool SaveLinksFlag { get; private set; } = false;

        public static bool SaveLinksStage1 { get; private set; } = false;

        public static bool SaveLinksStage2 { get; private set; } = false;

        public static bool GetLinksFlag { get; private set; } = false;

        public static bool GetLinksStage1 { get; private set; } = false;

        public static string TempLinks { get; private set; } = string.Empty;

        public static string TempCategory { get; private set; } = string.Empty;

        public static async Task<string> SaveLinks(string message)
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
                TempLinks = message!;
                SaveLinksStage1 = false;
                SaveLinksStage2 = true;
                return "Введите категорию";
            }

            if (SaveLinksStage2)
            {
                TempCategory = message;

                if (await storage.AddLinksToStorage(TempCategory, TempLinks))
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

        public static string GetLinks(string message)
        {
            IStorage storage = new Storage();
            string TempCategory = null;
            GetLinksFlag = true;

            if (GetLinksFlag && !GetLinksStage1)
            {
                GetLinksStage1 = true;
                return "Какую категорию получить";
            }

            if (GetLinksStage1)
            {
                TempCategory = message!;
                string answer = storage.ReturnLinks(TempCategory);
                GetLinksFlag = false;
                GetLinksStage1 = false;
                return $"Ссылка:{answer}";
            }
            return "Повторите запрос";
        }
    }
}
