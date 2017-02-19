using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureRest.Models
{
    public class DeviceData
    {
        //[BsonElement("DeviceID")]
        public int DeviceID { get; set; }
        //[BsonElement("ValueA")]

        public int ValueA { get; set; }
        //[BsonElement("ValueB")]

        public int ValueB { get; set; }
        //[BsonElement("ValueC")]

        public int ValueC { get; set; }
        //[BsonElement("ValueD")]

        public int ValueD { get; set; }
        //[BsonElement("CreatedDateTime")]

        public string CreatedDateTime { get; set; }
    }
}