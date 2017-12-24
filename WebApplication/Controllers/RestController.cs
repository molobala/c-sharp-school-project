using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Web.Http;
using System.Web.Mvc;
using DB;
using Entity;

namespace SearchEngineForTrip.Controllers
{
    public class RestController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCompaniesList()
        {
            using (var dao=new Dao.Dao())
            {
                return Json(dao.CompaniesList());
            }
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetVehiculesFor(int companyId)
        {
            using (var dao=new Dao.Dao())
            {
                return Json(dao.VehiculesListFor(companyId));
            }
        }
    }
}