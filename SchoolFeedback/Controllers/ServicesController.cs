using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolFeedback.Models;
using SchoolFeedback.Models.ViewModels;
using SchoolFeedback.Services.Infrastructure;

namespace SchoolFeedback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMySqlServices _db;
        public ServicesController(IMySqlServices mySqlServices)
        {
            _db = mySqlServices;
        }

        [HttpGet]
        public List<ServizioViewModel> Get()
        {
            FormattableString query = $"SELECT * FROM Servizio";
            DataSet dataSet = _db.Query(query);

            var dataTable = dataSet.Tables[0];
            var eleServizi = new List<ServizioViewModel>();
            foreach (DataRow row in dataTable.Rows)
            {
                FormattableString query2 = $"Select AVG(f.rating) as RatingMedio FROM feedback f INNER JOIN servizio s ON f.servizio = s.id WHERE s.id = {(int)row["Id"]}";
                DataSet dataSet2 = _db.Query(query2);
                var dataTable2 = dataSet2.Tables[0];
                var dataRow2 = dataTable2.Rows[0];
                ServizioViewModel servizio;
                if (dataRow2.ItemArray[0] == DBNull.Value)
                   servizio = new ServizioViewModel((int)row["Id"], (string)row["Titolo"], (string)row["Descrizione"], 0);
                else
                    servizio = new ServizioViewModel((int)row["Id"], (string)row["Titolo"], (string)row["Descrizione"],  (decimal)dataRow2["RatingMedio"]);
                eleServizi.Add(servizio);
            }
            return eleServizi;
        }

        [HttpGet("{id}")]
        public ServizioViewModel Get(int id)
        {
            FormattableString query = $"SELECT * FROM Servizio WHERE Id = {id}";
            DataSet dataSet = _db.Query(query);
            ServizioViewModel servizio;
            var dataTable = dataSet.Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                FormattableString query2 = $"Select AVG(f.rating) as RatingMedio FROM feedback f INNER JOIN servizio s ON f.servizio = s.id WHERE s.id = {id}";
                DataSet dataSet2 = _db.Query(query2);
                var dataTable2 = dataSet2.Tables[0];
                var dataRow2 = dataTable2.Rows[0];
                
                if (dataRow2.ItemArray[0] == DBNull.Value)
                    return servizio = new ServizioViewModel((int)row["Id"], (string)row["Titolo"], (string)row["Descrizione"], 0);
                else
                    return servizio = new ServizioViewModel((int)row["Id"], (string)row["Titolo"], (string)row["Descrizione"], (decimal)dataRow2["RatingMedio"]);
            }
            return null;
        }

    }
}

//SELECT s.Id, s.Titolo, s.Descrizione, AVG(f.Rating) as RatingMedio, f.Id, f.Titolo, f.Commento, f.Rating FROM Servizio s INNER JOIN Feedback f ON s.Id = f.Servizio WHERE Id = {id}