using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VozilaPrimljenaNaServisVlasnikView
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public IList<string> Telefoni { get; set; }

        public VozilaPrimljenaNaServisVlasnikView()
        {
            Telefoni = new List<string>();
        }

        public VozilaPrimljenaNaServisVlasnikView(Vlasnik v)
        {
            Telefoni = new List<string>();
            if(v.GetType()==typeof(NeregistrovaniKupac))
            {
                NeregistrovaniKupac n = (NeregistrovaniKupac)v;
                Ime = n.Ime;
                Prezime = n.Prezime;

                foreach(TelefonNeregistrovaniKupac t in n.Telefoni)
                {
                    Telefoni.Add(t.BrojTelefona);
                }
            }
            else if (v.GetType() == typeof(RegistrovaniKupac))
            {
                RegistrovaniKupac n = (RegistrovaniKupac)v;
                Ime =n.Kupac.LicnoIme;
                Prezime = n.Kupac.Prezime;

                foreach (TelefonKupac t in n.Kupac.Telefoni)
                {
                    Telefoni.Add(t.Telefon);
                }
            }

        }
    }
}
