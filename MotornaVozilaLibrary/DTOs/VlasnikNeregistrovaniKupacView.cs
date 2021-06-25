using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VlasnikNeregistrovaniKupacView :VlasnikView
    {
        public string Ime{ get; set; }
        public string Prezime { get; set; }
        public IList<string> Telefoni { get; set; }
        public VlasnikNeregistrovaniKupacView()
        {
            Telefoni = new List<string>();
        }

        public VlasnikNeregistrovaniKupacView(NeregistrovaniKupac n):base(n)
        {
            Telefoni = new List<string>();
            Ime = n.Ime;
            Prezime = n.Prezime;
            foreach(TelefonNeregistrovaniKupac t in n.Telefoni)
            {
                Telefoni.Add(t.BrojTelefona);
            }


        }

    }
}
