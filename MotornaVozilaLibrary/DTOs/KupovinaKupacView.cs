using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class KupovinaKupacView
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public IList<string> BrojeviTelefona { get; set; }

        public KupovinaKupacView()
        {
            BrojeviTelefona = new List<string>();
        }

        public KupovinaKupacView(Kupac k)
        {
            BrojeviTelefona = new List<string>();
            Ime = k.LicnoIme;
            Prezime = k.Prezime;
            foreach(TelefonKupac t in k.Telefoni)
            {
                BrojeviTelefona.Add(t.Telefon);
            }
        }


    }
}
