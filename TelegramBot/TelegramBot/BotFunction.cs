using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class BotFunction : Storage, ICommand
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
            SaveLinksFlag = true;

            if (SaveLinksFlag && !SaveLinksStage1 && !SaveLinksStage2)
            {
                await ICommand.SendMessage("Введите ссылку", message, botClient);
                SaveLinksStage1 = true;
                return;
            }

            if (SaveLinksStage1)
            {
                tempLinks = message.Text!;
                await ICommand.SendMessage("Введите категорию", message, botClient);
                SaveLinksStage1 = false;
                SaveLinksStage2 = true;
                return;
            }

            if (SaveLinksStage2)
            {
                tempCategory = message.Text!;

                if (await Storage.AddLinksToStorage(tempCategory, tempLinks))
                {
                    await ICommand.SendMessage("Запись добавлена", message, botClient);
                    SaveLinksStage2 = false;
                    SaveLinksFlag = false;
                    return;
                }
                else
                {
                    SaveLinksStage2 = false;
                    SaveLinksFlag = false;
                    await ICommand.SendMessage("Запись не добавлена", message, botClient);
                    return;
                }
            }
        }

        public static async Task GetLinks(ITelegramBotClient botClient, Message message)
        {
            string tempCategory = null;
            GetLinksFlag = true;

            if (GetLinksFlag && !GetLinksStage1)
            {
                await ICommand.SendMessage("Какую категорию получить", message, botClient);
                GetLinksStage1 = true;
                return;
            }

            if (GetLinksStage1)
            {
                tempCategory = message.Text!;
                string answer = Storage.ReturnLinks(tempCategory);
                await ICommand.SendMessage($"Ссылка:{answer}", message, botClient);
                GetLinksFlag = false;
                GetLinksStage1 = false;
                return;
            }
        }
    }
}
