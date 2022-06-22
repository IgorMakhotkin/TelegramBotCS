
namespace TelegramBot
{
    //Интерфейс хранилиша 
    public interface IStorage
    {
        public string ReturnLinks(string key);

        public Task<bool> AddLinksToStorage(string key, string value, long chatId);

    }
}
