using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using InventoryApi;

namespace InventoryApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using InventoryApi;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Lkup_Fin_Year>("Lkup_Fin_Year");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Lkup_Fin_YearController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Lkup_Fin_Year
        [EnableQuery]
        public IQueryable<Lkup_Fin_Year> GetLkup_Fin_Year()
        {
            return db.Lkup_Fin_Year.Where(Lkup_Fin_Year => Lkup_Fin_Year.ACTIVE == "Y"); 
        }

        // GET: odata/Lkup_Fin_Year(5)
        [EnableQuery]
        public SingleResult<Lkup_Fin_Year> GetLkup_Fin_Year([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Lkup_Fin_Year.Where(lkup_Fin_Year => lkup_Fin_Year.FIN_YEAR_ID == key));
        }

        // PUT: odata/Lkup_Fin_Year(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Lkup_Fin_Year> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Fin_Year lkup_Fin_Year = db.Lkup_Fin_Year.Find(key);
            if (lkup_Fin_Year == null)
            {
                return NotFound();
            }

            patch.Put(lkup_Fin_Year);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Fin_YearExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Fin_Year);
        }

        // POST: odata/Lkup_Fin_Year
        public IHttpActionResult Post(Lkup_Fin_Year lkup_Fin_Year)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lkup_Fin_Year.Add(lkup_Fin_Year);
            db.SaveChanges();

            return Created(lkup_Fin_Year);
        }

        // PATCH: odata/Lkup_Fin_Year(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Lkup_Fin_Year> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Fin_Year lkup_Fin_Year = db.Lkup_Fin_Year.Find(key);
            if (lkup_Fin_Year == null)
            {
                return NotFound();
            }

            patch.Patch(lkup_Fin_Year);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Fin_YearExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Fin_Year);
        }

        // DELETE: odata/Lkup_Fin_Year(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Lkup_Fin_Year lkup_Fin_Year = db.Lkup_Fin_Year.Find(key);
            lkup_Fin_Year.ACTIVE = "N";
            if (lkup_Fin_Year == null)
            {
                return NotFound();
            }

            db.Entry(lkup_Fin_Year).State = EntityState.Modified;
            //db.Lkup_Fin_Year.Remove(lkup_Fin_Year);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Lkup_Fin_YearExists(decimal key)
        {
            return db.Lkup_Fin_Year.Count(e => e.FIN_YEAR_ID == key) > 0;
        }
    }
}
