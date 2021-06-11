using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    class TelefonNeregistrovaniKupacMapiranja:ClassMap<TelefonNeregistrovaniKupac>
    {
        public TelefonNeregistrovaniKupacMapiranja()
        {
            Table("TELEFON_NEREGISTROVANI_KUPAC");

            Id(x => x.Id, "ID_TELEFONA").GeneratedBy.TriggerIdentity();
            Map(x => x.BrojTelefona, "KONTAKT_TELEFON");

            //mapiranje veze 1:N NeregistrovaniKupac-TelefonNeregistrovaniKupac
            References(x => x.NeregistrovaniKupac).Column("FK_ID_VLASNIKA").LazyLoad();



        }
    }
}
