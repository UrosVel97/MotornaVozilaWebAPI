using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VlasnikRegistrovaniKupacView : VlasnikView
    {
        public KupacView RegistrovaniKupac { get; set; }

        public VlasnikRegistrovaniKupacView()
        {

        }

        public VlasnikRegistrovaniKupacView(RegistrovaniKupac r):base(r)
        {
            RegistrovaniKupac = new KupacView(r.Kupac);
        }
    }
}
