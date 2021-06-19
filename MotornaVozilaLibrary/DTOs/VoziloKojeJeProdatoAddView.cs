using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VoziloKojeJeProdatoAddView
    {
        public int BrojSasije { get; set; }
        public int BrojMotora { get; set; }
        public string TipGoriva { get; set; }
        public int Kubikaza { get; set; }
        public string Model { get; set; }
        public string FPutnickoVozilo { get; set; }
        public string FTeretnoVozilo { get; set; }
        public int BrojPutnika { get; set; }
        public int Nosivost { get; set; }
        public string TipProstora { get; set; }
        public DateTime DatumUvoza { get; set; }
        public IList<string> Boje { get; set; }
        public int IdKupovine { get; set; }
        public int JmbgRadnikaTehnickeStruke { get; set; }

        public VoziloKojeJeProdatoAddView()
        {
            Boje = new List<string>();
        }
    }
}
