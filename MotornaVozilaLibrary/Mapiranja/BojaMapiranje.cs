using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    public class BojaMapiranje:ClassMap<Boja>
    {
        public BojaMapiranje()
        {
            Table("BOJA_UVEZENO_VOZILO");

            //Mapiranje primarnog kljuca
            Id(x => x.Id, "ID_BOJE").GeneratedBy.TriggerIdentity();

            //Mapiranje prostih atributa
            Map(x => x.BojaVozila, "BOJA");

            //Mapiranje veze 1:N, UvezenoVozilo : Boja
            References(x => x.UvezenoVozilo).Column("FK_BROJ_SASIJE").LazyLoad();
        }

    }
}
