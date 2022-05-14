using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class TelegramCommandInput
    {
        public string TextMessage { get; set; }

        public ITelegramBotClient client { get; set; }

        public Message message { get; set; }

        public TelegramCommandInput(ITelegramBotClient botClient, Message receivedMessage, string messageToSend)
        {
            this.client = botClient;
            this.message = receivedMessage;
            this.TextMessage = messageToSend;
        }

        public async Task sendMessage()
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, TextMessage);
        }
    }
}