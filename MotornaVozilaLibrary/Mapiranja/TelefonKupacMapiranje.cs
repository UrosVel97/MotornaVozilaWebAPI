using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    public class TelefonKupacMapiranje : ClassMap<TelefonKupac>
    {
        public TelefonKupacMapiranje()
        {
            Table("TELEFON_KUPAC");
            
            //Mapiranje primarnog kljuca
            Id(x => x.Id, "ID_TELEFONA").GeneratedBy.TriggerIdentity();

            Map(x => x.Telefon, "KONTAKT_TELEFON");

            //mapiranje veze 1:N Kupac-TelefonKupac
            References(x => x.Kupac).Column("FK_ID_KUPCA").LazyLoad();


        }
    }
}
