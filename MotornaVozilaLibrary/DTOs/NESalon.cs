using MotornaVozila.Entiteti;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotornaVozilaLibrary.DTOs
{
    //NezavisniEkonomistaView ce imati listu NESalon-a
    public class NESalon
    {
        public int Id { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public string SefSalona { get; set; }
        public string SefServisa { get; set; }

        public NESalon(Salon salon)
        {
            Id = salon.Id;
            Grad = salon.Grad;
            Adresa = salon.Adresa;
            SefSalona = salon.SefSalona;
            SefServisa = salon.SefServisa;
        }
    }
}
