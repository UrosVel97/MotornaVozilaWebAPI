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

        #region NezavisniEkonomista
        public static List<NezavisniEkonomistaView> VratiNezavisneEkonomiste()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<NezavisniEkonomistaView> n = new List<NezavisniEkonomistaView>();

                IList<NezavisniEkonomista> ekonomisti = s.QueryOver<NezavisniEkonomista>()
                                                        .List<NezavisniEkonomista>();

                foreach (NezavisniEkonomista e in ekonomisti)
                {
                    n.Add(new NezavisniEkonomistaView(e));
                }


                s.Close();

                return n;
            }
            catch (Exception ec)
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
                foreach (string t in n.Telefoni)
                {
                    TelefonNezavisniEkonomista tel = new TelefonNezavisniEkonomista()
                    {
                        BrojTelefona = t,
                        NezavisniEkonomista = nez
                    };
                    nez.Telefoni.Add(tel);
                    s.Save(tel);
                    s.Save(nez);
                    s.Flush();

                }

                s.Flush();

                s.Close();
            }
            catch (Exception ec)
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
        #endregion

        #region Zaposleni
        public static void DodajRadnikaTehnickeStruke(RadnikTehnickeStrukeView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                RadnikTehnickeStruke rts = new RadnikTehnickeStruke()
                {
                    Jmbg = r.Jmbg,
                    Ime = r.Ime,
                    Prezime = r.Prezime,
                    GodineRadnogStaza = r.GodineRadnogStaza,
                    DatumRodjena = r.DatumRodjenja,
                    DatumZaposlenja = r.DatumZaposlenja,
                    FZaposleniZaStalno = r.FZaposlenZaStalno,
                    FZaposleniPoUgovoru = r.FZaposlenPoUgovoru,
                    Plata = r.Plata,
                    DatumIstekaUgovora = r.DatumIstekaUgovora,
                    StrucnaSprema = r.StrucnaSprema

                };

                s.Save(rts);
                s.Flush();

                foreach (string spec in r.Specijalnosti)
                {
                    Specijalnost specijalnost = new Specijalnost()
                    {
                        SpecijalnostRadnika = spec,
                        RadnikTehnickeStruke = rts
                    };

                    rts.Specijalnosti.Add(specijalnost);
                    s.Save(specijalnost);
                    s.Save(rts);
                    s.Flush();
                }

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void DodajRadnikaEkonomskeStruke(RadnikEkonomskeStrukeView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                RadnikEkonomskeStruke res = new RadnikEkonomskeStruke()
                {
                    Jmbg = r.Jmbg,
                    Ime = r.Ime,
                    Prezime = r.Prezime,
                    GodineRadnogStaza = r.GodineRadnogStaza,
                    DatumRodjena = r.DatumRodjenja,
                    DatumZaposlenja = r.DatumZaposlenja,
                    FZaposleniZaStalno = r.FZaposlenZaStalno,
                    FZaposleniPoUgovoru = r.FZaposlenPoUgovoru,
                    Plata = r.Plata,
                    DatumIstekaUgovora = r.DatumIstekaUgovora,
                    StrucnaSprema = r.StrucnaSprema

                };

                s.Save(res);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static List<ZaposleniView> VratiZaposlene()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<ZaposleniView> zaposleni = new List<ZaposleniView>();

                IList<Zaposleni> za = s.QueryOver<Zaposleni>()
                                                        .List<Zaposleni>();

                foreach (Zaposleni z in za)
                {
                    zaposleni.Add(new ZaposleniView(z));
                }

                s.Close();

                return zaposleni;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static List<RadnikEkonomskeStrukeView> VratiRadnikaEkonomskeStruke()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<RadnikEkonomskeStrukeView> zaposleni = new List<RadnikEkonomskeStrukeView>();

                IList<RadnikEkonomskeStruke> rs = s.QueryOver<RadnikEkonomskeStruke>()
                                                        .List<RadnikEkonomskeStruke>();

                foreach (RadnikEkonomskeStruke z in rs)
                {
                    zaposleni.Add(new RadnikEkonomskeStrukeView(z));
                }

                s.Close();

                return zaposleni;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static List<RadnikTehnickeStrukeView> VratiRadnikaTehnickeStruke()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<RadnikTehnickeStrukeView> zaposleni = new List<RadnikTehnickeStrukeView>();

                IList<RadnikTehnickeStruke> rt = s.QueryOver<RadnikTehnickeStruke>()
                                                        .List<RadnikTehnickeStruke>();

                foreach (RadnikTehnickeStruke z in rt)
                {
                    zaposleni.Add(new RadnikTehnickeStrukeView(z));
                }

                s.Close();

                return zaposleni;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static List<ZaposleniView> VratiNekeDrugeZaposlene()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<ZaposleniView> zaposleni = new List<ZaposleniView>();

                IList<NekiDrugiZaposleni> nz = s.QueryOver<NekiDrugiZaposleni>()
                                                        .List<NekiDrugiZaposleni>();

                foreach (Zaposleni z in nz)
                {
                    zaposleni.Add(new ZaposleniView(z));
                }

                s.Close();

                return zaposleni;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion
    }
}
