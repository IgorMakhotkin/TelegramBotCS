﻿using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace TelegramBot
{
    public class Startup
    {
        private static TelegramBotClient? Bot;

        public  async Task Start()
        {
            Bot = new TelegramBotClient(Configuration.BotToken);

            User me = await Bot.GetMeAsync();
            Console.Title = me.Username ?? "Мой бот";
            using var cts = new CancellationTokenSource();
            //
            Bot.StartReceiving(updateHandler: Handlers.HandleUpdateAsync,
                               errorHandler: Handlers.HandleErrorAsync,
                               receiverOptions: new ReceiverOptions()
                               {
                                   AllowedUpdates = Array.Empty<UpdateType>()
                               },
                               cancellationToken: cts.Token);
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();
        }
    }
}