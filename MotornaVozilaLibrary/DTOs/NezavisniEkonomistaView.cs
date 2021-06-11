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
        public virtual IList<NESalon> Saloni { get; set; }
        public virtual IList<string> Telefoni { get; set; }

        public NezavisniEkonomistaView()
        {
            Saloni = new List<NESalon>();
            Telefoni = new List<string>();
        }

        public NezavisniEkonomistaView(NezavisniEkonomista n)
        {
            this.Jmbg = n.Jmbg;
            this.Ime = n.Ime;
            this.Prezime = n.Prezime;
            this.Adresa = n.Adresa;
            Saloni = new List<NESalon>();
            Telefoni = new List<string>();

            foreach (Salon s in n.Saloni)
            {
                this.Saloni.Add(new NESalon()
                {
                    Id = s.Id,
                    Grad = s.Grad,
                    Adresa = s.Adresa,
                    SefSalona = s.SefSalona,
                    SefServisa = s.SefServisa
                });
            }
            foreach (TelefonNezavisniEkonomista t in n.Telefoni)
            {
                this.Telefoni.Add(t.BrojTelefona);
            }
        }
    }
}
