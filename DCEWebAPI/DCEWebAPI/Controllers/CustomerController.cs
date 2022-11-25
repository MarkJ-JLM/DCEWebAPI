using DCEWebAPI.Models;
using DCEWebAPI.Repository;
using DCEWebAPI.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCEWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    //[ApiController]
    public class CustomerController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;
        public CustomerController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllCustomer")]
        public IActionResult GetAllCustomer()
        {
            var data = DbClientFactory<CustomerDbClient>.Instance.GetAllCustomer(appSettings.Value.DbConn);
            return Ok(data);
        }

        [HttpPost]
        [Route("InsertUpdateCustomer")]
        public IActionResult InsertUpdateCustomer([FromBody] CustomerModel model)
        {
            var msg = new Message<CustomerModel>();
            var data = DbClientFactory<CustomerDbClient>.Instance.InsertUpdateCustomer(model, appSettings.Value.DbConn);
            if (data == "DCE100")
            {
                msg.IsSuccess = true;
                if (model.UserId == "")
                    msg.ReturnMessage = "User saved successfully";
                else
                    msg.ReturnMessage = "User updated successfully";
            }
            else if (data == "DCE101")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "User name already exists";
            }
            else if (data == "DCE102")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Email Id already exists";
            }
            return Ok(msg);
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public IActionResult DeleteCustome(string UserId)
        {
            var msg = new Message<CustomerModel>();
            var data = DbClientFactory<CustomerDbClient>.Instance.DeleteCustomer(UserId, appSettings.Value.DbConn);
            if (data == "DCE200")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "Customer Deleted";
            }
            else if (data == "DCE201")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Invalid record";
            }
            return Ok(msg);
        }

        [HttpGet]
        [Route("GetAllActiveOrder")]
        public IActionResult GetAllActiveOrder()
        {
            var data = DbClientFactory<CustomerDbClient>.Instance.GetAllActiveOrder(appSettings.Value.DbConn);
            return Ok(data);
        }
    }
}
