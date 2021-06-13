using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VoziloKojeJeProdatoView
    {
        public int BrojSasije{ get; set; }
        public int BrojMotora { get; set; }
        public string TipGoriva { get; set; }
        public int Kubikaza { get; set; }
        public string Model { get; set; }
        public string FPutnickoVozilo{ get; set; }
        public string FTeretnoVozilo { get; set; }
        public int BrojPutnika { get; set; }
        public int Nosivost { get; set; }
        public string TipProstora { get; set; }
        public DateTime DatumKupovine { get; set; }

        public VoziloKojeJeProdatoView(VoziloKojeJeProdato v)
        {
            BrojSasije = v.BrojSasije;
            BrojMotora = v.BrojMotora;
            TipGoriva = v.TipGoriva;
            Kubikaza = v.Kubikaza;
            Model = v.ModelVozila;
            FPutnickoVozilo = v.FPutnickoVozilo;
            FTeretnoVozilo = v.FTeretnoVozilo;
            BrojPutnika = v.BrojPutnika;
            Nosivost = v.Nosivost;
            TipProstora = v.TipProstora;
            DatumKupovine = v.Kupovina.DatumKupovine;
        }

    }
}
