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
                foreach (Specijalnost sp in spec)
                {
                    s.Delete(sp);
                    s.Flush();
                }
                s.Save(rts);

                IList<UvezenoVozilo> uv = rts.UvezenaVozila;
                rts.UvezenaVozila = new List<UvezenoVozilo>();
                foreach (UvezenoVozilo u in uv)
                {
                    IList<Boja> b = u.Boje;
                    u.Boje = new List<Boja>();
                    foreach (Boja boja in b)
                    {
                        s.Delete(boja);
                        s.Flush();
                    }
                    s.Save(u);

                    if (u.GetType() == typeof(VoziloKojeJeProdato))
                    {
                        VoziloKojeJeProdato vp = (VoziloKojeJeProdato)u;
                        vp.Kupovina.ProdataVozila.Remove(vp);
                        s.Save(vp.Kupovina);
                        s.Delete(vp);
                        s.Flush();
                    }
                    else if (u.GetType() == typeof(VoziloKojeNijeProdato))
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

                foreach (VozilaPrimljenaNaServis vp in vpns)
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

        #region VoziloKojeNijeProdato
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

        public static void DodajVoziloKojeNijeProdato(VoziloKojeNijeProdatoAddView v)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VoziloKojeNijeProdato vozilo = new VoziloKojeNijeProdato()
                {
                    BrojSasije = v.BrojSasije,
                    BrojMotora = v.BrojMotora,
                    DatumUvoza = v.DatumUvoza,
                    TipGoriva = v.TipGoriva,
                    Kubikaza = v.Kubikaza,
                    ModelVozila = v.Model,
                    FPutnickoVozilo = v.FPutnickoVozilo,
                    FTeretnoVozilo = v.FTeretnoVozilo,
                    Nosivost = v.Nosivost,
                    TipProstora = v.TipProstora,
                    BrojPutnika = v.BrojPutnika
                };

                RadnikTehnickeStruke rts = s.Load<RadnikTehnickeStruke>(v.JmbgRadnikaTehnickeStruke);
                Salon salon = s.Load<Salon>(v.IdSalona);
                vozilo.RadnikTehnStruke = rts;
                vozilo.IzlozenUSalonu = salon;
                rts.UvezenaVozila.Add(vozilo);
                salon.VozilaKojaNisuProdata.Add(vozilo);
                s.Save(vozilo);
                s.Save(rts);
                s.Save(salon);
                s.Flush();

                foreach (string boja in v.Boje)
                {
                    Boja b = new Boja()
                    {
                        BojaVozila = boja,
                        UvezenoVozilo = vozilo
                    };
                    vozilo.Boje.Add(b);
                    s.Save(b);
                    s.Save(vozilo);
                    s.Flush();
                }


                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }


        public static void IzbrisiVoziloKojeNijeProdato(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VoziloKojeNijeProdato v = s.Load<VoziloKojeNijeProdato>(id);
                v.RadnikTehnStruke.UvezenaVozila.Remove(v);
                v.IzlozenUSalonu.VozilaKojaNisuProdata.Remove(v);

                IList<Boja> boje = v.Boje;
                v.Boje = new List<Boja>();

                foreach (Boja b in boje)
                {
                    s.Delete(b);
                    s.Flush();
                }

                s.SaveOrUpdate(v);
                s.Delete(v);
                s.Flush();


                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion

        #region VoziloKojeJeProdato
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




        public static void DodajVoziloKojeJeProdato(VoziloKojeJeProdatoAddView v)
        {

            try
            {

                ISession s = DataLayer.GetSession();
                VoziloKojeJeProdato vozilo = new VoziloKojeJeProdato()
                {
                    BrojSasije = v.BrojSasije,
                    BrojMotora = v.BrojMotora,
                    DatumUvoza = v.DatumUvoza,
                    TipGoriva = v.TipGoriva,
                    Kubikaza = v.Kubikaza,
                    ModelVozila = v.Model,
                    FPutnickoVozilo = v.FPutnickoVozilo,
                    FTeretnoVozilo = v.FTeretnoVozilo,
                    Nosivost = v.Nosivost,
                    TipProstora = v.TipProstora,
                    BrojPutnika = v.BrojPutnika
                };

                RadnikTehnickeStruke rts = s.Load<RadnikTehnickeStruke>(v.JmbgRadnikaTehnickeStruke);
                Kupovina kupovina = s.Load<Kupovina>(v.IdKupovine);
                vozilo.RadnikTehnStruke = rts;
                vozilo.Kupovina = kupovina;
                rts.UvezenaVozila.Add(vozilo);
                kupovina.ProdataVozila.Add(vozilo);
                s.Save(vozilo);
                s.Save(rts);
                s.Save(kupovina);
                s.Flush();

                foreach (string boja in v.Boje)
                {
                    Boja b = new Boja()
                    {
                        BojaVozila = boja,
                        UvezenoVozilo = vozilo
                    };
                    vozilo.Boje.Add(b);
                    s.Save(b);
                    s.Save(vozilo);
                    s.Flush();
                }


                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }


        public static void IzbrisiVoziloKojeJeProdato(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VoziloKojeJeProdato v = s.Load<VoziloKojeJeProdato>(id);
                v.RadnikTehnStruke.UvezenaVozila.Remove(v);
                v.Kupovina.ProdataVozila.Remove(v);

                IList<Boja> boje = v.Boje;
                v.Boje = new List<Boja>();

                foreach (Boja b in boje)
                {
                    s.Delete(b);
                    s.Flush();
                }

                s.SaveOrUpdate(v);
                s.Delete(v);
                s.Flush();


                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region Salon

        public static List<SalonView> VratiSalone()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<SalonView> vozila = new List<SalonView>();

                IList<Salon> salons = s.QueryOver<Salon>()
                                            .List<Salon>();

                foreach (Salon sa in salons)
                {
                    vozila.Add(new SalonView(sa));
                }

                s.Close();

                return vozila;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void AzurirajSalone(SalonView salon)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Salon sa = s.Load<Salon>(salon.Id);
                sa.Grad = salon.Grad;
                sa.Adresa = salon.Adresa;
                sa.SefSalona = salon.SefSalona;
                sa.SefServisa = salon.SefServisa;
                sa.StepenOpremljenostiServisa = salon.StepenOpremljenostiServisa;


                s.Update(sa);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }



        public static void DodajSalon(SalonAddView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Salon salon = new Salon()
                {
                    Grad = r.Grad,
                    Adresa = r.Adresa,
                    StepenOpremljenostiServisa = r.StepenOpremljenostiServisa,
                    SefSalona = r.SefSalona,
                    SefServisa = r.SefServisa,
                    FServis = r.FServis
                };

                s.Save(salon);
                s.Flush();

                foreach (string tip in r.TipoviRadova)
                {
                    TipRadova t = s.Get<TipRadova>(tip);
                    if (t == null)
                    {
                        t = new TipRadova()
                        {
                            Tip_Radova = tip
                        };

                    }
                    t.Servis.Add(salon);
                    salon.TipoviRadova.Add(t);
                    s.Save(t);
                    s.Save(salon);
                    s.Flush();


                }

                foreach (int jmbg in r.JmbgNezavisnihEkonomista)
                {
                    NezavisniEkonomista n = s.Load<NezavisniEkonomista>(jmbg);
                    salon.NezavisniEkonomisti.Add(n);
                    n.Saloni.Add(salon);
                    s.Save(salon);
                    s.Save(n);
                    s.Flush();

                }



                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }


        public static void IzbrisiSalon(int id)
        {
            try
            {
                IzbrisiVoziloKojeNijeProdatoZaSalon(id);
                IzbrisiKupovinuZaSalon(id);


                ISession s = DataLayer.GetSession();
                Salon salon = s.Load<Salon>(id);


                IList<TipRadova> tipovi = salon.TipoviRadova;
                salon.TipoviRadova = new List<TipRadova>();
                foreach (TipRadova t in tipovi)
                {
                    t.Servis.Remove(salon);
                    s.SaveOrUpdate(t);
                }

                s.SaveOrUpdate(salon);

                IList<NezavisniEkonomista> ne = salon.NezavisniEkonomisti;
                salon.NezavisniEkonomisti = new List<NezavisniEkonomista>();
                foreach (NezavisniEkonomista n in ne)
                {
                    n.Saloni.Remove(salon);
                    s.SaveOrUpdate(n);
                }
                s.SaveOrUpdate(salon);
                s.Flush();

                s.Delete(salon);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        private static void IzbrisiKupovinuZaSalon(int idSalona)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Salon salonZaBrisanje = s.Load<Salon>(idSalona);
                IList<Kupovina> kup = salonZaBrisanje.Kupovine;
                salonZaBrisanje.Kupovine = new List<Kupovina>();
                foreach (Kupovina kupovina in kup)
                {
                    Kupovina k = s.Load<Kupovina>(kupovina.Id);
                    Kupac kupac = s.Load<Kupac>(k.IdKupca.Id);

                    kupac.Kupovine.Remove(k);
                    s.SaveOrUpdate(kupac);

                    s.Flush();

                    IList<VoziloKojeJeProdato> prodata = k.ProdataVozila;
                    k.ProdataVozila = new List<VoziloKojeJeProdato>();
                    foreach (VoziloKojeJeProdato vkp in prodata)
                    {

                        RadnikTehnickeStruke rts = s.Load<RadnikTehnickeStruke>(vkp.RadnikTehnStruke.Jmbg);
                        rts.UvezenaVozila.Remove(vkp);
                        s.SaveOrUpdate(rts);

                        s.Delete(vkp);
                        s.Flush();

                    }
                    s.SaveOrUpdate(k);
                    s.Flush();
                    s.Delete(k);
                    s.Flush();

                }

                s.SaveOrUpdate(salonZaBrisanje);
                s.Flush();
                s.Close();
            }

            catch (Exception ec)
            {
                throw;
            }
        }

        private static void IzbrisiVoziloKojeNijeProdatoZaSalon(int idSalona)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Salon salonZaBrisanje = s.Load<Salon>(idSalona);
                IList<VoziloKojeNijeProdato> nisuProdata = salonZaBrisanje.VozilaKojaNisuProdata;
                salonZaBrisanje.VozilaKojaNisuProdata = new List<VoziloKojeNijeProdato>();
                foreach (VoziloKojeNijeProdato v in nisuProdata)
                {
                    VoziloKojeNijeProdato vknp = s.Load<VoziloKojeNijeProdato>(v.BrojSasije);
                    RadnikTehnickeStruke rts = s.Load<RadnikTehnickeStruke>(vknp.RadnikTehnStruke.Jmbg);


                    rts.UvezenaVozila.Remove(vknp);

                    s.SaveOrUpdate(rts);
                    s.Flush();
                    s.Delete(vknp);
                    s.Flush();

                }
                s.SaveOrUpdate(salonZaBrisanje);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }


        #endregion

        #region Kupovina

        public static List<KupovinaView> VratiKupovine()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<KupovinaView> kup = new List<KupovinaView>();

                IList<Kupovina> kupovina = s.QueryOver<Kupovina>()
                                            .List<Kupovina>();

                foreach (Kupovina ku in kupovina)
                {
                    kup.Add(new KupovinaView(ku));
                }

                s.Close();

                return kup;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void AzurirajKupovine(KupovinaView kupovina)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Kupovina ku = s.Load<Kupovina>(kupovina.Id);
                ku.DatumKupovine = kupovina.DatumKupovine;

                s.Update(ku);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void IzbrisiKupovinu(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Kupovina k = s.Load<Kupovina>(id);
                Salon salon = s.Load<Salon>(k.IdSalona.Id);
                Kupac kupac = s.Load<Kupac>(k.IdKupca.Id);

                salon.Kupovine.Remove(k);
                kupac.Kupovine.Remove(k);
                s.SaveOrUpdate(salon);
                s.SaveOrUpdate(kupac);

                s.Flush();
                IList<VoziloKojeJeProdato> vozila = k.ProdataVozila;
                k.ProdataVozila = new List<VoziloKojeJeProdato>();


                foreach (VoziloKojeJeProdato vkp in vozila)
                {
                    RadnikTehnickeStruke rts = s.Load<RadnikTehnickeStruke>(vkp.RadnikTehnStruke.Jmbg);
                    rts.UvezenaVozila.Remove(vkp);
                    s.SaveOrUpdate(rts);
                    s.Flush();
                    s.Delete(vkp);
                    s.Flush();
                }

                s.Delete(k);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void DodajKupovinu(KupovinaAddView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Kupovina k = new Kupovina();
                k.DatumKupovine = r.DatumKupovine;
                Kupac kupac = s.Load<Kupac>(r.IdKupca);
                k.IdKupca = kupac;
                kupac.Kupovine.Add(k);
                Salon salon = s.Load<Salon>(r.IdSalona);
                k.IdSalona = salon;
                salon.Kupovine.Add(k);

                s.Save(k);
                s.Save(kupac);
                s.Save(salon);
                s.Flush();

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion

        #region Kupac

        #region PravnoLice
        public static List<KupacPravnoLiceView> VratiPravnoLice()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<KupacPravnoLiceView> kupac = new List<KupacPravnoLiceView>();

                IList<PravnoLice> pl = s.QueryOver<PravnoLice>()
                                                        .List<PravnoLice>();

                foreach (PravnoLice p in pl)
                {
                    kupac.Add(new KupacPravnoLiceView(p));
                }

                s.Close();

                return kupac;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void DodajPravnoLice(KupacPravnoLiceView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                PravnoLice p = new PravnoLice();
                p.LicnoIme = r.LicnoIme;
                p.Prezime = r.Prezime;
                p.Pib = r.Pib;

                s.Save(p);
                s.Flush();

                foreach(string t in r.Telefoni)
                {
                    TelefonKupac tel = new TelefonKupac();
                    tel.Telefon = t;
                    tel.Kupac = p;
                    p.Telefoni.Add(tel);
                    s.Save(tel);
                    s.Save(p);
                    s.Flush();
                }


                s.Close();
            }
            catch(Exception ec)
            {
                throw;
            }
        }

        #endregion

        #region FizickoLice
        public static List<KupacFizickoLiceView> VratiFizickoLice()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<KupacFizickoLiceView> kupac = new List<KupacFizickoLiceView>();

                IList<FizickoLice> fl = s.QueryOver<FizickoLice>()
                                                        .List<FizickoLice>();

                foreach (FizickoLice f in fl)
                {
                    kupac.Add(new KupacFizickoLiceView(f));
                }

                s.Close();

                return kupac;
            }
            catch (Exception ec)
            {
                throw;
            }
        }



        public static void DodajFizickoLice(KupacFizickoLiceView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                FizickoLice p = new FizickoLice();
                p.LicnoIme = r.LicnoIme;
                p.Prezime = r.Prezime;
                p.Jmbg = r.Jmbg;

                s.Save(p);
                s.Flush();

                foreach (string t in r.Telefoni)
                {
                    TelefonKupac tel = new TelefonKupac();
                    tel.Telefon = t;
                    tel.Kupac = p;
                    p.Telefoni.Add(tel);
                    s.Save(tel);
                    s.Save(p);
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

        public static List<KupacView> VratiKupce()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<KupacView> kupac = new List<KupacView>();

                IList<Kupac> ku = s.QueryOver<Kupac>()
                                         .List<Kupac>();

                foreach (Kupac k in ku)
                {
                    kupac.Add(new KupacView(k));
                }

                s.Close();

                return kupac;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void AzurirajKupce(KupacView kupac)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Kupac k = s.Load<Kupac>(kupac.Id);

                k.LicnoIme = kupac.LicnoIme;
                k.Prezime = kupac.Prezime;

                s.SaveOrUpdate(k);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void IzbrisiKupca(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Kupac k = s.Load<Kupac>(id);

                //Brisanje telefona kupca
                IList<TelefonKupac> telefoni = k.Telefoni;
                k.Telefoni = new List<TelefonKupac>();
                foreach (TelefonKupac t in telefoni)
                {
                    s.Delete(t);
                    s.Flush();
                }

                s.SaveOrUpdate(k);
                s.Flush();

                //Brisanje kupovina
                IList<Kupovina> kupovine = k.Kupovine;
                k.Kupovine = new List<Kupovina>();
                foreach (Kupovina kupovina in kupovine)
                {
                    Salon salon = s.Load<Salon>(kupovina.IdSalona.Id);
                    Kupac kupac = s.Load<Kupac>(kupovina.IdKupca.Id);

                    salon.Kupovine.Remove(kupovina);
                    kupac.Kupovine.Remove(kupovina);
                    s.SaveOrUpdate(salon);
                    s.SaveOrUpdate(kupac);

                    s.Flush();
                    IList<VoziloKojeJeProdato> vozila = kupovina.ProdataVozila;
                    kupovina.ProdataVozila = new List<VoziloKojeJeProdato>();


                    foreach (VoziloKojeJeProdato vkp in vozila)
                    {
                        RadnikTehnickeStruke rts = s.Load<RadnikTehnickeStruke>(vkp.RadnikTehnStruke.Jmbg);
                        rts.UvezenaVozila.Remove(vkp);
                        s.SaveOrUpdate(rts);
                        s.Flush();
                        s.Delete(vkp);
                        s.Flush();
                    }

                    s.Delete(kupovina);
                    s.Flush();
                }

                s.SaveOrUpdate(k);
                s.Flush();

                //Brisanje registrovanih kupaca
                IList<RegistrovaniKupac> vlasnici = k.RegistrovaniKupci;
                k.RegistrovaniKupci = new List<RegistrovaniKupac>();
                foreach (RegistrovaniKupac r in vlasnici)
                {
                    RegistrovaniKupac n = s.Load<RegistrovaniKupac>(r.Id);

                    IList<VozilaPrimljenaNaServis> vozila = n.JePoslaoVoziloNaServis;
                    n.JePoslaoVoziloNaServis = new List<VozilaPrimljenaNaServis>();

                    foreach (VozilaPrimljenaNaServis v in vozila)
                    {
                        v.Zaposleni.PrimioVoziloNaServis.Remove(v);
                        s.SaveOrUpdate(v.Zaposleni);
                        s.Delete(v);
                        s.Flush();
                    }

                    n.Kupac.RegistrovaniKupci.Remove(n);
                    s.SaveOrUpdate(n.Kupac);

                    s.Delete(n);
                    s.Flush();
                }

                s.SaveOrUpdate(k);
                s.Delete(k);
                s.Flush();


                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion

        #region Vlasnik

        #region NeregistrovaniKupac

        public static List<VlasnikNeregistrovaniKupacView> VratiNeregistrovaneKupce()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<VlasnikNeregistrovaniKupacView> vlasnik = new List<VlasnikNeregistrovaniKupacView>();

                IList<NeregistrovaniKupac> nk = s.QueryOver<NeregistrovaniKupac>()
                                         .List<NeregistrovaniKupac>();

                foreach (NeregistrovaniKupac n in nk)
                {
                    vlasnik.Add(new VlasnikNeregistrovaniKupacView(n));
                }

                s.Close();

                return vlasnik;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void AzurirajNeregistrovanogKupca(VlasnikNeregistrovaniKupacView vlasnik)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                NeregistrovaniKupac nk = s.Load<NeregistrovaniKupac>(vlasnik.Id);

                nk.Ime = vlasnik.Ime;
                nk.Prezime = vlasnik.Prezime;

                s.SaveOrUpdate(nk);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }

        }

        public static void DodajNeregistrovanogKupca(VlasnikNeregistrovaniKupacAddView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                NeregistrovaniKupac n = new NeregistrovaniKupac();
                n.Ime = r.Ime;
                n.Prezime = r.Prezime;

                s.Save(n);
                s.Flush();

                foreach(string tel in r.Telefoni)
                {
                    TelefonNeregistrovaniKupac t = new TelefonNeregistrovaniKupac();
                    t.BrojTelefona = tel;
                    t.NeregistrovaniKupac = n;
                    n.Telefoni.Add(t);
                    s.Save(t);
                    s.Save(n);
                    s.Flush();
                }

                s.Close();
            }
            catch(Exception ec)
            {
                throw;
            }
            
        }

        public static void IzbrisiNeregistrovanogKupca(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                NeregistrovaniKupac n = s.Load<NeregistrovaniKupac>(id);

                IList<TelefonNeregistrovaniKupac> telefoni = n.Telefoni;
                n.Telefoni = new List<TelefonNeregistrovaniKupac>();
                foreach (TelefonNeregistrovaniKupac tel in telefoni)
                {
                    s.Delete(tel);
                    s.Flush();
                }

                IList<VozilaPrimljenaNaServis> vozila = n.JePoslaoVoziloNaServis;
                n.JePoslaoVoziloNaServis = new List<VozilaPrimljenaNaServis>();

                foreach (VozilaPrimljenaNaServis v in vozila)
                {
                    v.Zaposleni.PrimioVoziloNaServis.Remove(v);
                    s.SaveOrUpdate(v.Zaposleni);
                    s.Delete(v);
                    s.Flush();
                }
                s.SaveOrUpdate(n);
                s.Flush();
                s.Delete(n);
                s.Flush();

                s.Close();


            }
            catch (Exception ec)
            {
                throw;
            }
        }


        #endregion

        #region RegistrovaniKupac

        public static List<VlasnikRegistrovaniKupacView> VratiRegistrovaneKupce()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<VlasnikRegistrovaniKupacView> vlasnik = new List<VlasnikRegistrovaniKupacView>();

                IList<RegistrovaniKupac> rk = s.QueryOver<RegistrovaniKupac>()
                                         .List<RegistrovaniKupac>();

                foreach (RegistrovaniKupac n in rk)
                {
                    vlasnik.Add(new VlasnikRegistrovaniKupacView(n));
                }

                s.Close();

                return vlasnik;
            }
            catch (Exception ec)
            {
                throw;
            }
        }


        public static void DodajRegistrovanogKupca(VlasnikRegistrovaniKupacAddView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                RegistrovaniKupac k = new RegistrovaniKupac();
                k.Kupac = s.Load<Kupac>(r.Id);
                k.Kupac.RegistrovaniKupci.Add(k);
                s.Save(k);
                s.Save(k.Kupac);
                s.Flush();


                s.Close();
            }
            catch(Exception ec)
            {
                throw;
            }
        }


        public static void IzbrisiRegistrovanogKupca(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                RegistrovaniKupac n = s.Load<RegistrovaniKupac>(id);

                IList<VozilaPrimljenaNaServis> vozila = n.JePoslaoVoziloNaServis;
                n.JePoslaoVoziloNaServis = new List<VozilaPrimljenaNaServis>();

                foreach (VozilaPrimljenaNaServis v in vozila)
                {
                    v.Zaposleni.PrimioVoziloNaServis.Remove(v);
                    s.SaveOrUpdate(v.Zaposleni);
                    s.Delete(v);
                    s.Flush();
                }

                n.Kupac.RegistrovaniKupci.Remove(n);
                s.SaveOrUpdate(n.Kupac);

                s.Delete(n);
                s.Flush();



                s.Close();


            }
            catch (Exception ec)
            {
                throw;
            }
        }


        #endregion

        public static List<VlasnikView> VratiVlasnike()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<VlasnikView> kupac = new List<VlasnikView>();

                IList<Vlasnik> vlasnik = s.QueryOver<Vlasnik>()
                                         .List<Vlasnik>();

                foreach (Vlasnik v in vlasnik)
                {
                    kupac.Add(new VlasnikView(v));
                }

                s.Close();

                return kupac;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion

        #region VozilaPrimljenaNaServis

        public static void DodajVoziloNaServis(VoziloPrimljenoNaServisAddView r)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                VozilaPrimljenaNaServis v = new VozilaPrimljenaNaServis();
                v.ModelVozila = r.Model;
                v.OpisProblema = r.OpisProblema;
                v.GodinaProizvodnje = r.GodinaProizvodnje;
                v.DatumPrijema = r.DatumPrijema;
                v.DatumZavrsetkaRadova = r.DatumZavrsetkaRadova;

                Zaposleni z = s.Load<Zaposleni>(r.JmbgZaposleni);
                Vlasnik vlasnik = s.Load<Vlasnik>(r.IdVlasnika);

                v.Zaposleni = z;
                z.PrimioVoziloNaServis.Add(v);

                v.Vlasnik = vlasnik;
                vlasnik.JePoslaoVoziloNaServis.Add(v);

                s.Save(v);
                s.Save(z);
                s.Save(vlasnik);
                s.Flush();


                s.Close();
            }
            catch(Exception ec)
            {
                throw;
            }
        }



        public static void IzbrisiVoziloSaServisa(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VozilaPrimljenaNaServis v = s.Load<VozilaPrimljenaNaServis>(id);

                v.Vlasnik.JePoslaoVoziloNaServis.Remove(v);
                s.SaveOrUpdate(v.Vlasnik);
                s.Flush();
                v.Zaposleni.PrimioVoziloNaServis.Remove(v);
                s.SaveOrUpdate(v.Zaposleni);
                s.Flush();
                s.Delete(v);
                s.Flush();


                s.Close();
            }
            catch(Exception ec)
            {
                throw;
            }
        }


        #endregion


        public static List<VozilaPrimljenaNaServisView> VratiVozilaPrimljenaNaSerivs()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                List<VozilaPrimljenaNaServisView> vozilaPrimljena = new List<VozilaPrimljenaNaServisView>();

                IList<VozilaPrimljenaNaServis> vozila = s.QueryOver<VozilaPrimljenaNaServis>()
                                         .List<VozilaPrimljenaNaServis>();

                foreach (VozilaPrimljenaNaServis v in vozila)
                {
                    vozilaPrimljena.Add(new VozilaPrimljenaNaServisView(v));
                }

                s.Close();

                return vozilaPrimljena;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static void AzurirajVozilaPrimljenaNaServis(VozilaPrimljenaNaServisView vozila)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VozilaPrimljenaNaServis vp = s.Load<VozilaPrimljenaNaServis>(vozila.RegistarskiBroj);

                vp.ModelVozila = vozila.Model;
                vp.GodinaProizvodnje = vozila.GodinaProizvodnje;
                vp.OpisProblema = vozila.OpisProblema;

                s.SaveOrUpdate(vp);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }

        }

    }
}
