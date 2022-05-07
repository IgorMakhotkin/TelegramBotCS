using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class Handlers
    {
        static async Task Usage(ITelegramBotClient botClient, Message message)
        {
            const string usage = "Функции:\n" +
                                 "/store_link   - записать ссылку\n" +
                                 "/get_links - получить ссылку\n";

            await ICommand.SendMessage(usage, message, botClient);
            return;
        }

        static async Task StoreLink(ITelegramBotClient botClient, Message message)
        {
            string answer = await BotFunction.SaveLinks(message.Text!);
            await ICommand.SendMessage(answer,message,botClient);
            return;
        }
        static async Task GetLink(ITelegramBotClient botClient,Message message)
        {
            string answer = await BotFunction.GetLinks(message.Text!);
            await ICommand.SendMessage(answer,message,botClient);
            return;
        }
        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            Console.WriteLine($"Полученый тип сообщения: {message.Type}");
            if (message.Type != MessageType.Text)
            {
                await ICommand.SendMessage("Введите текст", message, botClient);
                return;
            }
            if (BotFunction.SaveLinksFlag)
            {
                await StoreLink(botClient,message);
                return;
            }

            if (BotFunction.GetLinksFlag)
            {
                await GetLink(botClient, message);
                return;
            }
            else
            {

                var action = message.Text!.Split(' ')[0] switch
                {

                    "/start" => Usage(botClient, message),
                    "/store_link" => StoreLink(botClient, message),
                    "/get_links" => GetLink(botClient, message)

                };
            }
            
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
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
            var handler = update.Type switch
            {

                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
                UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage!),
                _ => UnknownUpdateHandlerAsync(botClient, update)
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
