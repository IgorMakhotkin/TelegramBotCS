using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class Handlers
    {

        static async Task Usage(ITelegramBotClient botClient, Message message, ICommand send)
        {
            const string usage = "Функции:\n" +
                                 "/store_link   - записать ссылку\n" +
                                 "/get_links - получить ссылку\n";
            TelegramCommandInput textInput = new TelegramCommandInput(botClient,message,usage);
            await send.ExecuteAsync(textInput);
            return;
        }

        static async Task StoreLink(ITelegramBotClient botClient, Message message, ICommand send)
        {
            string answer = await BotFunction.SaveLinks(message.Text!);
            TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, answer);
            await send.ExecuteAsync(textInput);
            return;
        }

        static async Task GetLink(ITelegramBotClient botClient, Message message, ICommand send)
        {
            string answer = BotFunction.GetLinks(message.Text!);
            TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, answer);
            await send.ExecuteAsync(textInput);
            return;
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message, ICommand send)
        {
            Console.WriteLine($"Полученый тип сообщения: {message.Type}");
            if (message.Type != MessageType.Text)
            {
                TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, "Введите текст");
                await send.ExecuteAsync(textInput);
                return;
            }

            if (BotFunction.SaveLinksFlag)
            {
                await StoreLink(botClient, message, send);
                return;
            }

            if (BotFunction.GetLinksFlag)
            {
                await GetLink(botClient, message, send);
                return;
            }
            else
            {
                var action = message.Text!.Split(' ')[0] switch
                {

                    "/start" => Usage(botClient, message, send),
                    "/store_link" => StoreLink(botClient, message, send),
                    "/get_links" => GetLink(botClient, message, send),

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
            ICommand send = new Command();
          //  TelegramCommandInput textInput = new TelegramCommandInput();

            var handler = update.Type switch
            {

                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!, send),
                UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage!, send),
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