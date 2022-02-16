using System.Collections.Generic;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntityFramework.ViewModels
{
    //Cái ViewModels này Models cho thằng Views, mục đích là bây h ở ngoài Views sẽ yêu cầu 2 dữ liệu bao gồm
    //dữ liệu cho thằng Product và một cái list chứ cái category ở trong database để sử dụng cho thằng dropdown
    //nó xổ xuống nên phải tạo một cái ViewModels này để mang cùng lúc hai cái dữ liệu này ra ngoài.
    public class ProductUpSertViewModels
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}