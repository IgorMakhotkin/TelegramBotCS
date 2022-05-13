using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class Handlers
    {
         private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message, ICommand send)
        {
            IRepository rep = new Repository(botClient, message, send);

            Console.WriteLine($"Полученый тип сообщения: {message.Type}");
            if (message.Type != MessageType.Text)
            {
                TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, "Введите текст");
                await send.ExecuteAsync(textInput);
                return;
            }

            if (message.Type == MessageType.Text)
            {
               await rep.ExecutAsync();
            }
        }

         public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString(),
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }

         private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

         public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ICommand send = new Command();

            var handler = update.Type switch
            {

                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!, send),
                UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage!, send),
                _ => UnknownUpdateHandlerAsync(botClient, update),
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }
    }
}