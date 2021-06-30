using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VoziloPrimljenoNaServisAddView
    {
        public string Model { get; set; }
        public string OpisProblema{ get; set; }
        public int GodinaProizvodnje { get; set; }
        public int IdVlasnika { get; set; }
        public int JmbgZaposleni { get; set; }

        public virtual DateTime DatumPrijema { get; set; }
        public virtual DateTime DatumZavrsetkaRadova { get; set; }

        public VoziloPrimljenoNaServisAddView()
        {

        }

    }
}
