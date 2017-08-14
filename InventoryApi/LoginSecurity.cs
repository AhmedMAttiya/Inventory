using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryApi
{
    public class LoginSecurity
    {
        public static bool login(string username, string password)
        {
            using (Inventory_SystemEntities inventities = new Inventory_SystemEntities())
                return inventities.Users.Any(User => User.USER_NAME.Equals(username, StringComparison.OrdinalIgnoreCase)
                && User.PASSWORD == password);
        }
    }
}