using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolFeedback.Services.Application;

namespace SchoolFeedback.Pages.Feedback
{
    public class InsertModel : PageModel
    {
        private readonly IApiServices _apiServices;
        public InsertModel(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        [BindProperty]
        public Models.Feedback Input { get; set; }
        public int idServizio = default;

        public void OnGet(int id)
        {
            idServizio = id;
            if (Input == null)
            {
                Input = new Models.Feedback();
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Input.Data_ins = DateTime.Now.Date;
            Input.Utente = 6;
            var ris = _apiServices.InsertFeedback(Input);
            if (ris is true)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}