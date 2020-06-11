using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SchoolFeedback.Models.ViewModels;
using SchoolFeedback.Services.Application;

namespace SchoolFeedback.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IApiServices _apiServices;
        public IndexModel(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        public List<ServizioViewModel> eleServizi = new List<ServizioViewModel>();

        public void OnGet()
        {
            eleServizi = _apiServices.GetServizi();
        }

    }
}
