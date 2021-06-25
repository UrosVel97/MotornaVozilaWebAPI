using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VlasnikVoziloPrimljenoNaServisView
    {
        public int RegistarskiBroj { get; set; }
        public string Model { get; set; }
        public int GodinaProizvodnje { get; set; }
        public string OpisProblema { get; set; }
        public ZaposleniView PrimioVoziloNaServis { get; set; }

        public VlasnikVoziloPrimljenoNaServisView()
        {

        }

        public VlasnikVoziloPrimljenoNaServisView(VozilaPrimljenaNaServis v)
        {
            RegistarskiBroj = v.RegistarskiBroj;
            Model = v.ModelVozila;
            GodinaProizvodnje = v.GodinaProizvodnje;
            OpisProblema = v.OpisProblema;

            PrimioVoziloNaServis = new ZaposleniView(v.Zaposleni);
        }


    }
}
