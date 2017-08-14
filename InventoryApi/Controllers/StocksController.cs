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
    builder.EntitySet<Stock>("Stocks");
    builder.EntitySet<Employee>("Employees"); 
    builder.EntitySet<Issue>("Issues"); 
    builder.EntitySet<Lkup_Stock_Type>("Lkup_Stock_Type"); 
    builder.EntitySet<StockDetail>("StockDetails"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StocksController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Stocks
        [EnableQuery]
        public IQueryable<Stock> GetStocks()
        {
            return db.Stocks.Where(Stock => Stock.ACTIVE == "Y");
        }

        // GET: odata/Stocks(5)
        [EnableQuery]
        public SingleResult<Stock> GetStock([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Stocks.Where(stock => stock.STOCK_ID == key));
        }

        // PUT: odata/Stocks(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Stock> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Stock stock = db.Stocks.Find(key);
            if (stock == null)
            {
                return NotFound();
            }

            patch.Put(stock);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(stock);
        }

        // POST: odata/Stocks
        public IHttpActionResult Post(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stocks.Add(stock);
            db.SaveChanges();

            return Created(stock);
        }

        // PATCH: odata/Stocks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Stock> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Stock stock = db.Stocks.Find(key);
            if (stock == null)
            {
                return NotFound();
            }

            patch.Patch(stock);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(stock);
        }

        // DELETE: odata/Stocks(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Stock stock = db.Stocks.Find(key);
            stock.ACTIVE = "N";
            if (stock == null)
            {
                return NotFound();
            }

            db.Entry(stock).State = EntityState.Modified;
            //db.Stocks.Remove(stock);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Stocks(5)/Employee
        [EnableQuery]
        public SingleResult<Employee> GetEmployee([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Stocks.Where(m => m.STOCK_ID == key).Select(m => m.Employee));
        }

        // GET: odata/Stocks(5)/Issues
        [EnableQuery]
        public IQueryable<Issue> GetIssues([FromODataUri] decimal key)
        {
            return db.Stocks.Where(m => m.STOCK_ID == key).SelectMany(m => m.Issues);
        }

        // GET: odata/Stocks(5)/Lkup_Stock_Type
        [EnableQuery]
        public SingleResult<Lkup_Stock_Type> GetLkup_Stock_Type([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Stocks.Where(m => m.STOCK_ID == key).Select(m => m.Lkup_Stock_Type));
        }

        // GET: odata/Stocks(5)/StockDetails
        [EnableQuery]
        public IQueryable<StockDetail> GetStockDetails([FromODataUri] decimal key)
        {
            return db.Stocks.Where(m => m.STOCK_ID == key).SelectMany(m => m.StockDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockExists(decimal key)
        {
            return db.Stocks.Count(e => e.STOCK_ID == key) > 0;
        }
    }
}
