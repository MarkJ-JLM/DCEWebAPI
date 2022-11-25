using DCEWebAPI.Models;
using DCEWebAPI.Translators;
using DCEWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DCEWebAPI.Repository
{
    public class CustomerDbClient
    {
        public List<CustomerModel> GetAllCustomer(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<CustomerModel>>(connString,
                "sp_GetCustomer", r => r.TranslateAsCustomerList());
        }

        public string InsertUpdateCustomer(CustomerModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@UserId",model.UserId),
                new SqlParameter("@Username",model.Username),
                new SqlParameter("@Email",model.Email),
                new SqlParameter("@FirstName",model.FirstName),
                new SqlParameter("@LastName",model.LastName),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "sp_InsertUpdateCustomer", param);
            return (string)outParam.Value;
        }

        public string DeleteCustomer(string UserId, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@UserId",UserId),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "sp_DeleteCustomer", param);
            return (string)outParam.Value;
        }

        public List<ActiveOrdersModel> GetAllActiveOrder(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<ActiveOrdersModel>>(connString,
                "sp_GetCustomer", r => r.TranslateAsActiveOrderList());
        }
    }
}
