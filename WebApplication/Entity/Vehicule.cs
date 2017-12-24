
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Web.UI.WebControls;

namespace Entity
{
    [Table("Vehicules")]
    public class Vehicule
    {
        public Vehicule()
        {
        }

        public Vehicule(int id, string name, Company company)
        {
            Id = id;
            Name = name;
            Company = company;
        }
        //navigation
        
        public virtual Company Company { get; set; }
        //attriutes
        
        public int Id { get; set; }
        [MaxLength(50,ErrorMessage ="50 caractères depassés!!"),MinLength(1,ErrorMessage = "Il faut minimum 1 caractère")]
        [DisplayName("Le nom du vehicule")]
        [Required]
        public string Name { get; set; }
        [DisplayName("La description du vehicule")]
        public string Description { get; set; }
        [ForeignKey("Company")]
        [Required]
        public int CompanyId { get; set; }
        [DisplayName("Le type du vehicule"),DefaultValue("BUS")]
        [Required(ErrorMessage = "Veillez specifier le type du vehicule")]
        public string Type { get; set; }
    }
}