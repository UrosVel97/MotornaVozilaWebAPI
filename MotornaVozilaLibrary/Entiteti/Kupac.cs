using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class Kupac
    {
        public virtual int Id { get; protected set; }

        public virtual string LicnoIme { get; set; }

        public virtual string Prezime { get; set; }

        public virtual string TipKupca { get; set; }

        public virtual IList<Kupovina> Kupovine { get; set; }

        public virtual IList<RegistrovaniKupac> RegistrovaniKupci { get; set; }

        public virtual IList<TelefonKupac> Telefoni{ get; set; }



        public Kupac()
        {
            Kupovine = new List<Kupovina>();
            RegistrovaniKupci = new List<RegistrovaniKupac>();
            Telefoni = new List<TelefonKupac>();
        }

    }

    public class FizickoLice : Kupac
    {
        public virtual int Jmbg { get; set; }
        public FizickoLice()
        {

        }
    }

    public class PravnoLice : Kupac
    {
        public virtual int Pib { get; set; }

        public PravnoLice()
        {

        }
    }
}
