namespace TelegramBot
{
    public class Storage : IStorage
    {
        public string ReturnLinks(string key)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                if (key.ToLower() == "все")
                {
                    var allLinks = db.Links.ToList();
                    string link = null;

                    foreach (var Link in allLinks)
                    {
                        link += " \n";
                        link += Link.Url;
                        link += " \n";
                    }

                    return link;
                }
                else
                {
                    var links = db.Links.Where(k => k.Category == key);
                    string link = null;

                    foreach (var url in links)
                    {
                        link += " \n";
                        link += url.Url;
                    }

                    return link == null ? " Запись не найдена" : link;
                }
            }
        }

        public async Task<bool> AddLinksToStorage(string key, string value)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                Link SaveLink = new Link()
                { 
                    Url = value,
                    Category = key,
                };
                db.Links.Add(SaveLink);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
