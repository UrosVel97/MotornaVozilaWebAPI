using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Entiteti
{
    public class Specijalnost
    {
        public virtual int Id{ get; protected set; }
        public virtual RadnikTehnickeStruke RadnikTehnickeStruke { get; set; }
        public virtual string SpecijalnostRadnika { get; set; }

    }
}
