using FluentNHibernate.Mapping;
using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MotornaVozila.Mapiranja
{
    public class TipRadovaMapiranje : ClassMap<TipRadova>
    {
        public TipRadovaMapiranje()
        {
            try
            {
                //Mapiranje tabele
                Table("TIP_RADOVA");

                

                //Mapiranje primarnog kljuca
                Id(x => x.Tip_Radova, "TIP_RADOVA").GeneratedBy.Assigned();

                //Mapiranje veze M:N Salon : TipRadova. Prvo cuvamo Salon
                HasManyToMany(x => x.Servis)
                            .Table("OBAVLJA")
                            .ParentKeyColumn("FK_TIP_RADOVA")
                            .ChildKeyColumn("FK_ID_SALONA")
                            .Cascade.All();


            }
            catch (Exception ec)
            {
                throw;
            }
        }
    }
}
