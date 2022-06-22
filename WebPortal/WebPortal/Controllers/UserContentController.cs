using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAplication1.db;
using WebApplication1.db;

namespace WebApplication1.Controllers
{
    public class UserContentController : Controller
    {

        public async Task<IActionResult> Index()
        {
            DataBaseContext context = new DataBaseContext();

            long id = long.Parse(User.Identity.Name);
              return context.Links != null ?
                         View(context.Links.Where(i => i.UserId == id)) :
                         Problem("Entity set 'DataBaseContext.Link'  is null.");
        }

    
    }
}
