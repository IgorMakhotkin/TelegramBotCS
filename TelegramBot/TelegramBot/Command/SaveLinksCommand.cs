using Telegram.Bot.Types;

namespace TelegramBot
{
    public class SaveLinksCommand
    {
        public async Task<string> SaveLinks(Message message, Dictionary<long, User> usersDict)
        {
            User user = new User();
            user = usersDict[message.Chat.Id];
            IStorage storage = new Storage();
            user.SaveLinksFlag = true;

            if (user.SaveLinksFlag && !user.SaveLinksStage1 && !user.SaveLinksStage2)
            {
                user.SaveLinksStage1 = true;
                usersDict.Remove(message.Chat.Id);
                usersDict.Add(message.Chat.Id, user);
                return "Введите ссылку";
            }

            if (user.SaveLinksStage1)
            {
                user.TempUrl = message.Text!;
                user.SaveLinksStage1 = false;
                user.SaveLinksStage2 = true;
                usersDict.Remove(message.Chat.Id);
                usersDict.Add(message.Chat.Id, user);
                return "Введите категорию";
            }

            if (user.SaveLinksStage2)
            {
                user.TempCategory = message.Text;
                usersDict.Remove(message.Chat.Id);
                usersDict.Add(message.Chat.Id, user);

                if (await storage.AddLinksToStorage(user.TempCategory, user.TempUrl))
                {
                    user.SaveLinksStage2 = false;
                    user.SaveLinksFlag = false;
                    usersDict.Remove(message.Chat.Id);
                    usersDict.Add(message.Chat.Id, user);
                    return "Запись добавлена";
                }
                else
                {
                    user.SaveLinksStage2 = false;
                    user.SaveLinksFlag = false;
                    usersDict.Remove(message.Chat.Id);
                    usersDict.Add(message.Chat.Id, user);
                    return "Запись не добавлена";
                }
            }

            return "Повторите запрос";
        }
    }
}
