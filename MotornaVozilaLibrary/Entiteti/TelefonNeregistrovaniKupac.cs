using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class TelefonNeregistrovaniKupac
    {
        public virtual int Id{ get; protected set; }
        public virtual string BrojTelefona { get;  set; }
        public virtual NeregistrovaniKupac NeregistrovaniKupac { get;  set; }
        

    }
}
