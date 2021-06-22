using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class SalonAddView
    {
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public string SefSalona { get; set; }
        public string SefServisa { get; set; }
        public string FServis { get; set; }
        public string StepenOpremljenostiServisa { get; set; }
        public IList<string> TipoviRadova{ get; set; }
        public IList<int> JmbgNezavisnihEkonomista { get; set; }

        public SalonAddView()
        {
            TipoviRadova = new List<string>();
            JmbgNezavisnihEkonomista = new List<int>();
        }
    }
}
