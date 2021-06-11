using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    public class KupovinaMapiranja : ClassMap<Kupovina>
    {
        public KupovinaMapiranja()
        {
            Table("KUPOVINA");

            //Maprinaje primarnog kljuca
            Id(x => x.Id, "ID_KUPOVINE").GeneratedBy.TriggerIdentity();

            //Mapiranje prostih atributa
            Map(x => x.DatumKupovine, "DATUM_KUPOVINE");

            //Mapiranje veze 1:N, Salon : Kupovina
            References(x => x.IdSalona).Column("FK_ID_SALONA").LazyLoad();

            //Mapiranje veze 1:N, Kupac : Kupovina
            References(x => x.IdKupca).Column("FK_ID_KUPCA").LazyLoad();

            //Veza 1:N Kupovina : VoziloKojeJeProdato
            HasMany(x => x.ProdataVozila).KeyColumn("FK_ID_KUPOVINE").LazyLoad().Cascade.All().Inverse();

        }
    }
}
