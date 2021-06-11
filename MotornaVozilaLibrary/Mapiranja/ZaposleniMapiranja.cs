using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotornaVozila.Entiteti;


namespace MotornaVozila.Mapiranja
{
    public class ZaposleniMapiranja: ClassMap<Zaposleni>
    {
        public ZaposleniMapiranja()
        {
            try
            {
                //Mapiranje tabele
                Table("ZAPOSLENI");

                /*mapiranje podklasa. Ova fja kaze da ce kolona sa imenom 
                'TIP_ZAPOSLENOG' da odredi sa kojom podklasom radimo*/
                DiscriminateSubClassesOnColumn("TIP_ZAPOSLENOG");

                //Mapiranje primarnog kljuca
                Id(x => x.Jmbg, "JMBG").GeneratedBy.Assigned();

                //Mapiranje propertija
                Map(x => x.Ime, "IME");
                Map(x => x.Prezime, "PREZIME");
                Map(x => x.GodineRadnogStaza, "GODINE_RADNOG_STAZA");
                Map(x => x.DatumRodjena, "DATUM_RODJENJA");
                Map(x => x.DatumZaposlenja, "DATUM_ZAPOSLENJA");
                Map(x => x.StrucnaSprema, "STRUCNA_SPREMA");
                Map(x => x.Plata, "PLATA");
                Map(x => x.FZaposleniZaStalno, "F_ZAPOSLEN_ZA_STALNO");
                Map(x => x.FZaposleniPoUgovoru, "F_ZAPOSLEN_PO_UGOVORU");
                Map(x => x.DatumIstekaUgovora, "DATUM_ISTEKA_UGOVORA");

                //Veza 1:N, Zaposleni : VozilaPrimljenaNaServis
                HasMany(x => x.PrimioVoziloNaServis).KeyColumn("FK_JMBG_ZAPOSLENI").LazyLoad().Cascade.All().Inverse();
            }
            catch(Exception ec)
            {
                throw;
            }

        }
    }

    //Mapiranje podklasa
    public class RadnikEkonomskeStrukeMapiranje: SubclassMap<RadnikEkonomskeStruke>
    {
        public RadnikEkonomskeStrukeMapiranje()
        {
            DiscriminatorValue("Radnik ekonomske struke");
        }
    }

    public class RadnikTehnickeStrukeMapiranje : SubclassMap<RadnikTehnickeStruke>
    {
        public RadnikTehnickeStrukeMapiranje()
        {
            DiscriminatorValue("Radnik tehnicke struke");

            //Veza 1:N, RadnikTehnickeStruke : UvezenoVozilo
            HasMany(x => x.UvezenaVozila).KeyColumn("FK_JMBG_RADNIK_TEHNICKE_STRUKE").LazyLoad().Cascade.All().Inverse();

            //Veza 1:N, RadnikTehnickeStruke : Specijalnost
            HasMany(x => x.Specijalnosti).KeyColumn("FK_JMBG_ZAPOSLENOG").LazyLoad().Cascade.All().Inverse();

        }
    }

    public class NekiDrugiZaposleniMapiranje : SubclassMap<NekiDrugiZaposleni>
    {
        public NekiDrugiZaposleniMapiranje()
        {
            DiscriminatorValue("Neki drugi zaposleni");
        }
    }

}
