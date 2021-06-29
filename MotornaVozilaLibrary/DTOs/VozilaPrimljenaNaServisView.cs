using MotornaVozila.Entiteti;
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
        public ZaposleniView Zaposleni { get; set; }
        public int GodinaProizvodnje { get; set; }
        public VozilaPrimljenaNaServisVlasnikView Vlasnik { get; set; }

        public VozilaPrimljenaNaServisView()
        {

        }

        public VozilaPrimljenaNaServisView(VozilaPrimljenaNaServis v)
        {
            RegistarskiBroj = v.RegistarskiBroj;
            Model = v.ModelVozila;
            OpisProblema = v.OpisProblema;
            GodinaProizvodnje = v.GodinaProizvodnje;
            Zaposleni = new ZaposleniView(v.Zaposleni);
            Vlasnik = new VozilaPrimljenaNaServisVlasnikView(v.Vlasnik);
        }

    }
}
