using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    public class SpecijalnostMapiranje: ClassMap<Specijalnost>
    {
        public SpecijalnostMapiranje()
        {
            Table("SPEC_RADNIK_TEHNICKE_STRUKE");

            Id(x => x.Id, "ID_SPECIJALNOSTI").GeneratedBy.TriggerIdentity();

            Map(x => x.SpecijalnostRadnika, "SPECIJALNOST");

            //mapiranje veze 1:N RadnikTehnickeStruke-Specijalnost
            References(x => x.RadnikTehnickeStruke).Column("FK_JMBG_ZAPOSLENOG").LazyLoad();

        }
    }
}
