using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Entity;

namespace DB
{
    public class DBInitializer:DropCreateDatabaseIfModelChanges<BDContext>
    {
        protected override void Seed(BDContext context)
        {
            base.Seed(context);
            SeedAdminUser(context);
            SeedCompanies(context);
            SeedVilles(context);
        }

        public  void SeedCompanies(BDContext context)
        {
            context.Companies.Add(new Company
            {
                Id = 0,
                Description = "La compagnie par defaut",
                Name = "Ensa Transport",
            });
            context.SaveChanges();
        }

        public  void SeedVilles(BDContext context)
        {
            context.Villes.AddRange(new List<Ville>
            {
                new Ville{Name = "Tetouan",Description = "Une ville au Nord du Maroc"},
                new Ville{Name = "Tanger",Description = ""},
                new Ville{Name = "Rabat",Description = ""},
                new Ville{Name = "Casabalanca",Description = ""},
                new Ville{Name = "Fès",Description = ""},
                new Ville{Name = "Marrackech",Description = ""},
                new Ville{Name = "Agadir",Description = ""},
                new Ville{Name = "Kenitra",Description = ""},
                new Ville{Name = "Kenifra",Description = ""},
                new Ville{Name = "Ifrane",Description = ""},
                new Ville{Name = "Oujda",Description = ""},
            });
            context.SaveChanges();
        }
        public void SeedAdminUser(BDContext context)
        {
            var user = new User
            {
                Email = "admin00@admin.com",
                Password = Utils.EncodeMD5("molobala"),
                Name = "Admin",
                Id = 0
            };
            Console.WriteLine("Initialising ............");
            try
            {
                if (context.Users.First(u=>u.Email==user.Email)==null) return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Initialising ............");
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}