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
    builder.EntitySet<Lkup_Stock_Type>("Lkup_Stock_Type");
    builder.EntitySet<Issue>("Issues"); 
    builder.EntitySet<Stock>("Stocks"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Lkup_Stock_TypeController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Lkup_Stock_Type
        [EnableQuery]
        public IQueryable<Lkup_Stock_Type> GetLkup_Stock_Type()
        {
            return db.Lkup_Stock_Type.Where(Lkup_Stock_Type => Lkup_Stock_Type.ACTIVE == "Y");
        }

        // GET: odata/Lkup_Stock_Type(5)
        [EnableQuery]
        public SingleResult<Lkup_Stock_Type> GetLkup_Stock_Type([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Lkup_Stock_Type.Where(lkup_Stock_Type => lkup_Stock_Type.STOCK_TYPE_ID == key));
        }

        // PUT: odata/Lkup_Stock_Type(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Lkup_Stock_Type> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Stock_Type lkup_Stock_Type = db.Lkup_Stock_Type.Find(key);
            if (lkup_Stock_Type == null)
            {
                return NotFound();
            }

            patch.Put(lkup_Stock_Type);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Stock_TypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Stock_Type);
        }

        // POST: odata/Lkup_Stock_Type
        public IHttpActionResult Post(Lkup_Stock_Type lkup_Stock_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lkup_Stock_Type.Add(lkup_Stock_Type);
            db.SaveChanges();

            return Created(lkup_Stock_Type);
        }

        // PATCH: odata/Lkup_Stock_Type(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Lkup_Stock_Type> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_Stock_Type lkup_Stock_Type = db.Lkup_Stock_Type.Find(key);
            if (lkup_Stock_Type == null)
            {
                return NotFound();
            }

            patch.Patch(lkup_Stock_Type);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_Stock_TypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_Stock_Type);
        }

        // DELETE: odata/Lkup_Stock_Type(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Lkup_Stock_Type lkup_Stock_Type = db.Lkup_Stock_Type.Find(key);
            lkup_Stock_Type.ACTIVE = "N";
            if (lkup_Stock_Type == null)
            {
                return NotFound();
            }

            db.Entry(lkup_Stock_Type).State = EntityState.Modified;
            //db.Lkup_Stock_Type.Remove(lkup_Stock_Type);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Lkup_Stock_Type(5)/Issues
        [EnableQuery]
        public IQueryable<Issue> GetIssues([FromODataUri] decimal key)
        {
            return db.Lkup_Stock_Type.Where(m => m.STOCK_TYPE_ID == key).SelectMany(m => m.Issues);
        }

        // GET: odata/Lkup_Stock_Type(5)/Stocks
        [EnableQuery]
        public IQueryable<Stock> GetStocks([FromODataUri] decimal key)
        {
            return db.Lkup_Stock_Type.Where(m => m.STOCK_TYPE_ID == key).SelectMany(m => m.Stocks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Lkup_Stock_TypeExists(decimal key)
        {
            return db.Lkup_Stock_Type.Count(e => e.STOCK_TYPE_ID == key) > 0;
        }
    }
}
