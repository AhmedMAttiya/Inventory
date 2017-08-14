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
    builder.EntitySet<Item>("Items");
    builder.EntitySet<Category>("Categories"); 
    builder.EntitySet<IssueDetail>("IssueDetails"); 
    builder.EntitySet<Lkup_UOI>("Lkup_UOI"); 
    builder.EntitySet<OrderDetail>("OrderDetails"); 
    builder.EntitySet<StockDetail>("StockDetails"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ItemsController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Items
        [EnableQuery]
        public IQueryable<Item> GetItems()
        {
            return db.Items.Where(Item => Item.ACTIVE == "Y"); 
        }

        // GET: odata/Items(5)
        [EnableQuery]
        public SingleResult<Item> GetItem([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Items.Where(item => item.ITEM_ID == key));
        }

        // PUT: odata/Items(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Item> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Item item = db.Items.Find(key);
            if (item == null)
            {
                return NotFound();
            }

            patch.Put(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(item);
        }

        // POST: odata/Items
        public IHttpActionResult Post(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            db.SaveChanges();

            return Created(item);
        }

        // PATCH: odata/Items(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Item> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Item item = db.Items.Find(key);
            if (item == null)
            {
                return NotFound();
            }

            patch.Patch(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(item);
        }

        // DELETE: odata/Items(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Item item = db.Items.Find(key);
            item.ACTIVE = "N";
            if (item == null)
            {
                return NotFound();
            }

            db.Entry(item).State = EntityState.Modified;
            //db.Items.Remove(item);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Items(5)/Category
        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Items.Where(m => m.ITEM_ID == key).Select(m => m.Category));
        }

        // GET: odata/Items(5)/IssueDetails
        [EnableQuery]
        public IQueryable<IssueDetail> GetIssueDetails([FromODataUri] decimal key)
        {
            return db.Items.Where(m => m.ITEM_ID == key).SelectMany(m => m.IssueDetails);
        }

        // GET: odata/Items(5)/Lkup_UOI
        [EnableQuery]
        public SingleResult<Lkup_UOI> GetLkup_UOI([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Items.Where(m => m.ITEM_ID == key).Select(m => m.Lkup_UOI));
        }

        // GET: odata/Items(5)/OrderDetails
        [EnableQuery]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri] decimal key)
        {
            return db.Items.Where(m => m.ITEM_ID == key).SelectMany(m => m.OrderDetails);
        }

        // GET: odata/Items(5)/StockDetails
        [EnableQuery]
        public IQueryable<StockDetail> GetStockDetails([FromODataUri] decimal key)
        {
            return db.Items.Where(m => m.ITEM_ID == key).SelectMany(m => m.StockDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(decimal key)
        {
            return db.Items.Count(e => e.ITEM_ID == key) > 0;
        }
    }
}
