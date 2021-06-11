using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class NezavisniEkonomistaView
    {
        public virtual int Jmbg { get; set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual string Adresa { get; set; }
        public virtual IList<Salon> Saloni { get; set; }
        public virtual IList<TelefonNezavisniEkonomista> Telefoni { get; set; }

        public NezavisniEkonomistaView()
        {
            Saloni = new List<Salon>();
            Telefoni = new List<TelefonNezavisniEkonomista>();
        }

        public NezavisniEkonomistaView(NezavisniEkonomista n)
        {
            this.Jmbg = n.Jmbg;
            this.Ime = n.Ime;
            this.Prezime = n.Prezime;
            this.Adresa = n.Adresa;
        }
    }
}
