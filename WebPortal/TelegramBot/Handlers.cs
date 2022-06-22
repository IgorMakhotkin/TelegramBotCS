using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class Handlers
    {
        Dictionary<long, UserData> usersDict = new Dictionary<long, UserData>();

        private async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message, ICommand send)
        {
            Console.WriteLine($"Полученый тип сообщения: {message.Type} {message.MessageId} {message.Chat.Id}");

            if (message.Type != MessageType.Text)
            {
                TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, "Введите текст");
                await send.ExecuteAsync(textInput);
                return;
            }

            if (message.Type == MessageType.Text)
            {
                CommandFactory factory = new CommandFactory(botClient, message);
                await factory.NextStepAsync(message, usersDict);
            }

            return;
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString(),
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }

        private Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ICommand send = new Command();
            var handler = update.Type switch
            {

                UpdateType.Message => this.BotOnMessageReceived(botClient, update.Message!, send),
                UpdateType.EditedMessage => this.BotOnMessageReceived(botClient, update.EditedMessage!, send),
                _ => this.UnknownUpdateHandlerAsync(botClient, update),
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await this.HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }
    }
}