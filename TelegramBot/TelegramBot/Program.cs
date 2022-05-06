using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot;

public static class Program
{
    

    static async Task Main()
    {
        Startup s = new Startup();
        await s.Start();
    }
}
