using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace TelegramBot
{
    public class CommandFactory
    {
        private Message Answer { get; set; }

        private ITelegramBotClient Client { get; set; }

        public CommandFactory(ITelegramBotClient botClient, Message message)
        {
            Client = botClient;
            Answer = message;
        }

        public async Task NextStepAsync (Message message, Dictionary<long, User> usersDict)
        {
            if (usersDict.ContainsKey(message.Chat.Id))
            {
                IRepository rep = new Repository(Client, Answer);
                await rep.ExecutAsync(message, usersDict);
            }
            else
            {
                User user = new User();
                usersDict.Add(message.Chat.Id, user);
                IRepository rep = new Repository(Client, Answer);
                await rep.ExecutAsync(message, usersDict);
            }
            }
        }
    }