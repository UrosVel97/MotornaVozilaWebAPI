using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class SalonView
    {
        public int Id { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public string SefSalona { get; set; }
        public string SefServisa { get; set; }
        public string FServis { get; set; }
        public string StepenOpremljenostiServisa { get; set; }

        public IList<string> TipoviRadova { get; set; }
        public IList<SalonNezavisniEkonomistaView> AngozovaniEkonomisti { get; set; }
        public IList<SalonKupovinaView> Kupovine { get; set; }
        public IList<SalonVoziloKojeNijeProdatoView> VozilaKojaNisuProdata { get; set; }

        public SalonView()
        {
            AngozovaniEkonomisti = new List<SalonNezavisniEkonomistaView>();
            TipoviRadova = new List<string>();
            Kupovine = new List<SalonKupovinaView>();
            VozilaKojaNisuProdata = new List<SalonVoziloKojeNijeProdatoView>();
        }


        public SalonView(Salon sal)
        {
            AngozovaniEkonomisti = new List<SalonNezavisniEkonomistaView>();
            TipoviRadova = new List<string>();
            Kupovine = new List<SalonKupovinaView>();
            VozilaKojaNisuProdata = new List<SalonVoziloKojeNijeProdatoView>();
            this.Id = sal.Id;
            this.Grad = sal.Grad;
            this.Adresa = sal.Adresa;
            this.SefSalona = sal.SefSalona;
            this.SefServisa = sal.SefServisa;
            this.FServis = sal.FServis;
            this.StepenOpremljenostiServisa = sal.StepenOpremljenostiServisa;

            foreach(NezavisniEkonomista n in sal.NezavisniEkonomisti)
            {
                AngozovaniEkonomisti.Add(new SalonNezavisniEkonomistaView()
                {
                    Jmbg = n.Jmbg,
                    Ime = n.Ime,
                    Prezime = n.Prezime,
                    Adresa = n.Adresa
                });
            }

            foreach(TipRadova t in sal.TipoviRadova)
            {
                TipoviRadova.Add(t.Tip_Radova);
            }

            foreach(Kupovina k in sal.Kupovine)
            {
                Kupovine.Add(new SalonKupovinaView()
                {
                    DatumKupovine = k.DatumKupovine,
                    Id = k.Id
                });
            }

            foreach(VoziloKojeNijeProdato v in sal.VozilaKojaNisuProdata)
            {
                VozilaKojaNisuProdata.Add(new SalonVoziloKojeNijeProdatoView()
                {
                    BrojSasije = v.BrojSasije,
                    BrojMotora = v.BrojMotora,
                    TipGoriva = v.TipGoriva,
                    Kubikaza = v.Kubikaza,
                    Model = v.ModelVozila,
                    FPutnickoVozilo = v.FPutnickoVozilo,
                    FTeretnoVozilo = v.FTeretnoVozilo,
                    BrojPutnika = v.BrojPutnika,
                    Nosivost = v.Nosivost,
                    TipProstora = v.TipProstora

                });
            }
        }


    }
}
