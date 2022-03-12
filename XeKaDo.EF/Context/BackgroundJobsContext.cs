using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.EF.Context
{
    public class BackgroundJobsContext : DbContext
    {
        public BackgroundJobsContext(DbContextOptions<BackgroundJobsContext> options) : base(options) { }
    }
}
