using System.Collections.Generic;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntityFramework.ViewModels
{
    public class OderViewModels
    {
        public Oder Oder { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}