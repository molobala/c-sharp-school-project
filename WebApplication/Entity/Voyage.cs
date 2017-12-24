using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Entity
{
    [Table("Voyages")]
    public class Voyage
    {   
        [DisplayName("La date d'arrive")]
        public DateTime ArrivalTime { get; set; }
        [DisplayName("La date de depart")]
        public DateTime DepartTime { get; set; }
        [DisplayName("Le nom de la compagnie")]
        public int CompanyId { get; set; }
        [DisplayName("Ville de depart")]
        public int DepartId { get; set; }
        [DisplayName("Ville d'arrivee")]
        public int ArrivalId { get; set; }
        [DefaultValue("BUS")]
        public string Type { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Le prix d'un voyage est necessaire à specifier")]
        public double Price { get; set; }
        public virtual Company Company { get; set; }
        public virtual Ville Depart { get; set; }
        public virtual Ville Arrival { get; set; }
    }
}