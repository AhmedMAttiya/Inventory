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
    builder.EntitySet<Lkup_Item_Status>("Lkup_Item_Status");
    builder.EntitySet<IssueDetail>("IssueDetails"); 
    builder.EntitySet<StockDetail>("StockDetails"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Lkup_Item_StatusController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Lkup_Item_Status
        [EnableQuery]
        public IQueryable<Lkup_Item_Status> GetLkup_Item_Status()
        {
            return db.Lkup_Item_Status.Where(Lkup_Item_Status => Lkup_Item_Status.ACTIVE == "Y"); 
        }

        // GET: odata/Lkup_Item_Status(5)
        [EnableQuery]
        public SingleResult<Lkup_Item_Status> GetLkup_Item_Status([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Lkup_Item_Status.Where(lkup_Item_Status => lkup_Item_Status.ITEM_STATUS_ID == key));
        }

        // PUT: odata/Lkup_Item_Status(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Lkup_Item_Status> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Item_Status lkup_Item_Status = db.Lkup_Item_Status.Find(key);
            if (lkup_Item_Status == null)
            {
                return NotFound();
            }

            patch.Put(lkup_Item_Status);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Item_StatusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Item_Status);
        }

        // POST: odata/Lkup_Item_Status
        public IHttpActionResult Post(Lkup_Item_Status lkup_Item_Status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lkup_Item_Status.Add(lkup_Item_Status);
            db.SaveChanges();

            return Created(lkup_Item_Status);
        }

        // PATCH: odata/Lkup_Item_Status(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Lkup_Item_Status> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Item_Status lkup_Item_Status = db.Lkup_Item_Status.Find(key);
            if (lkup_Item_Status == null)
            {
                return NotFound();
            }

            patch.Patch(lkup_Item_Status);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Item_StatusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Item_Status);
        }

        // DELETE: odata/Lkup_Item_Status(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Lkup_Item_Status lkup_Item_Status = db.Lkup_Item_Status.Find(key);
            if (lkup_Item_Status == null)
            {
                return NotFound();
            }

            lkup_Item_Status.ACTIVE = "N";

            db.Entry(lkup_Item_Status).State = EntityState.Modified;
            //db.Lkup_Item_Status.Remove(lkup_Item_Status);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Lkup_Item_Status(5)/IssueDetails
        [EnableQuery]
        public IQueryable<IssueDetail> GetIssueDetails([FromODataUri] decimal key)
        {
            return db.Lkup_Item_Status.Where(m => m.ITEM_STATUS_ID == key).SelectMany(m => m.IssueDetails);
        }

        // GET: odata/Lkup_Item_Status(5)/StockDetails
        [EnableQuery]
        public IQueryable<StockDetail> GetStockDetails([FromODataUri] decimal key)
        {
            return db.Lkup_Item_Status.Where(m => m.ITEM_STATUS_ID == key).SelectMany(m => m.StockDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Lkup_Item_StatusExists(decimal key)
        {
            return db.Lkup_Item_Status.Count(e => e.ITEM_STATUS_ID == key) > 0;
        }
    }
}
