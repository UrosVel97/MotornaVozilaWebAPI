using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class TelefonKupac
    {
        public virtual int Id { get; protected set; }

        public virtual string Telefon { get; set; }
        public virtual Kupac Kupac { get; set; }

        public TelefonKupac()
        {

        }

    }
}
