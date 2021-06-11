using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class UvezenoVozilo
    {
        public virtual int BrojSasije { get; set; }
        public virtual DateTime DatumUvoza { get; set; }
        public virtual int BrojMotora { get; set; }
        public virtual string TipGoriva { get; set; }
        public virtual int Kubikaza { get; set; }
        public virtual string ModelVozila { get; set; }
        public virtual string FPutnickoVozilo { get; set; }
        public virtual int BrojPutnika { get; set; }
        public virtual string FTeretnoVozilo { get; set; }
        public virtual int Nosivost { get; set; }
        public virtual string TipProstora { get; set; }
        public virtual string TipVozila { get; set; }

        public virtual RadnikTehnickeStruke RadnikTehnStruke { get; set; }

        public virtual IList<Boja> Boje{ get; set; }
        public UvezenoVozilo()
        {
            Boje = new List<Boja>();
        }

    }

    public class VoziloKojeJeProdato :UvezenoVozilo
    {
        public virtual Kupovina Kupovina { get; set; }
        public VoziloKojeJeProdato()
        {

        }
    }

    public class VoziloKojeNijeProdato : UvezenoVozilo
    {
        public virtual Salon IzlozenUSalonu { get; set; }
        public VoziloKojeNijeProdato()
        {
            
        }

    }
}

