using Kemrex.Core.Common.Interfaces;
using Kemrex.Core.Database;
using Kemrex.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kemrex.Core.Common.Modules
{
    public class TeamModule
    {
        private readonly mainContext db;
        public TeamModule(mainContext context)
        {
            db = context;
        }



        
    }
}
