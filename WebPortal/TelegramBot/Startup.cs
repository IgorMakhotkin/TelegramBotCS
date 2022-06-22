namespace TelegramBot
{
    public class Startup
    {
        public void Start()
        {
            IChat chat = new TelegramChatAPI();
        }
    }
}
