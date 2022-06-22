﻿
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Command : ICommand
    {
        // Реализация обобщенного метода
        public async Task ExecuteAsync<TCommandInput>(TCommandInput input)
            where TCommandInput : TelegramCommandInput
        {
            TelegramCommandInput textInput = input as TelegramCommandInput;
            await textInput.sendMessage();
        }
    }
}