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
    builder.EntitySet<Payment>("Payments");
    builder.EntitySet<Lkup_Payment_Reason>("Lkup_Payment_Reason"); 
    builder.EntitySet<Lkup_Fin_Year>("Lkup_Fin_Year"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PaymentsController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Payments
        [EnableQuery]
        public IQueryable<Payment> GetPayments()
        {
            return db.Payments.Where(Payment => Payment.ACTIVE == "Y");
        }

        // GET: odata/Payments(5)
        [EnableQuery]
        public SingleResult<Payment> GetPayment([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Payments.Where(payment => payment.PAYMENT_ID == key));
        }

        // PUT: odata/Payments(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Payment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Payment payment = db.Payments.Find(key);
            if (payment == null)
            {
                return NotFound();
            }

            patch.Put(payment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(payment);
        }

        // POST: odata/Payments
        public IHttpActionResult Post(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(payment);
            db.SaveChanges();

            return Created(payment);
        }

        // PATCH: odata/Payments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Payment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Payment payment = db.Payments.Find(key);
            if (payment == null)
            {
                return NotFound();
            }

            patch.Patch(payment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(payment);
        }

        // DELETE: odata/Payments(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Payment payment = db.Payments.Find(key);
            payment.ACTIVE = "N";
            if (payment == null)
            {
                return NotFound();
            }

            db.Entry(payment).State = EntityState.Modified;
            //db.Payments.Remove(payment);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Payments(5)/Lkup_Payment_Reason
        [EnableQuery]
        public SingleResult<Lkup_Payment_Reason> GetLkup_Payment_Reason([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Payments.Where(m => m.PAYMENT_ID == key).Select(m => m.Lkup_Payment_Reason));
        }

        // GET: odata/Payments(5)/Lkup_Fin_Year
        [EnableQuery]
        public SingleResult<Lkup_Fin_Year> GetLkup_Fin_Year([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Payments.Where(m => m.PAYMENT_ID == key).Select(m => m.Lkup_Fin_Year));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentExists(decimal key)
        {
            return db.Payments.Count(e => e.PAYMENT_ID == key) > 0;
        }
    }
}
