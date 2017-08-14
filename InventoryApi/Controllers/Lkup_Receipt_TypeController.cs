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
    builder.EntitySet<Lkup_Receipt_Type>("Lkup_Receipt_Type");
    builder.EntitySet<Order>("Orders"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Lkup_Receipt_TypeController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Lkup_Receipt_Type
        [EnableQuery]
        public IQueryable<Lkup_Receipt_Type> GetLkup_Receipt_Type()
        {
            return db.Lkup_Receipt_Type.Where(Lkup_Receipt_Type => Lkup_Receipt_Type.ACTIVE == "Y");
        }

        // GET: odata/Lkup_Receipt_Type(5)
        [EnableQuery]
        public SingleResult<Lkup_Receipt_Type> GetLkup_Receipt_Type([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Lkup_Receipt_Type.Where(lkup_Receipt_Type => lkup_Receipt_Type.RECEIPT_TYPE_ID == key));
        }

        // PUT: odata/Lkup_Receipt_Type(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Lkup_Receipt_Type> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Receipt_Type lkup_Receipt_Type = db.Lkup_Receipt_Type.Find(key);
            if (lkup_Receipt_Type == null)
            {
                return NotFound();
            }

            patch.Put(lkup_Receipt_Type);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Receipt_TypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Receipt_Type);
        }

        // POST: odata/Lkup_Receipt_Type
        public IHttpActionResult Post(Lkup_Receipt_Type lkup_Receipt_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lkup_Receipt_Type.Add(lkup_Receipt_Type);
            db.SaveChanges();

            return Created(lkup_Receipt_Type);
        }

        // PATCH: odata/Lkup_Receipt_Type(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Lkup_Receipt_Type> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Receipt_Type lkup_Receipt_Type = db.Lkup_Receipt_Type.Find(key);
            if (lkup_Receipt_Type == null)
            {
                return NotFound();
            }

            patch.Patch(lkup_Receipt_Type);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Receipt_TypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Receipt_Type);
        }

        // DELETE: odata/Lkup_Receipt_Type(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Lkup_Receipt_Type lkup_Receipt_Type = db.Lkup_Receipt_Type.Find(key);
            lkup_Receipt_Type.ACTIVE = "N";
            if (lkup_Receipt_Type == null)
            {
                return NotFound();
            }

            db.Entry(lkup_Receipt_Type).State = EntityState.Modified;
            //db.Lkup_Receipt_Type.Remove(lkup_Receipt_Type);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Lkup_Receipt_Type(5)/Orders
        [EnableQuery]
        public IQueryable<Order> GetOrders([FromODataUri] decimal key)
        {
            return db.Lkup_Receipt_Type.Where(m => m.RECEIPT_TYPE_ID == key).SelectMany(m => m.Orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Lkup_Receipt_TypeExists(decimal key)
        {
            return db.Lkup_Receipt_Type.Count(e => e.RECEIPT_TYPE_ID == key) > 0;
        }
    }
}
