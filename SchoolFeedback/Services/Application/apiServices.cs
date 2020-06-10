using Microsoft.Extensions.Logging;
using SchoolFeedback.Models;
using SchoolFeedback.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SchoolFeedback.Models.ViewModels;

namespace SchoolFeedback.Services.Application
{
    public class apiServices : IApiServices
    {
        private readonly ILogger<apiServices> _logger;
        public apiServices(ILogger<apiServices> logger)
        {
            _logger = logger;
        }


        public List<Feedback> GetFeedbacks()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/api/feedbacks");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonResponse = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Feedback>>(jsonResponse);
            }
        }

        public List<ServizioViewModel> GetServizi()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/api/services");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonResponse = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<ServizioViewModel>>(jsonResponse);
            }
        }

        public ServizioViewModel GetServizio(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/api/services/" + id);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonResponse = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ServizioViewModel>(jsonResponse);
            }
        }

        public List<Feedback> GetServizioFeedbacks(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/api/feedbacks/servicefeedbacks?id=" + id);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonResponse = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Feedback>>(jsonResponse);
            }
        }

        public bool InsertFeedback(Feedback feedback)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/api/feedbacks");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter body = new StreamWriter(request.GetRequestStream()))
            {
                body.Write(JsonConvert.SerializeObject(feedback));
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonResponse = reader.ReadToEnd();
                if (jsonResponse == "201")
                    return true;
                else
                    return false;
            }
        }
    }
}
