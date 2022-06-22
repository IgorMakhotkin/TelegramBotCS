using Telegram.Bot.Types;

namespace TelegramBot
{
    public class GetLinksCommand
    {
        public async Task<string> GetLinks(Message message, Dictionary<long, UserData> usersDict)
        {
            UserData user = new UserData();
            user = usersDict[message.Chat.Id];
            IStorage storage = new Storage();
            user.GetLinksFlag = true;

            if (user.GetLinksFlag && !user.GetLinksStage1)
            {
                user.GetLinksStage1 = true;
                usersDict.Remove(message.Chat.Id);
                usersDict.Add(message.Chat.Id, user);
                return "Какую категорию получить";
            }

            if (user.GetLinksStage1)
            {
                string answer = storage.ReturnLinks(message.Text!);
                user.GetLinksFlag = false;
                user.GetLinksStage1 = false;
                usersDict.Remove(message.Chat.Id);
                usersDict.Add(message.Chat.Id, user);
                return $"Ссылка:{answer}";
            }

            return "Повторите запрос";
        }
    }
}
