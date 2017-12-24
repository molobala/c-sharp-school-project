using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace Entity
{
    [Table("Companies")]
    public class Company
    {
        public Company()
        {
            Vehicules = new HashSet<Vehicule>();
        }

        public Company(int id, string name,string icon)
        {
            Id = id;
            Name = name;
            this.Icon = icon;
            Vehicules = new HashSet<Vehicule>();
        }
        [DisplayName("Nom de la compagnie")]
        [MaxLength(50,ErrorMessage = "Le nom d'une compagnie ne peut ne peut depassé 50 caracteres"),
         MinLength(2,ErrorMessage = "Le nom d'une compagnie ne peut pas contenir moins de 2 caracter")]
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
        [DisplayName("Description de la compagnie")]
        public string Description { get; set; }
        [StringLength(100),DefaultValue("company-icon.png")]
        public string Icon { get; set; }
        //navigagtion
        [JsonIgnore]
        public virtual ICollection<Vehicule> Vehicules { get; set; }
    }
}