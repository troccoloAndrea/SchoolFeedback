using SchoolFeedback.Models;
using SchoolFeedback.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFeedback.Services.Application
{
    public interface IApiServices
    {
        public List<Feedback> GetFeedbacks();

        public List<ServizioViewModel> GetServizi();

        public ServizioViewModel GetServizio(int id);

        public List<Feedback> GetServizioFeedbacks(int id);

        public bool InsertFeedback(Feedback feedback);
    }
}
