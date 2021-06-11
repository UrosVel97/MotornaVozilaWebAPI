using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class Zaposleni
    {
        public virtual int Jmbg { get; set; }

        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }

        public virtual int GodineRadnogStaza { get; set; }
        public virtual DateTime DatumRodjena { get; set; }
        public virtual DateTime DatumZaposlenja { get; set; }

        public virtual string StrucnaSprema { get; set; }

        public virtual string TipZaposlenog { get; set; }
        public virtual string FZaposleniZaStalno { get; set; }
        public virtual int Plata { get; set; }
        public virtual string FZaposleniPoUgovoru { get; set; }
        public virtual DateTime? DatumIstekaUgovora { get; set; }

        public virtual IList<VozilaPrimljenaNaServis> PrimioVoziloNaServis { get; set; }

        public Zaposleni()
        {
            PrimioVoziloNaServis = new List<VozilaPrimljenaNaServis>();
        }
    }

    public class RadnikEkonomskeStruke : Zaposleni
    {
        public RadnikEkonomskeStruke()
        {

        }
    }

    public class RadnikTehnickeStruke : Zaposleni
    {
        public virtual IList<UvezenoVozilo> UvezenaVozila { get; set; }
        public virtual IList<Specijalnost> Specijalnosti{ get; set; }
        public RadnikTehnickeStruke()
        {
            UvezenaVozila = new List<UvezenoVozilo>();
            Specijalnosti = new List<Specijalnost>();
        }
    }

    public class NekiDrugiZaposleni : Zaposleni
    {
        public NekiDrugiZaposleni()
        {

        }
    }

}
