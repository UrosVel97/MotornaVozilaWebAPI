using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class ZaposleniView
    {
        public int Jmbg{ get; set; }
        public string Ime{ get; set; }
        public string Prezime{ get; set; }
        public int GodineRadnogStaza { get; set; }
        public DateTime DatumRodjenja{ get; set; }
        public DateTime DatumZaposlenja{ get; set; }
        public string StrucnaSprema { get; set; }
        public string FZaposlenZaStalno { get; set; }
        public string FZaposlenPoUgovoru { get; set; }
        public int Plata{ get; set; }
        public DateTime? DatumIstekaUgovora{ get; set; }


        public ZaposleniView()
        {

        }
        public ZaposleniView(Zaposleni z)
        {
            Jmbg = z.Jmbg;
            Ime = z.Ime;
            Prezime = z.Prezime;
            GodineRadnogStaza = z.GodineRadnogStaza;
            DatumRodjenja = z.DatumRodjena;
            DatumZaposlenja = z.DatumZaposlenja;
            StrucnaSprema = z.StrucnaSprema;
            FZaposlenZaStalno = z.FZaposleniZaStalno;
            FZaposlenPoUgovoru = FZaposlenPoUgovoru;
            Plata = z.Plata;
            DatumIstekaUgovora = z.DatumIstekaUgovora;
        }
    }
}
