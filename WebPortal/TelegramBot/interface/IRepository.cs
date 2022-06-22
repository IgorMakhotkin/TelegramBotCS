using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public interface IRepository
{
        public Task Usage(ITelegramBotClient botClient, Message message, ICommand send);

        public Task StoreLink(ITelegramBotClient botClient, Message message, ICommand send, Dictionary<long, UserData> usersDict);

        public Task GetLink(ITelegramBotClient botClient, Message message, ICommand send, Dictionary<long, UserData> usersDict);

        public Task ExecutAsync(Message message, Dictionary<long, UserData> usersDict);

}
}
