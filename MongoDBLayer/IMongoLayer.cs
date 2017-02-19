using AzureRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLayer
{
    interface IMongoLayer
    {
        void Add(DeviceData data);


    }
}
