using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace WebPortal.Mapping
{
 
    public class LinkDto
    {
        
        public string? Url { get; set; }

        public string? Category { get; set; }
    }
}
