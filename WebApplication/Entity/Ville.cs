using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("Villes")]
    public class Ville
    {
        public Ville()
        {
        }

        public Ville(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        [DisplayName("Le nom de la ville")]
        [MinLength(2,ErrorMessage = "Le nom d'une ville ne peut etre inferieur à 2 caractères")]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}