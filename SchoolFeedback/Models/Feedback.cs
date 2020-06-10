using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFeedback.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string  Commento { get; set; }
        public int Rating { get; set; }
        public DateTime Data_ins { get; set; }
        public int Utente { get; set; }
        public int Servizio { get; set; }

        public Feedback(int i, string t, string c, int r, DateTime d,int u, int s)
        {
            Id = i;
            Titolo = t;
            Commento = c;
            Rating = r;
            Data_ins = d;
            Utente = u;
            Servizio = s;
        }
        public Feedback() { }

    }
}
