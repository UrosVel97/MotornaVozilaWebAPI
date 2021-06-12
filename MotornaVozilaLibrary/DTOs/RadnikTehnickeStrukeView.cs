using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class RadnikTehnickeStrukeView :ZaposleniView
    {
        public IList<string> Specijalnosti{ get; set; }

        public RadnikTehnickeStrukeView()
        {
            Specijalnosti = new List<string>();
        }
        public RadnikTehnickeStrukeView(RadnikTehnickeStruke z):base(z)
        {
            foreach (Specijalnost spec in z.Specijalnosti)
            {
                Specijalnosti.Add(spec.SpecijalnostRadnika);
            }
        }
    }
}
