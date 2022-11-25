using DCEWebAPI.Models;
using DCEWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DCEWebAPI.Translators
{
    public static class CustomerTranslator
    {
        public static CustomerModel TranslateAsCustomer(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new CustomerModel();
            if (reader.IsColumnExists("UserId"))
                item.UserId = SqlHelper.GetNullableString(reader, "UserId");

            if (reader.IsColumnExists("Username"))
                item.Username = SqlHelper.GetNullableString(reader, "Username");

            if (reader.IsColumnExists("Email"))
                item.Email = SqlHelper.GetNullableString(reader, "Email");

            if (reader.IsColumnExists("FirstName"))
                item.FirstName = SqlHelper.GetNullableString(reader, "FirstName");

            if (reader.IsColumnExists("LastName"))
                item.LastName = SqlHelper.GetNullableString(reader, "LastName");

            if (reader.IsColumnExists("CreatedOn"))
                item.CreatedOn = SqlHelper.GetDateTime(reader, "CreatedOn");

            if (reader.IsColumnExists("IsActive"))
                item.IsActive = SqlHelper.GetBoolean(reader, "IsActive");

            return item;
        }

        public static List<CustomerModel> TranslateAsCustomerList(this SqlDataReader reader)
        {
            var list = new List<CustomerModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsCustomer(reader, true));
            }
            return list;
        }

        /////// - Active Order

        public static ActiveOrdersModel TranslateAsActiveOrder(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new ActiveOrdersModel();
            if (reader.IsColumnExists("UserId"))
                item.UserId = SqlHelper.GetNullableString(reader, "UserId");

            if (reader.IsColumnExists("FullName"))
                item.FullName = SqlHelper.GetNullableString(reader, "FullName");

            if (reader.IsColumnExists("OrderId"))
                item.OrderId = SqlHelper.GetNullableString(reader, "OrderId");

            if (reader.IsColumnExists("ProductName"))
                item.ProductName = SqlHelper.GetNullableString(reader, "ProductName");

            if (reader.IsColumnExists("OrderStatus"))
                item.OrderStatus = SqlHelper.GetNullableInt32(reader, "OrderStatus");

            if (reader.IsColumnExists("CreatedOn"))
                item.OrderType = SqlHelper.GetNullableInt32(reader, "OrderType");

            if (reader.IsColumnExists("OrderedOn"))
                item.OrderedOn = SqlHelper.GetDateTime(reader, "OrderedOn");

            if (reader.IsColumnExists("ShippedOn"))
                item.ShippedOn = SqlHelper.GetDateTime(reader, "ShippedOn");

            if (reader.IsColumnExists("SupplierName"))
                item.SupplierName = SqlHelper.GetNullableString(reader, "SupplierName");

            return item;
        }

        public static List<ActiveOrdersModel> TranslateAsActiveOrderList(this SqlDataReader reader)
        {
            var list = new List<ActiveOrdersModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsActiveOrder(reader, true));
            }
            return list;
        }
    }
}
