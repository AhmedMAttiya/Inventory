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
    builder.EntitySet<Issue>("Issues");
    builder.EntitySet<Employee>("Employees"); 
    builder.EntitySet<Lkup_Stock_Type>("Lkup_Stock_Type"); 
    builder.EntitySet<Stock>("Stocks"); 
    builder.EntitySet<IssueDetail>("IssueDetails"); 
    builder.EntitySet<Lkup_Fin_Year>("Lkup_Fin_Year"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class IssuesController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Issues
        [EnableQuery]
        public IQueryable<Issue> GetIssues()
        {
            return db.Issues.Where(Issue => Issue.ACTIVE == "Y"); 
        }

        // GET: odata/Issues(5)
        [EnableQuery]
        public SingleResult<Issue> GetIssue([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Issues.Where(issue => issue.ISSUE_ID == key));
        }

        // PUT: odata/Issues(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Issue> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Issue issue = db.Issues.Find(key);
            if (issue == null)
            {
                return NotFound();
            }

            patch.Put(issue);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(issue);
        }

        // POST: odata/Issues
        public IHttpActionResult Post(Issue issue,Stock stock,StockDetail StockDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Issues.Add(issue);
            db.Stocks.Add(stock);
            db.StockDetails.Add(StockDetail);
            db.SaveChanges();

            return Created(issue);
        }

        // PATCH: odata/Issues(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Issue> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Issue issue = db.Issues.Find(key);
            if (issue == null)
            {
                return NotFound();
            }

            patch.Patch(issue);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(issue);
        }

        // DELETE: odata/Issues(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Issue issue = db.Issues.Find(key);
            issue.ACTIVE = "N";
            if (issue == null)
            {
                return NotFound();
            }

            db.Entry(issue).State = EntityState.Modified;
            //db.Issues.Remove(issue);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Issues(5)/Employee
        [EnableQuery]
        public SingleResult<Employee> GetEmployee([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Issues.Where(m => m.ISSUE_ID == key).Select(m => m.Employee));
        }

        // GET: odata/Issues(5)/Lkup_Stock_Type
        [EnableQuery]
        public SingleResult<Lkup_Stock_Type> GetLkup_Stock_Type([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Issues.Where(m => m.ISSUE_ID == key).Select(m => m.Lkup_Stock_Type));
        }

        // GET: odata/Issues(5)/Stock
        [EnableQuery]
        public SingleResult<Stock> GetStock([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Issues.Where(m => m.ISSUE_ID == key).Select(m => m.Stock));
        }

        // GET: odata/Issues(5)/IssueDetails
        [EnableQuery]
        public IQueryable<IssueDetail> GetIssueDetails([FromODataUri] decimal key)
        {
            return db.Issues.Where(m => m.ISSUE_ID == key).SelectMany(m => m.IssueDetails);
        }

        // GET: odata/Issues(5)/Lkup_Fin_Year
        [EnableQuery]
        public SingleResult<Lkup_Fin_Year> GetLkup_Fin_Year([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Issues.Where(m => m.ISSUE_ID == key).Select(m => m.Lkup_Fin_Year));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IssueExists(decimal key)
        {
            return db.Issues.Count(e => e.ISSUE_ID == key) > 0;
        }
    }
}
