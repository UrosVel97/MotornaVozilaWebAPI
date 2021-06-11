using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozila.Mapiranja
{
    public class SalonMapiranja:ClassMap<Salon>
    {
        public SalonMapiranja()
        {
            try
            {
                //Mapiranje tabele
                Table("SALON");

                //Mapiranje primarnog kljuca
                Id(x => x.Id, "ID_SALONA").GeneratedBy.TriggerIdentity();

                //Mapiranje propertija
                Map(x => x.Grad, "GRAD");
                Map(x => x.Adresa, "ADRESA");
                Map(x => x.StepenOpremljenostiServisa, "STEPEN_OPREMLJENOSTI_SERVISA");
                Map(x => x.SefSalona, "SEF_SALONA");
                Map(x => x.SefServisa, "SEF_SERVISA");
                Map(x => x.FServis, "F_SERVIS");


                //Mapiranje veze M:N Salon : TipRadova. Prvo cuvamo Salon
                HasManyToMany(x => x.TipoviRadova)
                            .Table("OBAVLJA")
                            .ParentKeyColumn("FK_ID_SALONA")
                            .ChildKeyColumn("FK_TIP_RADOVA")
                            .Cascade.All()
                            .Inverse();


                //Mapiranje veze M:N Salon : NezavisniEkonomista. Prvo cuvamo NezavisnogEkonomistu
                HasManyToMany(x => x.NezavisniEkonomisti)
                            .Table("JE_ANGAZOVAO")
                            .ParentKeyColumn("FK_ID_SALONA")
                            .ChildKeyColumn("FK_JMBG_NEZAVISNI_EKONOMISTA")
                            .Cascade.All();

                //Veza 1:N, Salon : VoziloKojeNijeProdato
                HasMany(x => x.VozilaKojaNisuProdata).KeyColumn("FK_ID_SALONA_U_KOME_JE_IZLOZEN").LazyLoad().Cascade.All().Inverse();

                //Veza 1:N Salon : Kupovina
                HasMany(x => x.Kupovine).KeyColumn("FK_ID_SALONA").LazyLoad().Cascade.All().Inverse();

            }
            catch (Exception ec)
            {
                throw;
            }

        }

    }
}
