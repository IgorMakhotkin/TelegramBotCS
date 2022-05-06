
namespace TelegramBot
{
    //Интерфейс хранилиша 
    public interface IStorage
    {
        public abstract string ReturnLinks(string key);

        public abstract Task<bool> AddLinksToStorage(string key, string value);

    }
}
