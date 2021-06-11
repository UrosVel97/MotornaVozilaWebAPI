using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    public class VlasnikMapiranje : ClassMap<Vlasnik>
    {
        public VlasnikMapiranje()
        {
            Table("VLASNIK");

            /*mapiranje podklasa. Ova fja kaze da ce kolona sa imenom 
             'TIP' da odredi sa kojom podklasom radimo*/
            DiscriminateSubClassesOnColumn("TIP_VLASNIKA");

            //mapiranje primarnog kljuca
            Id(x => x.Id, "ID_VLASNIKA").GeneratedBy.TriggerIdentity();

            //Veza 1:N, Vlasnik : VozilaPrimljenaNaServis
            HasMany(x => x.JePoslaoVoziloNaServis).KeyColumn("FK_ID_VLASNIKA").LazyLoad().Cascade.All().Inverse();
        }
    }

    public class NeregistrovaniKupacMapiranje : SubclassMap<NeregistrovaniKupac>
    {
        public NeregistrovaniKupacMapiranje()
        {
            DiscriminatorValue("Neregistrovani kupac");
            //Mapiramo proste atribute podklase ako ih ima
            Map(x => x.Ime, "IME");
            Map(x => x.Prezime, "PREZIME");

            //Veza 1:N, NeregistrovaniKupac : TelefonNeregistrovaniKupac
            HasMany(x => x.Telefoni).KeyColumn("FK_ID_VLASNIKA").LazyLoad().Cascade.All().Inverse();


        }
    }

    public class RegistrovaniKupacMapiranje : SubclassMap<RegistrovaniKupac>
    {
        public RegistrovaniKupacMapiranje()
        {
            DiscriminatorValue("Registrovani kupac");

            //mapiranje veze 1:N Kupac-RegistrovaniKupac
            References(x => x.Kupac).Column("FK_ID_KUPCA").LazyLoad();
        }
    }
}
