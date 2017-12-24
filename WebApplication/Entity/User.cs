using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Entity
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(50,ErrorMessage = "Le nom ne peut depasser 50 caractères"),MinLength(2,ErrorMessage = "Le nom ne peut être moins de 2 caractères")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-z][a-z0-9\\.]*@[a-z0-9]+\\.[a-z]{2,6}",ErrorMessage = "L'email est incorrecte")]
        public string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage = "Le mot de passe doit depasser 6 caractères")]
        public string Password { get; set; }
    }
}