using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Repository : IRepository
    {
        private ICommand Send { get; set; }

        private ITelegramBotClient Client { get; set; }

        private Message Message { get; set; }

        public Repository(ITelegramBotClient botClient, Message receivedMessage, ICommand send)
        {
            this.Client = botClient;
            this.Message = receivedMessage;
            this.Send = send;
        }

        public async Task ExecutAsync()
        {
            if (BotFunction.SaveLinksFlag)
            {
                await this.StoreLink(Client, Message, Send);
                return;
            }

            if (BotFunction.GetLinksFlag)
            {
                await this.GetLink(Client, Message, Send);
                return;
            }
            else
            {
                await(Message.Text!.Split(' ')[0] switch
                {

                    "/start" => this.Usage(Client, Message, Send),
                    "/store_link" => this.StoreLink(Client, Message, Send),
                    "/get_links" => this.GetLink(Client, Message, Send),

                });
            }
        }

        public async Task Usage(ITelegramBotClient botClient, Message message, ICommand send)
        {
            const string usage = "Функции:\n" +
                                 "/store_link   - записать ссылку\n" +
                                 "/get_links - получить ссылку\n";
            TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, usage);
            await send.ExecuteAsync(textInput);
            return;
        }

        public async Task StoreLink(ITelegramBotClient botClient, Message message, ICommand send)
        {
            string answer = await BotFunction.SaveLinks(message.Text!);
            TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, answer);
            await send.ExecuteAsync(textInput);
            return;
        }

        public async Task GetLink(ITelegramBotClient botClient, Message message, ICommand send)
        {
            string answer = BotFunction.GetLinks(message.Text!);
            TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, answer);
            await send.ExecuteAsync(textInput);
            return;
        }
    }
}
