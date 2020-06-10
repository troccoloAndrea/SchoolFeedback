using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFeedback.Models.ViewModels
{
    public class ServizioViewModel
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public decimal RatingMedio { get; set; }

        public ServizioViewModel(int i, string t, string d, decimal rm)
        {
            Id = i;
            Titolo = t;
            Descrizione = d;
            RatingMedio = rm;
        }
    }
}
