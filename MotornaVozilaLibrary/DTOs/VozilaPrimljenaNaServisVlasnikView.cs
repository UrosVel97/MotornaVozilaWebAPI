using MotornaVozila.Entiteti;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VozilaPrimljenaNaServisVlasnikView
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public IList<string> Telefoni { get; set; }

        public VozilaPrimljenaNaServisVlasnikView()
        {
            Telefoni = new List<string>();
        }

        public VozilaPrimljenaNaServisVlasnikView(Vlasnik v1, ISession s,bool t1)
        {

            Telefoni = new List<string>();
            if(t1)
            {
                NeregistrovaniKupac n = s.Load<NeregistrovaniKupac>(v1.Id);
                Id = n.Id;
                Ime = n.Ime;
                Prezime = n.Prezime;

                foreach(TelefonNeregistrovaniKupac t in n.Telefoni)
                {
                    Telefoni.Add(t.BrojTelefona);
                }
            }
            else
            {
                RegistrovaniKupac n = s.Load<RegistrovaniKupac>(v1.Id);
                Ime =n.Kupac.LicnoIme;
                Prezime = n.Kupac.Prezime;
                Id = n.Id;
                foreach (TelefonKupac t in n.Kupac.Telefoni)
                {
                    Telefoni.Add(t.Telefon);
                }
            }

        }
    }
}
