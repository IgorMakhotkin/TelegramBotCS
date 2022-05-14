namespace TelegramBot
{
    public class StartCommand
    {
        const string usage = "Функции:\n" +
                                 "/store_link   - записать ссылку\n" +
                                 "/get_links - получить ссылку\n";

        public string StartUse()
        {
            return usage;
        }
    }
}
