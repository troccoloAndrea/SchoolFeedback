using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolFeedback.Models;
using SchoolFeedback.Models.ViewModels;
using SchoolFeedback.Services.Application;

namespace SchoolFeedback.Pages
{
    public class DetailModel : PageModel
    {
        private readonly IApiServices _apiServices;
        public DetailModel(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        public ServizioViewModel ServizioViewModel = default(ServizioViewModel);
        public List<SchoolFeedback.Models.Feedback> eleFeedback = new List<SchoolFeedback.Models.Feedback>();

        public void OnGet(int id)
        {
            ServizioViewModel = _apiServices.GetServizio(id);
            eleFeedback = _apiServices.GetServizioFeedbacks(id);
        }
    }
}