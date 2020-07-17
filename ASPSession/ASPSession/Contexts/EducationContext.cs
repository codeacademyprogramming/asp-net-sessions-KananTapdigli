namespace ASPSession.Contexts
{
    using ASPSession.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EducationContext : DbContext
    {
        
        public EducationContext()
            : base("name=EducationContext")
        {

        }


        public DbSet<User> users { get; set; }
    }

   
}