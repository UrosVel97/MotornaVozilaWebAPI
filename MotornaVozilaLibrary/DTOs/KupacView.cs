using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class KupacView
    {
        public int Id{ get; set; }
        public string LicnoIme { get; set; }
        public string Prezime { get; set; }

        public IList<string> Telefoni { get; set; }

        public KupacView()
        {
            Telefoni = new List<string>();
        }

        public KupacView(Kupac k)
        {
            Telefoni = new List<string>();
            Id = k.Id;
            LicnoIme = k.LicnoIme;
            Prezime = k.Prezime;

            foreach(TelefonKupac t in k.Telefoni)
            {
                Telefoni.Add(t.Telefon);
            }

        }
    }
}
