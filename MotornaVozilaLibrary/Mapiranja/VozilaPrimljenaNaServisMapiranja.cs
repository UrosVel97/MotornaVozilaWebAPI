using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;


namespace MotornaVozila.Mapiranja
{
    public class VozilaPrimljenaNaServisMapiranja : ClassMap<VozilaPrimljenaNaServis>
    {
        public VozilaPrimljenaNaServisMapiranja()
        {
            //Mapiranje tabele
            Table("VOZILA_PRIMLJENA_NA_SERVIS");


            //mapiranje primarnog kljuca
            Id(x => x.RegistarskiBroj, "REGISTARSKI_BROJ").GeneratedBy.TriggerIdentity();

            //mapiranje svojstava
            Map(x => x.ModelVozila, "MODEL_VOZILA");
            Map(x => x.GodinaProizvodnje, "GODINA_PROIZVODNJE");
            Map(x => x.OpisProblema, "OPIS_PROBLEMA");
            Map(x => x.DatumPrijema, "DATUM_PRIJEMA");
            Map(x => x.DatumZavrsetkaRadova, "DATUM_ZAVRSETKA_RADOVA");

            //mapiranje veze 1:N Zaposleni-VozilaPrimljenaNaServis
            References(x => x.Zaposleni).Column("FK_JMBG_ZAPOSLENI").LazyLoad();

            //mapiranje veze 1:N Vlasnik-VozilaPrimljenaNaServis
            References(x => x.Vlasnik).Column("FK_ID_VLASNIKA").LazyLoad();
        }
    }
}
