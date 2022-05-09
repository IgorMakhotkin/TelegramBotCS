using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class TelegramChatAPI : IChat
    {
        private static TelegramBotClient? Bot;

        public TelegramChatAPI()
        {

            Bot = new TelegramBotClient(Configuration.BotToken);
            using var cts = new CancellationTokenSource();
            Bot.StartReceiving(
                updateHandler: Handlers.HandleUpdateAsync,
                errorHandler: Handlers.HandleErrorAsync,
                receiverOptions: new ReceiverOptions()
                               {
                    AllowedUpdates = Array.Empty<UpdateType>(),
                               },
                cancellationToken: cts.Token);
            Console.WriteLine($"Start listening");
            Console.ReadLine();
            cts.Cancel();
        }
    }
}
