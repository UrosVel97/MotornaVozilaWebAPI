
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class TelefonNezavisniEkonomista
    {
        public virtual int Id { get; protected set; }
        public virtual string BrojTelefona { get;  set; }
        public virtual NezavisniEkonomista NezavisniEkonomista { get;  set; }

    }
}
