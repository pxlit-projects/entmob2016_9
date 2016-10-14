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
            modelBuilder.Entity<DomainClasses.Classes.Profile>().HasKey(p => p.ActId);

            modelBuilder.Entity<DomainClasses.Classes.Action>().HasRequired(s => s.profile).WithRequiredDependent(a => a.action);

            modelBuilder.Entity<Command>().HasMany(s => s.profiles);

            modelBuilder.Entity<User>().HasMany(s => s.profiles);
        }
    }
}
