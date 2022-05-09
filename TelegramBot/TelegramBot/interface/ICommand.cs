using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public interface ICommand
    {
        // Обьявление обобшенного метода отправки сообщений
        public Task SendMessage< TMessage, TClient>(string messageToSend, TMessage receivedMessage, TClient client)
            where TMessage : Message
            where TClient : ITelegramBotClient;
    }
}
