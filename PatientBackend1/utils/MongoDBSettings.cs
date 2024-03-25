using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientBackend1.utils
{
    public class MongoDBSettings
    {
        public string ConnectionUrl { get; set; } = "";
        public string DBName { get; set; } = "";
    }
}