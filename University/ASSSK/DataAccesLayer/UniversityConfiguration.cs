using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Infrastructure.Interception;

namespace ASSSK.DataAccesLayer
{
    public class UniversityConfiguration : DbConfiguration
    {
        public UniversityConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
          //  DbInterception.Add(new UniversityInterceptorTransientErrors());
          // DbInterception.Add(new UniversityInterceptorLogging());
        }
    }
}