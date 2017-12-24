using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DB;
using Entity;

namespace Dao
{
    public  class Dao: IDisposable
    {
        public BDContext Context;

        public Dao()
        {
            Context=new BDContext();
        }
        // operation sur l'entité Vehicule
        public List<Entity.Vehicule> VehiculesList()
        {
            return Context.Vehicules
                .Include(v=>v.Company)
                .ToList();
        }

        public Entity.Vehicule GetOneHVehicule(int id)
        {
            try
            {
                return Context.Vehicules.FirstOrDefault(v => v.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<Vehicule> VehiculesListFor(int companyId)
        {
            return Context.Vehicules.Where(v => v.CompanyId == companyId)
                .Include(v=>v.Company)
                .ToList();
        }

        public Entity.Vehicule InsertVehicule(Entity.Vehicule v)
        {
            v=Context.Vehicules.Add(v);
            Context.SaveChanges();
            return v;
        }

        public Entity.Vehicule UpdateVehicule(Entity.Vehicule v)
        {
            Context.Vehicules.AddOrUpdate(v);
            Context.SaveChanges();
            return v;
        }

        public void DeleteVehicule(int id)
        {
            Context.Vehicules.Remove(GetOneHVehicule(id));
            Context.SaveChanges();
        }

        //operation sur  company

        public List<Entity.Company> CompaniesList()
        {
            return Context.Companies
                .ToList();
        }

        public Entity.Company GetOneCompany(int id)
        {
            try
            {
                return Context.Companies.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Entity.Company UpdateCompany(Entity.Company c)
        {
            Context.Companies.AddOrUpdate(c);
            Context.SaveChanges();
            return c;
        }

        public Entity.Company InsertCompany(Entity.Company v)
        {
            v=Context.Companies.Add(v);
            Context.SaveChanges();
            return v;
        }

        public void DeleteCompany(int id)
        {
            Context.Companies.Remove(GetOneCompany(id));
            Context.SaveChanges();
        }

        //Operation on Ville

        public List<Entity.Ville> VillesList()
        {
            return Context.Villes.ToList();
        }

        public Entity.Ville GetOneVille(int id)
        {
            try
            {
                return Context.Villes.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Entity.Ville UpdateVille(Entity.Ville c)
        {
            Context.Villes.AddOrUpdate(c);
            Context.SaveChanges();
            return c;
        }

        public Entity.Ville InsertVille(Entity.Ville v)
        {
            v=Context.Villes.Add(v);
            Context.SaveChanges();
            return v;
        }

        public void DeleteVille(int id)
        {
            Context.Villes.Remove(GetOneVille(id));
            Context.SaveChanges();
        }

        //Operation sur voyage

        public List<Entity.Voyage> VoyagesList()
        {
            return Context.Voyages.ToList();
        }

        public Entity.Voyage GetOneVoyage(int id)
        {
            try
            {
                return Context.Voyages.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Entity.Voyage UpdateVoyage(Entity.Voyage c)
        {
            Context.Voyages.AddOrUpdate(c);
            Context.SaveChanges();
            return c;
        }

        public Entity.Voyage InsertVoyage(Entity.Voyage v)
        {
            v=Context.Voyages.Add(v);
            Context.SaveChanges();
            return v;
        }

        public void DeleteVoyage(int id)
        {
            Context.Voyages.Remove(GetOneVoyage(id));
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public List<Voyage> VoyagesListFor(string depart, string arrival, DateTime departTime,string type)
        {
            return Context.Voyages
                .Include(v=>v.Company)
                .Include(v=>v.Depart)
                .Include(v=>v.Arrival)
                .ToList()
                .Where(v =>v.Arrival.Name == arrival && v.Depart.Name== depart)
                .Where(v => (v.DepartTime.AddHours(1) >= departTime && v.DepartTime.Subtract(new TimeSpan(60*60*100))<=departTime
                             && (type==null || v.Type==type)))
                .ToList();

        } 
        public List<Voyage> VoyagesListFor(string depart, string arrival,string type)
        {
            return Context.Voyages
                .Include(v=>v.Company)
                .Include(v=>v.Depart)
                .Include(v=>v.Arrival)
                .ToList()
                .Where(v =>v.Arrival.Name == arrival && v.Depart.Name == depart
                           && (type==null || v.Type==type))
                .OrderBy(v=>v.DepartTime)
                .ToList();

        }
        //User
        public List<User> UsersList()
        {
            return Context.Users
                .ToList();
        }

        public User GetOneUser(int id)
        {
            try
            {
                return Context.Users.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public User UpdateUser(User c)
        {
            Context.Users.AddOrUpdate(c);
            Context.SaveChanges();
            return c;
        }

        public User InsertUser(User v)
        {
            v=Context.Users.Add(v);
            Context.SaveChanges();
            return v;
        }

        public void DeleteUser(int id)
        {
            Context.Users.Remove(GetOneUser(id));
            Context.SaveChanges();
        }

        public User GetOneUser(string email,string cryptedPassword)
        {
            return Context.Users.FirstOrDefault(u => u.Email == email && u.Password == cryptedPassword);
        }

        public User NewUser(User u)
        {
            u.Password = EncodeMD5(u.Password);
            return InsertUser(u);
        }
        public User Authentificate(string email, string password)
        {
            return GetOneUser(email, EncodeMD5(password));
        }
        public User GetOneUser(string email)
        {
            return Context.Users.FirstOrDefault(u => u.Email == email);
        }
        
        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "Molobala" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}