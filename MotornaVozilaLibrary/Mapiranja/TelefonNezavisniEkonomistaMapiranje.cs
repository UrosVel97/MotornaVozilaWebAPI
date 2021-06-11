using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    class TelefonNezavisniEkonomistaMapiranje:ClassMap<TelefonNezavisniEkonomista>
    {
        public TelefonNezavisniEkonomistaMapiranje()
        {
            Table("TELEFON_NEZAVISNI_EKONOMISTA");

            Id(x => x.Id, "ID_TELEFONA").GeneratedBy.TriggerIdentity();
            Map(x => x.BrojTelefona, "KONTAKT_TELEFON");

            //mapiranje veze 1:N NeregistrovaniKupac-TelefonNeregistrovaniKupac
            References(x => x.NezavisniEkonomista).Column("FK_JMBG_NEZAVISNI_EKONOMISTA").LazyLoad();



        }
    }
}
