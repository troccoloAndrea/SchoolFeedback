using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFeedback.Models
{
    public class Servizio
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string  Descrizione { get; set; }

        public Servizio(int i, string t, string d)
        {
            Id = i;
            Titolo = t;
            Descrizione = d;
        }
    }
}
