using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class Boja
    {
        public virtual int Id{ get; protected set; }
        public virtual string BojaVozila { get; set; }
        public virtual UvezenoVozilo UvezenoVozilo { get;  set; }
    }
}
