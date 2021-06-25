using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    public class VlasnikView
    {
        public int Id { get; set; }
        public IList<VlasnikVoziloPrimljenoNaServisView> Vozila { get; set; }

        public VlasnikView()
        {
            Vozila = new List<VlasnikVoziloPrimljenoNaServisView>();
        }

        public VlasnikView(Vlasnik v)
        {
            Vozila = new List<VlasnikVoziloPrimljenoNaServisView>();
            Id = v.Id;
            foreach(VozilaPrimljenaNaServis vozilo in v.JePoslaoVoziloNaServis)
            {
                Vozila.Add(new VlasnikVoziloPrimljenoNaServisView(vozilo));
            }
        }
    }
}
