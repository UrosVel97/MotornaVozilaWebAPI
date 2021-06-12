﻿using MotornaVozila.Entiteti;
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

        }

        public static void DodajNEkonomistu(NezavisniEkonomistaView n)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                NezavisniEkonomista nez = new NezavisniEkonomista()
                {
                    Jmbg = n.Jmbg,
                    Ime = n.Ime,
                    Prezime = n.Prezime,
                    Adresa = n.Adresa
                };
                s.Save(nez);
                s.Flush();
                foreach(string t in n.Telefoni)
                {
                    TelefonNezavisniEkonomista tel = new TelefonNezavisniEkonomista()
                    { 
                        BrojTelefona=t,
                        NezavisniEkonomista=nez
                    };
                    nez.Telefoni.Add(tel);
                    s.Save(tel);
                    s.Save(nez);
                    s.Flush();

                }

                s.Flush();

                s.Close();
            }
            catch(Exception ec)
            {
                throw;
            }
        }

        public static void IzbrisiNEkonomistu(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                NezavisniEkonomista n = s.Load<NezavisniEkonomista>(id);
                n.Saloni = new List<Salon>();
                IList<Salon> saloni = s.QueryOver<Salon>()
                                        .List<Salon>();
                foreach (Salon salon in saloni)
                {
                    salon.NezavisniEkonomisti.Remove(n);
                    s.SaveOrUpdate(salon);
                    s.Flush();
                }
                n.Telefoni = new List<TelefonNezavisniEkonomista>();
                s.SaveOrUpdate(n);
                s.Flush();


                IList<TelefonNezavisniEkonomista> telefoni = s.QueryOver<TelefonNezavisniEkonomista>()
                                .Where(x => x.NezavisniEkonomista == n)
                                .List<TelefonNezavisniEkonomista>();

                foreach (TelefonNezavisniEkonomista t in telefoni)
                {
                    s.Delete(t);
                    s.Flush();
                }




                n = s.Load<NezavisniEkonomista>(n.Jmbg);
                s.Refresh(n);
                s.Delete(n);

                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void AzurirajNezavisneEkonomiste(NezavisniEkonomistaView nezavisni)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                NezavisniEkonomista ne = s.Load<NezavisniEkonomista>(nezavisni.Jmbg);
                ne.Ime = nezavisni.Ime;
                ne.Prezime = nezavisni.Prezime;
                ne.Adresa = nezavisni.Adresa;

                s.Update(ne);
                s.Flush();
                s.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
