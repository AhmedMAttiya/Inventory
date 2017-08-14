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
    builder.EntitySet<Employee>("Employees");
    builder.EntitySet<Issue>("Issues"); 
    builder.EntitySet<Stock>("Stocks"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    [AuthenticationAttributes]

    public class EmployeesController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Employees
        [EnableQuery]
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees.Where(Employee => Employee.ACTIVE == "Y");
        }

        // GET: odata/Employees(5)
        [EnableQuery]
        public SingleResult<Employee> GetEmployee([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Employees.Where(employee => employee.EMPLOYEE_ID == key));
        }

        // PUT: odata/Employees(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Employee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = db.Employees.Find(key);
            if (employee == null)
            {
                return NotFound();
            }

            patch.Put(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employee);
        }

        // POST: odata/Employees
        public IHttpActionResult Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return Created(employee);
        }

        // PATCH: odata/Employees(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Employee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = db.Employees.Find(key);
            if (employee == null)
            {
                return NotFound();
            }

            patch.Patch(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employee);
        }

        // DELETE: odata/Employees(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Employee employee = db.Employees.Find(key);
            employee.ACTIVE = "N";
            if (employee == null)
            {
                return NotFound();
            }

            db.Entry(employee).State = EntityState.Modified;
            //db.Employees.Remove(employee);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Employees(5)/Issues
        [EnableQuery]
        public IQueryable<Issue> GetIssues([FromODataUri] decimal key)
        {
            return db.Employees.Where(m => m.EMPLOYEE_ID == key).SelectMany(m => m.Issues);
        }

        // GET: odata/Employees(5)/Stocks
        [EnableQuery]
        public IQueryable<Stock> GetStocks([FromODataUri] decimal key)
        {
            return db.Employees.Where(m => m.EMPLOYEE_ID == key).SelectMany(m => m.Stocks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(decimal key)
        {
            return db.Employees.Count(e => e.EMPLOYEE_ID == key) > 0;
        }
    }
}
