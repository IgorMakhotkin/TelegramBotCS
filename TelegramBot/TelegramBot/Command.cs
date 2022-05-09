using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Command : ICommand
    {
        // Реализация обобщенного метода 
       public async Task SendMessage<TMessage, TClient>(string messageToSend, TMessage receivedMessage, TClient client)
            where TMessage : Message
            where TClient : ITelegramBotClient
        {
            var chatId = receivedMessage.Chat.Id;
            await client.SendTextMessageAsync(chatId, messageToSend);
        }
    }
}
