using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eMotion.DomainClasses;
using eMotion.DomainClasses.Classes;

namespace eMotion.EF.Console.Display.DataLayer
{
    public class eMotionDBContext : DbContext
    {
        public eMotionDBContext() : base("eMotionDB")
        {

        }

        public DbSet<DomainClasses.Classes.Action> Action { get; set; }
        public DbSet<Command> Command { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SWMovie>().HasKey(c => c.ResourceUri);
            //base.OnModelCreating(modelBuilder); 

            //modelBuilder.Entity<User>()
            //.HasOptional<Contact>(u => u.Contact)
            //.WithOptionalDependent(c => c.User).Map(p => p.MapKey("ContactID"));

            //modelBuilder.Entity<Profile>()
            //    .HasOptional<DomainClasses.Classes.Action>(p => p.action)
            //    .WithOptionalDependent(a => a.profile).Map(p => p.MapKey("ActId"));

            modelBuilder.Entity<DomainClasses.Classes.Profile>().HasKey(p => p.ActId);

            modelBuilder.Entity<DomainClasses.Classes.Action>().HasRequired(s => s.profile).WithRequiredDependent(a => a.action);
        }
    }

    
}
