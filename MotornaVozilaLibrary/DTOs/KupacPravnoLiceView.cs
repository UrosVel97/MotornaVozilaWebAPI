using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class KupacPravnoLiceView : KupacView
    {
        public int Pib { get; set; }

        public KupacPravnoLiceView()
        {

        }
        public KupacPravnoLiceView(PravnoLice p) : base(p)
        {
            Pib = p.Pib;

        }
    }
}
