using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using InventoryApi;

namespace InventoryApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new AuthenticationAttributes());
            //config.Filters.Add(new AuthorizeAttribute());


            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Item>("Items");
            builder.EntitySet<IssueDetail>("IssueDetails");
            builder.EntitySet<Lkup_UOI>("Lkup_UOI");
            builder.EntitySet<OrderDetail>("OrderDetails");
            builder.EntitySet<StockDetail>("StockDetails");
            builder.EntitySet<Employee>("Employees");
            builder.EntitySet<Issue>("Issues");
            builder.EntitySet<Stock>("Stocks");
            builder.EntitySet<Lkup_Fin_Year>("Lkup_Fin_Year");
            builder.EntitySet<Lkup_Item_Status>("Lkup_Item_Status");
            builder.EntitySet<Lkup_Payment_Reason>("Lkup_Payment_Reason");
            builder.EntitySet<Lkup_Payment_Type>("Lkup_Payment_Type");
            builder.EntitySet<Lkup_Receipt_Type>("Lkup_Receipt_Type");
            builder.EntitySet<Lkup_Stock_Type>("Lkup_Stock_Type");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<Payment>("Payments");
            builder.EntitySet<StockDetail>("StockDetails");
            builder.EntitySet<Supplier>("Suppliers");
            builder.EntitySet<User>("Users");
            builder.EntitySet<Lkup_User_Roles>("Lkup_User_Roles");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
