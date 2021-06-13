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

        #region RadnikTehnickeStruke
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


        public static void IzbrisiRadnikaTehnickeStruke(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                RadnikTehnickeStruke rts = s.Load<RadnikTehnickeStruke>(id);

                IList<Specijalnost> spec = rts.Specijalnosti;
                rts.Specijalnosti = new List<Specijalnost>();
                foreach(Specijalnost sp in spec)
                {
                    s.Delete(sp);
                    s.Flush();
                }
                s.Save(rts);

                IList<UvezenoVozilo> uv = rts.UvezenaVozila;
                rts.UvezenaVozila = new List<UvezenoVozilo>();
                foreach(UvezenoVozilo u in uv)
                {
                    IList<Boja>b= u.Boje;
                    u.Boje = new List<Boja>();
                    foreach(Boja boja in b)
                    {
                        s.Delete(boja);
                        s.Flush();
                    }
                    s.Save(u);

                    if(u.GetType()==typeof(VoziloKojeJeProdato))
                    {
                        VoziloKojeJeProdato vp = (VoziloKojeJeProdato)u;
                        vp.Kupovina.ProdataVozila.Remove(vp);
                        s.Save(vp.Kupovina);
                        s.Delete(vp);
                        s.Flush();
                    }
                    else if(u.GetType() == typeof(VoziloKojeNijeProdato))
                    {
                        VoziloKojeNijeProdato vp = (VoziloKojeNijeProdato)u;
                        vp.IzlozenUSalonu.VozilaKojaNisuProdata.Remove(vp);
                        s.Save(vp.IzlozenUSalonu);
                        s.Delete(vp);
                        s.Flush();
                    }
                }

                s.Save(rts);

                IList<VozilaPrimljenaNaServis> vpns = rts.PrimioVoziloNaServis;
                rts.PrimioVoziloNaServis = new List<VozilaPrimljenaNaServis>();

                foreach(VozilaPrimljenaNaServis vp in vpns)
                {
                    vp.Vlasnik.JePoslaoVoziloNaServis.Remove(vp);
                    s.Save(vp.Vlasnik);
                    s.Delete(vp);
                    s.Flush();
                }
                s.Save(rts);

                s.Delete(rts);
                s.Flush();



                s.Close();
            }
            catch(Exception ec)
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
        public static void AzurirajRadnikaTehnickeStruke(RadnikTehnickeStrukeView rts)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                RadnikTehnickeStruke r = s.Load<RadnikTehnickeStruke>(rts.Jmbg);

                r.Ime = rts.Ime;
                r.Prezime = rts.Prezime;
                r.GodineRadnogStaza = rts.GodineRadnogStaza;
                r.DatumIstekaUgovora = rts.DatumIstekaUgovora;
                r.DatumRodjena = rts.DatumRodjenja;
                r.DatumZaposlenja = rts.DatumZaposlenja;
                r.FZaposleniPoUgovoru = rts.FZaposlenPoUgovoru;
                r.FZaposleniZaStalno = rts.FZaposlenZaStalno;
                r.Plata = rts.Plata;
                r.StrucnaSprema = rts.StrucnaSprema;

                s.SaveOrUpdate(r);
                s.Flush();

                foreach (string spec in rts.Specijalnosti)
                {
                    Specijalnost specijalnost = new Specijalnost()
                    {
                        SpecijalnostRadnika = spec,
                        RadnikTehnickeStruke = r
                    };
                    r.Specijalnosti.Add(specijalnost);
                    s.Save(specijalnost);
                    s.Save(r);
                    s.Flush();
                }

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion

        #region RadnikEkonomskeStruke
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


        public static void IzbrisiRadnikaEkonomskeStruke(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                RadnikEkonomskeStruke res = s.Load<RadnikEkonomskeStruke>(id);


                IList<VozilaPrimljenaNaServis> vpns = res.PrimioVoziloNaServis;
                res.PrimioVoziloNaServis = new List<VozilaPrimljenaNaServis>();

                foreach (VozilaPrimljenaNaServis vp in vpns)
                {
                    vp.Vlasnik.JePoslaoVoziloNaServis.Remove(vp);
                    s.Save(vp.Vlasnik);
                    s.Delete(vp);
                    s.Flush();
                }
                s.Save(res);

                s.Delete(res);
                s.Flush();



                s.Close();
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

        public static void AzurirajRadnikaEkonomskeStruke(RadnikEkonomskeStrukeView rts)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                RadnikEkonomskeStruke r = s.Load<RadnikEkonomskeStruke>(rts.Jmbg);

                r.Ime = rts.Ime;
                r.Prezime = rts.Prezime;
                r.GodineRadnogStaza = rts.GodineRadnogStaza;
                r.DatumIstekaUgovora = rts.DatumIstekaUgovora;
                r.DatumRodjena = rts.DatumRodjenja;
                r.DatumZaposlenja = rts.DatumZaposlenja;
                r.FZaposleniPoUgovoru = rts.FZaposlenPoUgovoru;
                r.FZaposleniZaStalno = rts.FZaposlenZaStalno;
                r.Plata = rts.Plata;
                r.StrucnaSprema = rts.StrucnaSprema;

                s.SaveOrUpdate(r);
                s.Flush();



                s.Close();
            }

            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion

        #region NekiDrugiZaposleni
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

        public static void IzbrisiNekogDrugogZaposlenog(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                NekiDrugiZaposleni res = s.Load<NekiDrugiZaposleni>(id);


                IList<VozilaPrimljenaNaServis> vpns = res.PrimioVoziloNaServis;
                res.PrimioVoziloNaServis = new List<VozilaPrimljenaNaServis>();

                foreach (VozilaPrimljenaNaServis vp in vpns)
                {
                    vp.Vlasnik.JePoslaoVoziloNaServis.Remove(vp);
                    s.Save(vp.Vlasnik);
                    s.Delete(vp);
                    s.Flush();
                }
                s.Save(res);

                s.Delete(res);
                s.Flush();



                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void DodajNekogDrugogZaposlenog(ZaposleniView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                NekiDrugiZaposleni z = new NekiDrugiZaposleni()
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

                s.Save(z);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion
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


        #endregion

        #region UvezenoVozilo

        public static List<VoziloKojeJeProdatoView> VratiVoziloKojeJeProdato()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<VoziloKojeJeProdatoView> vozila = new List<VoziloKojeJeProdatoView>();

                IList<VoziloKojeJeProdato> vk = s.QueryOver<VoziloKojeJeProdato>()
                                                        .List<VoziloKojeJeProdato>();

                foreach (VoziloKojeJeProdato v in vk)
                {
                    vozila.Add(new VoziloKojeJeProdatoView(v));
                }

                s.Close();

                return vozila;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static List<VoziloKojeNijeProdatoView> VratiVoziloKojeNijeProdato()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<VoziloKojeNijeProdatoView> vozila = new List<VoziloKojeNijeProdatoView>();

                IList<VoziloKojeNijeProdato> vk = s.QueryOver<VoziloKojeNijeProdato>()
                                                        .List<VoziloKojeNijeProdato>();

                foreach (VoziloKojeNijeProdato v in vk)
                {
                    vozila.Add(new VoziloKojeNijeProdatoView(v));
                }

                s.Close();

                return vozila;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void AzurirajVoziloKojeNijeProdato(VoziloKojeNijeProdatoView vozilo)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                VoziloKojeNijeProdato vk = s.Load<VoziloKojeNijeProdato>(vozilo.BrojSasije);
                vk.TipGoriva = vozilo.TipGoriva;
                vk.Kubikaza = vozilo.Kubikaza;
                vk.ModelVozila = vozilo.Model;
                vk.FPutnickoVozilo = vozilo.FPutnickoVozilo;
                vk.FTeretnoVozilo = vozilo.FTeretnoVozilo;
                vk.BrojPutnika = vozilo.BrojPutnika;
                vk.Nosivost = vozilo.Nosivost;
                vk.TipProstora = vozilo.TipProstora;

                s.Update(vk);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void AzurirajVoziloKojeJeProdato(VoziloKojeJeProdatoView vozilo)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                VoziloKojeJeProdato vk = s.Load<VoziloKojeJeProdato>(vozilo.BrojSasije);
                vk.TipGoriva = vozilo.TipGoriva;
                vk.Kubikaza = vozilo.Kubikaza;
                vk.ModelVozila = vozilo.Model;
                vk.FPutnickoVozilo = vozilo.FPutnickoVozilo;
                vk.FTeretnoVozilo = vozilo.FTeretnoVozilo;
                vk.BrojPutnika = vozilo.BrojPutnika;
                vk.Nosivost = vozilo.Nosivost;
                vk.TipProstora = vozilo.TipProstora;

                s.Update(vk);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion
    }
}
