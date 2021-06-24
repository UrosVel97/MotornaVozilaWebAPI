using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class KupovinaAddView
    {
        public DateTime DatumKupovine { get; set; }
        public int IdSalona { get; set; }
        public int IdKupca { get; set; }
    }
}
