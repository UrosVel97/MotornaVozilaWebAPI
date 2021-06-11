using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class TipRadova
    {
        public virtual string Tip_Radova { get;  set; }
        public virtual IList<Salon> Servis { get; set; }

        public TipRadova()
        {
            Servis = new List<Salon>();
        }

    }
}
