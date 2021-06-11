using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class Vlasnik
    {
        public virtual int Id { get; protected set; }

        public virtual string TipVlasnika { get; set; }

        public virtual IList<VozilaPrimljenaNaServis> JePoslaoVoziloNaServis { get; set; }

        public Vlasnik()
        {
            JePoslaoVoziloNaServis = new List<VozilaPrimljenaNaServis>();
        }
    }

    public class NeregistrovaniKupac : Vlasnik
    {
        public virtual string Ime { get; set; }

        public virtual string Prezime { get; set; }
        public virtual IList<TelefonNeregistrovaniKupac> Telefoni { get; set; }
        public NeregistrovaniKupac()
        {
            Telefoni = new List<TelefonNeregistrovaniKupac>();
        }
    }

    public class RegistrovaniKupac : Vlasnik
    {
        public virtual Kupac Kupac { get; set; }
        
        public RegistrovaniKupac()
        {
            
        }
    }
}
