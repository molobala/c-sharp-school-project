using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Mvc;
using System.Web.WebPages;
using Entity;

namespace SearchEngineForTrip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var dao=new Dao.Dao())
            {
                var vehiculeTypes=new SelectList(new List<Object>
                {
                    new {Id="ALL",Name="Tous"},
                    new {Id="BUS",Name="Bus"},
                    new {Id="TRAIN",Name="Train"},
                    new {Id="AVION",Name="Avion"},
                    new {Id="TAXI",Name="Taxi"},
                },"Id","Name");
                ViewBag.typesList = vehiculeTypes;
                ViewBag.justHome = true;
                return View(dao.VillesList());
            }
        }

        public ActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Search()
        {
            var data = Request.Form;
            var date = data["DepartTime"];
            var type = data["Type"];
            ViewBag.Type = type?.ToLower();
            if (type == "ALL") type = null;
            var depart = (data["Depart"]);
            var arrival = (data["Arrival"]);
            Console.WriteLine(data["Depart"]+"==>"+data["Arrival"]+";"+data["DepartTime"]);
            ViewBag.Depart = depart;
            ViewBag.Arrival = arrival;
            using (var dao=new Dao.Dao())
            {
                List<Voyage> list;
                if (date == null || date.IsEmpty() || string.IsNullOrWhiteSpace(date))
                {
                    list = dao.VoyagesListFor(depart, arrival,DateTime.Now,type);
                    //departTime = DateTime.Now;
                    ViewBag.DepartTime = null;
                }
                else
                {
                    DateTime departTime ;
                    try
                    {
                        departTime =DateTime.Parse(date);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return Redirect("/");
                    }
                    list = dao.VoyagesListFor(depart, arrival,departTime ,type);
                    ViewBag.DepartTime = departTime;
                }
                if (list.Count>0)
                {
                    ViewBag.Depart = list[0].Depart;
                    ViewBag.Arrival = list[0].Arrival;
                }
                ViewBag.ListVoyages = list;
                return View();
            }
        }
    }
}