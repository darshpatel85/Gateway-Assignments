using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SourceControlAss.Models
{
    public class Context : DbContext
    {
        public DbSet<Employee> employees { get; set; }

    }
}