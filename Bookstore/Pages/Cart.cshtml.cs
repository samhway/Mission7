using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterProject.Infrastructure;
using Bookstore.Models;
using Bookstore.Infrastructure;

namespace Bookstore.Pages
{
    public class CartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        public CartModel(IBookstoreRepository temp)
        {
            repo = temp;
        }

        public Basket basket {get; set;}
        public string ReturnUrl;

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";

            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();

            basket.AddItem(b, 1);

            HttpContext.Session.SetJson("basket", basket);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
