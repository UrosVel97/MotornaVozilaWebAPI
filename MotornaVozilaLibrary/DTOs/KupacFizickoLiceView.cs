using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class KupacFizickoLiceView :KupacView
    {
        public int Jmbg{ get; set; }

        public KupacFizickoLiceView()
        {

        }
        public KupacFizickoLiceView(FizickoLice f): base(f)
        {
            Jmbg = f.Jmbg;
        }
    }
}
