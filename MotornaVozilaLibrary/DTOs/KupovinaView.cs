using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class KupovinaView
    {
        public DateTime DatumKupovine{ get; set; }
        public NESalon KupljenoUSalonu{ get; set; }
        public KupovinaKupacView Kupac { get; set; }
        public IList<VoziloKojeJeProdatoView> Vozila { get; set; }

        public KupovinaView(Kupovina k)
        {
            Vozila = new List<VoziloKojeJeProdatoView>();
            KupljenoUSalonu = new NESalon(k.IdSalona);
            Kupac = new KupovinaKupacView(k.IdKupca);
            DatumKupovine = k.DatumKupovine;

            foreach(VoziloKojeJeProdato v in k.ProdataVozila)
            {
                Vozila.Add(new VoziloKojeJeProdatoView(v));
            }

        }


    }
}
