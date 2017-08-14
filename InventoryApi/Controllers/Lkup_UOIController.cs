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
    builder.EntitySet<Lkup_UOI>("Lkup_UOI");
    builder.EntitySet<IssueDetail>("IssueDetails"); 
    builder.EntitySet<Item>("Items"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Lkup_UOIController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Lkup_UOI
        [EnableQuery]
        public IQueryable<Lkup_UOI> GetLkup_UOI()
        {
            return db.Lkup_UOI.Where(Lkup_UOI => Lkup_UOI.ACTIVE == "Y");
        }

        // GET: odata/Lkup_UOI(5)
        [EnableQuery]
        public SingleResult<Lkup_UOI> GetLkup_UOI([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Lkup_UOI.Where(lkup_UOI => lkup_UOI.UOI_ID == key));
        }

        // PUT: odata/Lkup_UOI(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Lkup_UOI> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_UOI lkup_UOI = db.Lkup_UOI.Find(key);
            if (lkup_UOI == null)
            {
                return NotFound();
            }

            patch.Put(lkup_UOI);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_UOIExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_UOI);
        }

        // POST: odata/Lkup_UOI
        public IHttpActionResult Post(Lkup_UOI lkup_UOI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lkup_UOI.Add(lkup_UOI);
            db.SaveChanges();

            return Created(lkup_UOI);
        }

        // PATCH: odata/Lkup_UOI(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Lkup_UOI> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_UOI lkup_UOI = db.Lkup_UOI.Find(key);
            if (lkup_UOI == null)
            {
                return NotFound();
            }

            patch.Patch(lkup_UOI);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_UOIExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_UOI);
        }

        // DELETE: odata/Lkup_UOI(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Lkup_UOI lkup_UOI = db.Lkup_UOI.Find(key);
            lkup_UOI.ACTIVE = "N";
            if (lkup_UOI == null)
            {
                return NotFound();
            }

            db.Entry(lkup_UOI).State = EntityState.Modified;
            //db.Lkup_UOI.Remove(lkup_UOI);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Lkup_UOI(5)/IssueDetails
        [EnableQuery]
        public IQueryable<IssueDetail> GetIssueDetails([FromODataUri] decimal key)
        {
            return db.Lkup_UOI.Where(m => m.UOI_ID == key).SelectMany(m => m.IssueDetails);
        }

        // GET: odata/Lkup_UOI(5)/Items
        [EnableQuery]
        public IQueryable<Item> GetItems([FromODataUri] decimal key)
        {
            return db.Lkup_UOI.Where(m => m.UOI_ID == key).SelectMany(m => m.Items);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Lkup_UOIExists(decimal key)
        {
            return db.Lkup_UOI.Count(e => e.UOI_ID == key) > 0;
        }
    }
}
