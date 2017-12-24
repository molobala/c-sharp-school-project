using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using Entity;
using Company = Entity.Company;

namespace SearchEngineForTrip.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET
        [Authorize]
        public ActionResult Index()
        {
            //get user companies list
            using (var dao=new Dao.Dao())
            {
                List<Company> companies=dao.CompaniesList();
                ViewData["vehicules"] = dao.VehiculesList();
                ViewData["companies"] = companies;
                ViewData["villes"] = dao.VillesList();
                ViewData["voyages"] = dao.VoyagesList();
                ViewBag.listCompanies = new SelectList(companies,"Id","Name");
            }
            return View("index");
        }
        [Authorize]
        public ActionResult SaveCompany()
        {
            var company=new Entity.Company
            {
                //Id = Convert.ToInt32(this.Request.Form["cid"]),
                Name = this.Request.Form["cname"],
                Description = this.Request.Form["cdesc"],
            };
            ViewBag.post = true;
            ViewBag.postInfo = "Company saved successfuly";
            ViewBag.postInfoClass = "alert-success";
            if (company.Name.IsEmpty())
            {
                ViewBag.postInfo = "Error while saving!!Company name can't be blanc, please specify the company name";
                ViewBag.postInfoClass = "alert-danger";
            }
            else
            {
                Console.WriteLine(company.Id+";"+company.Name+";"+company.Description);
                using (var dao=new Dao.Dao())
                {
                    dao.InsertCompany(company);
                }
            }
            return View("Saved");
        }
        
        [Authorize]
        public ActionResult EditCompany(int id)
        {
            ViewBag.Title = "Edit Company";
            using (var dao=new Dao.Dao())
            {
                return View("editCompany",dao.GetOneCompany(id));
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditCompany(Company company)
        {
            ViewBag.Title = "Edit Company";
            Console.WriteLine(company.Id+";"+company.Name+";"+company.Description);
            ViewBag.postInfo = "Company saved successfuly";
            ViewBag.postInfoClass = "alert-success";
            if (!ModelState.IsValid)
            {
                //ViewBag.Errors.Name=ModelState["Name"].Errors[0].ErrorMessage+"\n";
                //ViewBag.postInfoClass = "alert-danger";
                return View(company);
            }
            using (var dao=new Dao.Dao())
            {
                dao.UpdateCompany(company);
            }
            return View("Saved");
        }
        [Authorize]
        public ActionResult DeleteCompany(int id)
        {
             using (var dao=new Dao.Dao())
             {
                 dao.DeleteCompany(id);
                 return RedirectToAction("index");
             }
        }
        [Authorize]
        public ActionResult SaveVehicule(Vehicule vehicule)
        {
            ViewBag.Title = "Ajout de vehicule";
            ViewBag.postInfo = "Company saved successfuly";
            ViewBag.postInfoClass = "alert-success";
            Console.WriteLine(vehicule.Id+";"+vehicule.Name+";"+vehicule.Description+";"+vehicule.CompanyId);
            if (!ModelState.IsValid)
            {
                var errors=ModelState.Values.SelectMany(v => v.Errors);
                ViewBag.postInfo = errors;
                foreach (var v in errors)
                {
                    Console.WriteLine("Error"+v.ErrorMessage);
                }
                ViewBag.postInfoClass = "alert-danger";
            }
            else
            {
                using (var dao=new Dao.Dao())
                {
                    dao.InsertVehicule(vehicule);
                }
            }
            return View("Saved");
        }
        [Authorize]
        public ActionResult EditVehicule(int? id)
        {
            ViewBag.Title = "Edit Company";
            using (var dao = new Dao.Dao())
            {
                var companiesList = dao.CompaniesList();
                var vehiculeTypes=new SelectList(new List<Object>
                {
                    new {Id="BUS",Name="Bus"},
                    new {Id="TRAIN",Name="Train"},
                    new {Id="AVION",Name="Avion"},
                    new {Id="TAXI",Name="Taxi"},
                },"Id","Name");
                ViewBag.vehiculeTypes = vehiculeTypes;
                ViewBag.listCompanies = new SelectList(companiesList, "Id", "Name");
                Vehicule vehicule;
                if (id != null)
                {
                     vehicule= dao.GetOneHVehicule((int)id);
                    if (vehicule != null)
                    {
                        return View("editVehicule", vehicule);
                    }
                }
                vehicule=new Vehicule();
                return View("editVehicule", vehicule);
            }
            
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditVehicule(Entity.Vehicule vehicule)
        {
            ViewBag.Title = "Edit Company";
            ViewBag.postInfo = "Company saved successfuly";
            ViewBag.postInfoClass = "alert-success";
            Console.WriteLine(vehicule.Id+";"+vehicule.Name+";"+vehicule.Description);
            using (var dao = new Dao.Dao())
            {
                if (!ModelState.IsValid)
                {
                    var companiesList = dao.CompaniesList();
                    var vehiculeTypes=new SelectList(new List<Object>
                    {
                        new {Id="BUS",Name="BUS"},
                        new {Id="TRAIN",Name="Train"},
                        new {Id="AVION",Name="Avion"},
                        new {Id="TAXI",Name="Taxi"},
                    },"Id","Name");
                    ViewBag.vehiculeTypes = vehiculeTypes;
                    ViewBag.listCompanies = new SelectList(companiesList,"Id","Name");
                    return View(vehicule);
                }
                dao.UpdateVehicule(vehicule);
                return View("Saved");
            }
        }
        [Authorize]
        public ActionResult DeleteVehicule(int id)
        {
            using (var dao=new Dao.Dao())
             {
                 dao.DeleteVehicule(id);
                 return RedirectToAction("index");
             }
        }
        [Authorize]
        [HttpPost]
        public ActionResult SaveVille(Ville value)
        {
            ViewBag.Title = "Ajout de vehicule";
            ViewBag.postInfo = "Company saved successfuly";
            ViewBag.postInfoClass = "alert-success";
            Console.WriteLine(value.Id+";"+value.Name+";"+value.Description+";");
            if (!ModelState.IsValid)
            {
                var errors=ModelState.Values.SelectMany(v => v.Errors);
                ViewBag.postInfo = errors;
                foreach (var v in errors)
                {
                    Console.WriteLine("Error"+v.ErrorMessage);
                }
                ViewBag.postInfoClass = "alert-danger";
            }
            else
            {
                using (var dao=new Dao.Dao())
                {
                    dao.InsertVille(value);
                }
            }
            return View("Saved");
        }
        [Authorize]
        public ActionResult EditVille(int? id)
        {
            ViewBag.Title = "Edit Company";
            using (var dao = new Dao.Dao())
            {
                if (id != null)
                {
                    var ville = dao.GetOneVille((int)id);
                    if (ville != null)
                        return View("editVille", ville);
                    else return View(new Ville());
                }
                return View(new Ville());
            }
            
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditVille(Entity.Ville value)
        {
            ViewBag.Title = "Edit Ville";
            ViewBag.postInfo = "Ville saved successfuly";
            ViewBag.postInfoClass = "alert-success";
            Console.WriteLine(value.Id+";"+value.Name+";"+value.Description);
            using (var dao = new Dao.Dao())
            {
                if (!ModelState.IsValid)
                {
                    return View(value);
                }
                dao.UpdateVille(value);
                return View("Saved");
            }
        }
        [Authorize]
        public ActionResult DeleteVille(int id)
        {
            using (var dao=new Dao.Dao())
             {
                 dao.DeleteVille(id);
                 return RedirectToAction("index");
             }
        }
        
        //End Ville
        //Voyage
        [Authorize]
        [HttpPost]
        public ActionResult SaveVoyage(Voyage value)
        {
            ViewBag.Title = "Ajout de vehicule";
            ViewBag.postInfo = "Company saved successfuly";
            ViewBag.postInfoClass = "alert-success";
            Console.WriteLine(value.Id+";"+value.DepartId+";"+value.ArrivalId+";"+value.DepartTime+";"+value.ArrivalTime+";"+value.CompanyId);
            if (!ModelState.IsValid)
            {
                var errors=ModelState.Values.SelectMany(v => v.Errors);
                ViewBag.postInfo = errors;
                foreach (var v in errors)
                {
                    Console.WriteLine("Error"+v.ErrorMessage);
                }
                ViewBag.postInfoClass = "alert-danger";
            }
            else
            {
                using (var dao=new Dao.Dao())
                {
                    dao.InsertVoyage(value);
                }
            }
            return View("Saved");
        }
        [Authorize]
        public ActionResult EditVoyage(int? id)
        {
            ViewBag.Title = "Edit Voyage";
            var vehiculeTypes=new SelectList(new List<Object>
            {
                new {Id="BUS",Name="Bus"},
                new {Id="TRAIN",Name="Train"},
                new {Id="AVION",Name="Avion"},
                new {Id="TAXI",Name="Taxi"},
            },"Id","Name");
            ViewBag.vehiculeTypes = vehiculeTypes;
            using (var dao = new Dao.Dao())
            {
                var listCompanies= dao.CompaniesList();
                ViewBag.listVilles = new SelectList(dao.VillesList(),"Id","Name");
                ViewBag.listCompanies = new SelectList(listCompanies, "Id", "Name");
                var listVehicules = listCompanies.Count > 0
                    ? dao.VehiculesListFor(listCompanies[0].Id)
                    : new List<Vehicule>();
                ViewBag.listVehicules = new SelectList(listVehicules,"Id","Name");
                if (id != null)
                {
                    var v = dao.GetOneVoyage((int)id);
                    if (v != null) return View("editVoyage", v);
                    var vo = new Voyage {DepartTime = DateTime.Now,ArrivalTime= DateTime.Now.AddHours(1)};
                    return View(vo);
                }
                else
                {
                    var vo = new Voyage {DepartTime = DateTime.Now,ArrivalTime= DateTime.Now.AddHours(1)};
                   return View(vo);
                }
            }
            
        }
        [HttpPost]
        [Authorize]
        public ActionResult EditVoyage(Entity.Voyage value)
        {
            ViewBag.Title = "Edit Voyage";
            ViewBag.postInfo = "L'enregistrement voyage saved successfuly";
            ViewBag.postInfoClass = "alert-success";
            var vehiculeTypes=new SelectList(new List<Object>
            {
                new {Id="BUS",Name="Bus"},
                new {Id="TRAIN",Name="Train"},
                new {Id="AVION",Name="Avion"},
                new {Id="TAXI",Name="Taxi"},
            },"Id","Name");
            ViewBag.vehiculeTypes = vehiculeTypes;
            Console.WriteLine(value.Id+";"+value.DepartId+";"+value.ArrivalId+";"+value.DepartTime+";"+value.ArrivalTime+";"+value.CompanyId);
            using (var dao = new Dao.Dao())
            {
                var listCompanies= dao.CompaniesList();
                ViewBag.listVilles = new SelectList(dao.VillesList(),"Id","Name");
                ViewBag.listCompanies = new SelectList(listCompanies, "Id", "Name");
                var listVehicules = listCompanies.Count > 0
                    ? dao.VehiculesListFor(listCompanies[0].Id)
                    : new List<Vehicule>();
                ViewBag.listVehicules = new SelectList(listVehicules,"Id","Name");
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Error, model non valid");
                    ViewBag.Error = "Model non valid";
                    return View(value);
                }
                if (value.ArrivalTime<value.DepartTime)
                {
                    ModelState.AddModelError("DeparTime","La date d'arrivé ne peut être superieur à la date de depart");
                    return View(value); 
                }
                dao.UpdateVoyage(value);
                return View("Saved");
            }
        }
        [Authorize]
        public ActionResult DeleteVoyage(int id)
        {
            using (var dao=new Dao.Dao())
             {
                 dao.DeleteVoyage(id);
                 return RedirectToAction("index");
             }
        }
        
        //End Ville
        [Authorize]
        public ActionResult Vehicule()
        {
            return View();
        }
        [Authorize]
        public ActionResult Company()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            var viewModel = new UserViewModel{IsAuthenticated = HttpContext.User.Identity.IsAuthenticated};
            using (var dao=new Dao.Dao())
            {
                if (viewModel.IsAuthenticated)
                {
                    viewModel.User = dao.GetOneUser(HttpContext.User.Identity.Name);
                }
                return View(viewModel);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserViewModel userViewModel,string returnUrl)
        {
            using (var dao=new Dao.Dao())
            {
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Email:"+userViewModel.User.Email+";;"+userViewModel.User.Password);
                    var u = dao.Authentificate(userViewModel.User.Email, userViewModel.User.Password);
                    foreach (var us in dao.UsersList())
                    {
                        Console.WriteLine("U.Name"+us.Name+";U.Email"+us.Email+";U.password"+DB.Utils.EncodeMD5( us.Password)+"=="+DB.Utils.EncodeMD5(userViewModel.User.Password));
                    }
                    if (u != null)
                    {
                        FormsAuthentication.SetAuthCookie(u.Email,false);
                        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return Redirect("/admin");
                    }
                }
                foreach (var v in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Error"+v.ErrorMessage);
                }
                ModelState.AddModelError("User.Email","Mot de passe ou email incorrecte");
                return View(userViewModel);
            }
        }

        public ActionResult AddNewUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewUser(User user)
        {
            using (var dao=new Dao.Dao())
            {
                if (ModelState.IsValid)
                {
                    user = dao.NewUser(user);
                    ViewBag.postInfoClass = "alert alert-success";
                    ViewBag.postInfo = "Utilisateur ajouté avec succès";
                    return View("Saved");
                }
                return View(user);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/admin"); 
        }
    }
}