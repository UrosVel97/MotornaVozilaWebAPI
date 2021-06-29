using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VlasnikNeregistrovaniKupacAddView
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public IList<string> Telefoni { get; set; }
        public VlasnikNeregistrovaniKupacAddView()
        {
            Telefoni = new List<string>();
        }
    }
}
