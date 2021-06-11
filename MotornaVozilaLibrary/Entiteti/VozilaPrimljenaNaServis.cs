using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class VozilaPrimljenaNaServis
    {
        public virtual int RegistarskiBroj { get; protected set; }
        public virtual string ModelVozila { get; set; }
        public virtual int GodinaProizvodnje { get; set; }
        public virtual string OpisProblema { get; set; }
        public virtual DateTime DatumPrijema { get; set; }
        public virtual DateTime DatumZavrsetkaRadova { get; set; }
        public virtual Zaposleni Zaposleni { get; set; }
        public virtual Vlasnik Vlasnik { get; set; }
        public VozilaPrimljenaNaServis()
        {

        }
    }
}
