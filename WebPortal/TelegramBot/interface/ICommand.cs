using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public interface ICommand
    {
        // Обьявление обобшенного метода отправки сообщений
        public Task ExecuteAsync<TCommandInput>(TCommandInput input)
            where TCommandInput : TelegramCommandInput;
    }
}