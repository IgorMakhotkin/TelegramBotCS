using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortal.db;
using WebPortal;
using WebPortal.Mapping;
using AutoMapper.QueryableExtensions;

namespace WebPortal.Controllers
{
    public class UserContentController : Controller
    {
        private readonly IMapper _mapper;

        public UserContentController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            DataBaseContext context = new DataBaseContext();

            long id = long.Parse(User.Identity.Name);
            var result = context.Links.Where(i => i.UserId == id);
            var ViewLinks = _mapper.ProjectTo<LinkDto>(result);
            return context.Links != null ?
                         View(ViewLinks) :
                         Problem("Entity set 'DataBaseContext.Link'  is null.");
        }

    
    }
}
