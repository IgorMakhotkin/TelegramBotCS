namespace TelegramBot
{
    {
        public string ReturnLinks(string key)
        {

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

        {
        }
    }
}
