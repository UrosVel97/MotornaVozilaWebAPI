using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class Salon
    {
        public virtual int Id { get; protected set; }
        public virtual string Grad { get; set; }

        public virtual string Adresa { get; set; }

        public virtual string StepenOpremljenostiServisa { get; set; }
        public virtual string SefSalona { get; set; }

        public virtual string SefServisa { get; set; }

        public virtual string FServis { get; set; }

        public virtual IList<TipRadova> TipoviRadova { get; set; }

        public virtual IList<NezavisniEkonomista> NezavisniEkonomisti { get; set; }

        public virtual IList<VoziloKojeNijeProdato> VozilaKojaNisuProdata { get; set; }

        public virtual IList<Kupovina> Kupovine { get; set; }

        public Salon()
        {
            TipoviRadova = new List<TipRadova>();
            NezavisniEkonomisti = new List<NezavisniEkonomista>();
            VozilaKojaNisuProdata = new List<VoziloKojeNijeProdato>();
            Kupovine = new List<Kupovina>();
        }
    }
}
