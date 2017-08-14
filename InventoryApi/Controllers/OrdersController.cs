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
    builder.EntitySet<Order>("Orders");
    builder.EntitySet<Lkup_Payment_Type>("Lkup_Payment_Type"); 
    builder.EntitySet<Lkup_Receipt_Type>("Lkup_Receipt_Type"); 
    builder.EntitySet<Supplier>("Suppliers"); 
    builder.EntitySet<OrderDetail>("OrderDetails"); 
    builder.EntitySet<Lkup_Fin_Year>("Lkup_Fin_Year"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrdersController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Orders
        [EnableQuery]
        public IQueryable<Order> GetOrders()
        {
            return db.Orders.Where(Order => Order.ACTIVE == "Y");
        }

        // GET: odata/Orders(5)
        [EnableQuery]
        public SingleResult<Order> GetOrder([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Orders.Where(order => order.ORDER_ID == key));
        }

        // PUT: odata/Orders(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Order> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = db.Orders.Find(key);
            if (order == null)
            {
                return NotFound();
            }

            patch.Put(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(order);
        }

        // POST: odata/Orders
        public IHttpActionResult Post(Order order, Stock stock, StockDetail StockDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.Stocks.Add(stock);
            db.StockDetails.Add(StockDetail);
            db.SaveChanges();

            return Created(order);
        }

        // PATCH: odata/Orders(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Order> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = db.Orders.Find(key);
            if (order == null)
            {
                return NotFound();
            }

            patch.Patch(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(order);
        }

        // DELETE: odata/Orders(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Order order = db.Orders.Find(key);
            order.ACTIVE = "N";
            if (order == null)
            {
                return NotFound();
            }

            db.Entry(order).State = EntityState.Modified;
            //db.Orders.Remove(order);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Orders(5)/Lkup_Payment_Type
        [EnableQuery]
        public SingleResult<Lkup_Payment_Type> GetLkup_Payment_Type([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Orders.Where(m => m.ORDER_ID == key).Select(m => m.Lkup_Payment_Type));
        }

        // GET: odata/Orders(5)/Lkup_Receipt_Type
        [EnableQuery]
        public SingleResult<Lkup_Receipt_Type> GetLkup_Receipt_Type([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Orders.Where(m => m.ORDER_ID == key).Select(m => m.Lkup_Receipt_Type));
        }

        // GET: odata/Orders(5)/Supplier1
        [EnableQuery]
        public SingleResult<Supplier> GetSupplier1([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Orders.Where(m => m.ORDER_ID == key).Select(m => m.Supplier1));
        }

        // GET: odata/Orders(5)/OrderDetails
        [EnableQuery]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri] decimal key)
        {
            return db.Orders.Where(m => m.ORDER_ID == key).SelectMany(m => m.OrderDetails);
        }

        // GET: odata/Orders(5)/Lkup_Fin_Year
        [EnableQuery]
        public SingleResult<Lkup_Fin_Year> GetLkup_Fin_Year([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Orders.Where(m => m.ORDER_ID == key).Select(m => m.Lkup_Fin_Year));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(decimal key)
        {
            return db.Orders.Count(e => e.ORDER_ID == key) > 0;
        }
    }
}
