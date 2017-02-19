using AzureRest.Models;
using MongoDBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AzureRest.Controllers
{
    [RoutePrefix("api/devicedata")]
    [EnableCors("*", "*", "*")]
    public class DeviceDataController : ApiController
    {
        // GET api/values/5
        [HttpGet]
        [Route("bring")]

        public string Data(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("Add")]
        public void Add(Models.DeviceData data)
        {
            var x = new DeviceData { DeviceID = data.DeviceID, ValueA = data.ValueA, ValueB = data.ValueB, ValueC = data.ValueC, ValueD = data.ValueD,CreatedDateTime=DateTime.Now.ToString() };
            var y = new MongoLayer("Devices","test");
            y.Add(x);
            //return false;
        }

        [HttpGet]
        [Route("AddData")]
        public void AddData(int DeviceId, int ValueA, int ValueB, int ValueC, int ValueD)
        {
            var x = new DeviceData { DeviceID = DeviceId, ValueA = ValueA, ValueB = ValueB, ValueC = ValueC, ValueD = ValueD, CreatedDateTime = DateTime.Now.ToString() };
            var y = new MongoLayer("Devices", "test");
            y.Add(x);
            //return false;
        }

        [HttpGet]
        [Route("FilterDeviceData")]
        public List<DeviceData> DeviceData(int id)
        {
            var y = new MongoLayer("Devices","test");
            return y.GetData(id.ToString());
        }

        [HttpGet]
        [Route("DeviceList")]
        public List<string> DeviceList(string customerId)
        {
            var y = new MongoLayer("Devices","test");
            return y.GetDeviceList(customerId);
            
        }
    }
}
