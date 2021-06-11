using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class Kupovina
    {
        public virtual int Id { get; protected set; }
        public virtual DateTime DatumKupovine { get; set; }

        public virtual Salon IdSalona { get; set; }

        public virtual Kupac IdKupca { get; set; }

        public virtual IList<VoziloKojeJeProdato> ProdataVozila { get; set; }

        public Kupovina()
        {
            ProdataVozila = new List<VoziloKojeJeProdato>();
        }

    }
}
