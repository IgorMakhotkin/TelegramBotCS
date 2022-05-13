using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public interface IRepository
{
        public Task Usage(ITelegramBotClient botClient, Message message, ICommand send);

        public Task StoreLink(ITelegramBotClient botClient, Message message, ICommand send);

        public Task GetLink(ITelegramBotClient botClient, Message message, ICommand send);

        public Task ExecutAsync();

}
}
