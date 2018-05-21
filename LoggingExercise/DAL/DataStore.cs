using LoggingExercise.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggingExercise.DAL
{
    public class DataStore:IdentityDbContext<AppUser,AppRole,int,AppUserLogin,AppUserRole,AppUserClaim>
    {
        public DataStore():base($"name={nameof(DataStore)}")
        {

        }
    }
}