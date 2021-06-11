using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class NezavisniEkonomista
    {
        public virtual int Jmbg { get;  set; }
        public virtual string Ime { get;  set; }
        public virtual string Prezime { get; set; }
        public virtual string Adresa { get; set; }
        public virtual IList<Salon> Saloni { get; set; }
        public virtual IList<TelefonNezavisniEkonomista> Telefoni { get; set; }

        public NezavisniEkonomista()
        {
            Saloni = new List<Salon>();
            Telefoni = new List<TelefonNezavisniEkonomista>();
        }

    }
}
