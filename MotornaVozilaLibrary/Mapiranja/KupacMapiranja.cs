using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    public class KupacMapiranja: ClassMap<Kupac>
    {
        public KupacMapiranja()
        {

            //Mapiranje tabele
            Table("KUPAC");

            /*mapiranje podklasa. Ova fja kaze da ce kolona sa imenom 
             'TIP' da odredi sa kojom podklasom radimo*/
            DiscriminateSubClassesOnColumn("TIP_KUPCA");

            //mapiranje primarnog kljuca
            Id(x => x.Id, "ID_KUPCA").GeneratedBy.TriggerIdentity();

            //mapiranje svojstava
            //Map(x => x.Tip, "TIP");
            Map(x => x.LicnoIme, "LICNO_IME");
            Map(x => x.Prezime, "PREZIME");

            //Veza 1:N Kupac : Kupovina
            HasMany(x => x.Kupovine).KeyColumn("FK_ID_KUPCA").LazyLoad().Cascade.All().Inverse();

            //Veza 1:N Kupac : RegistrovaniKupac
            HasMany(x => x.RegistrovaniKupci).KeyColumn("FK_ID_KUPCA").LazyLoad().Cascade.All().Inverse();

            //Veza 1:N Kupac : TelefonKupac
            HasMany(x => x.Telefoni).KeyColumn("FK_ID_KUPCA").LazyLoad().Cascade.All().Inverse();

        }
    }

    //Mapiranje podklasa

    class FizickoLiceMapiranja : SubclassMap<FizickoLice>
    {
        public FizickoLiceMapiranja()
        {
            /*Podklase moraju da imaju ovu fju 'DiscriminatorValue()' kojoj kao parametar
            prosledjujemo vrednost koju kolona 'TIP_KUPCA' u tabeli 'KUPAC' moze da ima*/
            DiscriminatorValue("Fizicko lice");
            //Mapiramo proste atribute podklase ako ih ima
            Map(x => x.Jmbg, "JMBG");
        }
    }

    class PravnoLiceMapiranja : SubclassMap<PravnoLice>
    {
        public PravnoLiceMapiranja()
        {
            /*Podklase moraju da imaju ovu fju 'DiscriminatorValue()' kojoj kao parametar
            prosledjujemo vrednost koju kolona 'TIP_KUPCA' u tabeli 'KUPAC' moze da ima*/
            DiscriminatorValue("Pravno lice");
            //Mapiramo proste atribute podklase ako ih ima
            Map(x => x.Pib, "PIB");
        }
    }
}
