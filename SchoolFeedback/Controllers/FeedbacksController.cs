using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolFeedback.Models;
using SchoolFeedback.Services.Application;
using SchoolFeedback.Services.Infrastructure;

namespace SchoolFeedback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IMySqlServices _db;
        public FeedbacksController(IMySqlServices mySqlServices)
        {
            _db = mySqlServices;
        }

        [HttpGet]
        public List<Feedback> Get()
        {
            FormattableString query = $"SELECT * FROM Feedback";
            DataSet dataSet = _db.Query(query);

            var dataTable = dataSet.Tables[0];
            var eleFeedback = new List<Feedback>();
            foreach (DataRow row in dataTable.Rows)
            {
                var feedback = new Feedback((int)row["Id"], (string)row["Titolo"], (string)row["Commento"], 
                    (int)row["Rating"], (DateTime)row["Data_ins"], (int)row["Utente"], (int)row["Servizio"]);
                eleFeedback.Add(feedback);
            }
            return eleFeedback;
        }


        [HttpGet("servicefeedbacks")]
        public List<Feedback> Get(int id)
        {
            FormattableString query = $"SELECT f.Id, f.Titolo, f.Commento, f.Rating, f.Data_ins, f.Utente, f.Servizio FROM Servizio s INNER JOIN Feedback f ON s.Id = f.Servizio WHERE s.Id = {id}";
            DataSet dataSet = _db.Query(query);

            var dataTable = dataSet.Tables[0];
            var eleFeedback = new List<Feedback>();
            foreach (DataRow row in dataTable.Rows)
            {
                var feedback = new Feedback((int)row["Id"], (string)row["Titolo"], (string)row["Commento"], 
                    (int)row["Rating"], (DateTime)row["Data_ins"],(int)row["Utente"], (int)row["Servizio"]);
                eleFeedback.Add(feedback);
            }
            return eleFeedback;
        }

        [HttpPost]
        public HttpStatusCode Post(Feedback feedback)
        {
            FormattableString query = $"INSERT INTO feedback VALUES (null, {feedback.Titolo}, {feedback.Commento}, {feedback.Rating}, {feedback.Data_ins}, {feedback.Utente}, {feedback.Servizio})";
            _ = _db.Query(query);
            return HttpStatusCode.Created;
        }
    }
}