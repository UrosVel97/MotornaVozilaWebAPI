using MotornaVozila.Entiteti;
using MotornaVozilaLibrary.DTOs;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotornaVozilaLibrary
{
    public class DataProvider
    {
        public static List<NezavisniEkonomistaView> VratiNezavisneEkonomiste()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<NezavisniEkonomistaView> n = new List<NezavisniEkonomistaView>();

                IList<NezavisniEkonomista> ekonomisti = s.QueryOver<NezavisniEkonomista>()
                                                        .List<NezavisniEkonomista>();

                foreach(NezavisniEkonomista e in ekonomisti)
                {
                    n.Add(new NezavisniEkonomistaView(e));
                }


                s.Close();

                return n;
            }
            catch(Exception ec)
            {
                throw;
            }
            return null;
        }
    }
}
