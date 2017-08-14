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
    builder.EntitySet<Lkup_User_Roles>("Lkup_User_Roles");
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Lkup_User_RolesController : ODataController
    {
        private Inventory_SystemEntities db = new Inventory_SystemEntities();

        // GET: odata/Lkup_User_Roles
        [EnableQuery]
        public IQueryable<Lkup_User_Roles> GetLkup_User_Roles()
        {
            return db.Lkup_User_Roles.Where(Lkup_User_Roles => Lkup_User_Roles.ACTIVE == "Y");
        }

        // GET: odata/Lkup_User_Roles(5)
        [EnableQuery]
        public SingleResult<Lkup_User_Roles> GetLkup_User_Roles([FromODataUri] decimal key)
        {
            return SingleResult.Create(db.Lkup_User_Roles.Where(lkup_User_Roles => lkup_User_Roles.ROLE_ID == key));
        }

        // PUT: odata/Lkup_User_Roles(5)
        public IHttpActionResult Put([FromODataUri] decimal key, Delta<Lkup_User_Roles> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_User_Roles lkup_User_Roles = db.Lkup_User_Roles.Find(key);
            if (lkup_User_Roles == null)
            {
                return NotFound();
            }

            patch.Put(lkup_User_Roles);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_User_RolesExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_User_Roles);
        }

        // POST: odata/Lkup_User_Roles
        public IHttpActionResult Post(Lkup_User_Roles lkup_User_Roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lkup_User_Roles.Add(lkup_User_Roles);
            db.SaveChanges();

            return Created(lkup_User_Roles);
        }

        // PATCH: odata/Lkup_User_Roles(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] decimal key, Delta<Lkup_User_Roles> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lkup_User_Roles lkup_User_Roles = db.Lkup_User_Roles.Find(key);
            if (lkup_User_Roles == null)
            {
                return NotFound();
            }

            patch.Patch(lkup_User_Roles);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Lkup_User_RolesExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lkup_User_Roles);
        }

        // DELETE: odata/Lkup_User_Roles(5)
        public IHttpActionResult Delete([FromODataUri] decimal key)
        {
            Lkup_User_Roles lkup_User_Roles = db.Lkup_User_Roles.Find(key);
            lkup_User_Roles.ACTIVE = "N";
            if (lkup_User_Roles == null)
            {
                return NotFound();
            }

            db.Entry(lkup_User_Roles).State = EntityState.Modified;
            //db.Lkup_User_Roles.Remove(lkup_User_Roles);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Lkup_User_Roles(5)/Users
        [EnableQuery]
        public IQueryable<User> GetUsers([FromODataUri] decimal key)
        {
            return db.Lkup_User_Roles.Where(m => m.ROLE_ID == key).SelectMany(m => m.Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Lkup_User_RolesExists(decimal key)
        {
            return db.Lkup_User_Roles.Count(e => e.ROLE_ID == key) > 0;
        }
    }
}
