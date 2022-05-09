using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Command : ICommand
    {
        public async Task SendMessage(string messageToSend, Message receivedMessage, ITelegramBotClient client)
        {
            var chatId = receivedMessage.Chat.Id;
            await client.SendTextMessageAsync(chatId, messageToSend);
        }
    }
}
