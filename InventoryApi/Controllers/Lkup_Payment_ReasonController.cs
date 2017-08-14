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
    builder.EntitySet<Lkup_Payment_Reason>("Lkup_Payment_Reason");
    builder.EntitySet<Payment>("Payments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Lkup_Payment_ReasonController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Lkup_Payment_Reason
        [EnableQuery]
        public IQueryable<Lkup_Payment_Reason> GetLkup_Payment_Reason()
        {
            return db.Lkup_Payment_Reason.Where(Lkup_Payment_Reason => Lkup_Payment_Reason.ACTIVE == "Y");
        }

        // GET: odata/Lkup_Payment_Reason(5)
        [EnableQuery]
        public SingleResult<Lkup_Payment_Reason> GetLkup_Payment_Reason([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Lkup_Payment_Reason.Where(lkup_Payment_Reason => lkup_Payment_Reason.REASON_ID == key));
        }

        // PUT: odata/Lkup_Payment_Reason(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Lkup_Payment_Reason> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Payment_Reason lkup_Payment_Reason = db.Lkup_Payment_Reason.Find(key);
            if (lkup_Payment_Reason == null)
            {
                return NotFound();
            }

            patch.Put(lkup_Payment_Reason);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Payment_ReasonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Payment_Reason);
        }

        // POST: odata/Lkup_Payment_Reason
        public IHttpActionResult Post(Lkup_Payment_Reason lkup_Payment_Reason)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lkup_Payment_Reason.Add(lkup_Payment_Reason);
            db.SaveChanges();

            return Created(lkup_Payment_Reason);
        }

        // PATCH: odata/Lkup_Payment_Reason(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Lkup_Payment_Reason> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Payment_Reason lkup_Payment_Reason = db.Lkup_Payment_Reason.Find(key);
            if (lkup_Payment_Reason == null)
            {
                return NotFound();
            }

            patch.Patch(lkup_Payment_Reason);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Payment_ReasonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Payment_Reason);
        }

        // DELETE: odata/Lkup_Payment_Reason(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Lkup_Payment_Reason lkup_Payment_Reason = db.Lkup_Payment_Reason.Find(key);
            if (lkup_Payment_Reason == null)
            {
                return NotFound();
            }

            lkup_Payment_Reason.ACTIVE = "N";
           
            db.Entry(lkup_Payment_Reason).State = EntityState.Modified;
            //db.Lkup_Payment_Reason.Remove(lkup_Payment_Reason);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Lkup_Payment_Reason(5)/Payments
        [EnableQuery]
        public IQueryable<Payment> GetPayments([FromODataUri] decimal key)
        {
            return db.Lkup_Payment_Reason.Where(m => m.REASON_ID == key).SelectMany(m => m.Payments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Lkup_Payment_ReasonExists(decimal key)
        {
            return db.Lkup_Payment_Reason.Count(e => e.REASON_ID == key) > 0;
        }
    }
}
