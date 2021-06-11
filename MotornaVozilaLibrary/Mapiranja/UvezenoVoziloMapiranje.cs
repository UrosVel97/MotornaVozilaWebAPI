using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MotornaVozila.Mapiranja
{
    public class UvezenoVoziloMapiranje: ClassMap<UvezenoVozilo>
    {
        public UvezenoVoziloMapiranje()
        {
            try
            {
                //Mapiranje tabele
                Table("UVEZENO_VOZILO");

                /*mapiranje podklasa. Ova fja kaze da ce kolona sa imenom 
                'TIP_VOZILA' da odredi sa kojom podklasom radimo*/
                DiscriminateSubClassesOnColumn("TIP_VOZILA");

                //Mapiranje primarnog kljuca
                Id(x => x.BrojSasije, "BROJ_SASIJE").GeneratedBy.Assigned();

                //Mapiranje propertija
                Map(x => x.DatumUvoza, "DATUM_UVOZA");
                Map(x => x.BrojMotora, "BROJ_MOTORA");
                Map(x => x.TipGoriva, "TIP_GORIVA");
                Map(x => x.Kubikaza, "KUBIKAZA");
                Map(x => x.ModelVozila, "MODEL_VOZILA");
                Map(x => x.FPutnickoVozilo, "F_PUTNICKO_VOZILO");
                Map(x => x.BrojPutnika, "BROJ_PUTNIKA");
                Map(x => x.FTeretnoVozilo, "F_TERETNO_VOZILO");
                Map(x => x.Nosivost, "NOSIVOST");
                Map(x => x.TipProstora, "TIP_PROSTORA");

                //mapiranje veze 1:N RadnikTehnickeStruke-UvezenoVozilo
                References(x => x.RadnikTehnStruke).Column("FK_JMBG_RADNIK_TEHNICKE_STRUKE").LazyLoad();

                //Veza 1:N, UvezenoVozilo : Boja
                HasMany(x => x.Boje).KeyColumn("FK_BROJ_SASIJE").LazyLoad().Cascade.All().Inverse();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
    }


    //Mapiranje podklasa
    public class VoziloKojeJeProdatoMapiranje : SubclassMap<VoziloKojeJeProdato>
    {
        public VoziloKojeJeProdatoMapiranje()
        {
            DiscriminatorValue("Vozilo koje je prodato");

            //mapiranje veze 1:N Kupovina-VoziloKojeJeProdato
            References(x => x.Kupovina).Column("FK_ID_KUPOVINE").LazyLoad();
        }
    }

    public class VoziloKojeNijeProdatoMapiranje : SubclassMap<VoziloKojeNijeProdato>
    {
        public VoziloKojeNijeProdatoMapiranje()
        {
            DiscriminatorValue("Vozilo koje nije prodato");

            //mapiranje veze 1:N Salon-VoziloKojeNijeProdato
            References(x => x.IzlozenUSalonu).Column("FK_ID_SALONA_U_KOME_JE_IZLOZEN").LazyLoad();
        }
    }
}
