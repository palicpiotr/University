using System;
using System.Collections.Generic;
using ASSSK.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ASSSK.DataAccesLayer
{
    public class UniversityInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<UniversityContext>

    {
        protected override void Seed(UniversityContext context)
        {


        }
    }
}