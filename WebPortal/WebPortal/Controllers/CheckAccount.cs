using WebPortal.ViewModels;
using WebPortal.db;
namespace WebPortal.Controllers

{
    public class CheckAccount
    {
        public bool IsAuthenticated (LoginViewModel model)
        {
            DataBaseContext dataBase = new DataBaseContext();
            long id = long.Parse(model.Username);
            UserData result = dataBase.Users.First(i => i.UserId == id);

            if (result != null)
            {
                if (result.UserId == id && result.Password == model.Password)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
