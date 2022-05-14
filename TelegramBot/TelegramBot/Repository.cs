using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Repository : IRepository
    {
        private ICommand Send { get; set; }

        private ITelegramBotClient Client { get; set; }

        private Message Message { get; set; }

        public Repository(ITelegramBotClient botClient, Message receivedMessage)
        {
            this.Client = botClient;
            this.Message = receivedMessage;
            ICommand send = new Command();
            this.Send = send;
        }

        public async Task ExecutAsync(Message message, Dictionary<long, User> usersDict)
        {
            User user = new User();
            user = usersDict[message.Chat.Id];

            if (user.SaveLinksFlag)
            {
                await this.StoreLink(Client, Message, Send, usersDict);
                return;
            }

            if (user.GetLinksFlag)
            {
                await this.GetLink(Client, Message, Send, usersDict);
                return;
            }
            else
            {
                await(Message.Text!.Split(' ')[0] switch
                {

                    "/start" => this.Usage(Client, Message, Send),
                    "/store_link" => this.StoreLink(Client, Message, Send, usersDict),
                    "/get_links" => this.GetLink(Client, Message, Send, usersDict),

                });
            }
        }

        public async Task Usage(ITelegramBotClient botClient, Message message, ICommand send)
        {
            StartCommand start = new StartCommand();
            TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, start.StartUse());
            await send.ExecuteAsync(textInput);
            return;
        }

        public async Task StoreLink(ITelegramBotClient botClient, Message message, ICommand send, Dictionary<long, User> usersDict)
        {
            SaveLinksCommand save = new SaveLinksCommand();
            string answer = await save.SaveLinks(message, usersDict);
            TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, answer);
            await send.ExecuteAsync(textInput);
            return;
        }

        public async Task GetLink(ITelegramBotClient botClient, Message message, ICommand send, Dictionary<long, User> usersDict)
        {
            GetLinksCommand get = new GetLinksCommand();
            string answer = await get.GetLinks(message, usersDict);
            TelegramCommandInput textInput = new TelegramCommandInput(botClient, message, answer);
            await send.ExecuteAsync(textInput);
            return;
        }
    }
}
