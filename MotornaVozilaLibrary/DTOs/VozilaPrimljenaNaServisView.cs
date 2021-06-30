using MotornaVozila.Entiteti;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VozilaPrimljenaNaServisView
    {
        public int RegistarskiBroj { get; set; }
        public string Model { get; set; }
        public string OpisProblema { get; set; }
       
        public int GodinaProizvodnje { get; set; }
        
        public virtual DateTime DatumPrijema { get; set; }
        public virtual DateTime DatumZavrsetkaRadova { get; set; }

        public ZaposleniView Zaposleni { get; set; }
        public VozilaPrimljenaNaServisVlasnikView Vlasnik { get; set; }

        public VozilaPrimljenaNaServisView()
        {

        }

        public VozilaPrimljenaNaServisView(VozilaPrimljenaNaServis v, ISession s)
        {
            RegistarskiBroj = v.RegistarskiBroj;
            Model = v.ModelVozila;
            OpisProblema = v.OpisProblema;
            GodinaProizvodnje = v.GodinaProizvodnje;
            Zaposleni = new ZaposleniView(v.Zaposleni);
            DatumPrijema=v.DatumPrijema;
            DatumZavrsetkaRadova = v.DatumZavrsetkaRadova;

            NeregistrovaniKupac n = s.Get<NeregistrovaniKupac>(v.Vlasnik.Id);
            RegistrovaniKupac r = s.Get<RegistrovaniKupac>(v.Vlasnik.Id);

            if (n != null)
            {
                Vlasnik = new VozilaPrimljenaNaServisVlasnikView(v.Vlasnik, s,true);
            }
            else if(r!=null)
            {
                Vlasnik = new VozilaPrimljenaNaServisVlasnikView(v.Vlasnik, s, false);
            }
                
            

            
        }

    }
}
