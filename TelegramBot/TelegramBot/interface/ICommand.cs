using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public interface ICommand
    {
        // Команда отправки сообщений
        public Task SendMessage(string messageToSend, Message receivedMessage, ITelegramBotClient client);
    }
}
