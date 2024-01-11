using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Sozilan.Data.Model;

namespace Sozilan.Data.DataContext
{
    public class ManagementSystemContext : DbContext
    {
        public ManagementSystemContext()
        {
            Database.Connection.ConnectionString = "Server=EROLAKGUL\\BESIKTAS;Database=SozilanDb;uid=sa;pwd=istanbul;";
        }
        public DbSet<Entry> Enter { get; set; }
        public DbSet<Title> Title { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Notification> Notification { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            //FLUENT APİ
            //modelBuilder.Entity<Page>()
            //       .HasRequired<PageTemplate>(s => s.PageTemplates) // Student entity requires Standard 
            //       .WithMany(s => s.Pages); // Standard entity includes many Students entities

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
// Introducing FOREIGN KEY constraint 'FK_dbo.Title_dbo.User_UserID' on table 'Title' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
//Could not create constraint or index. See previous errors. HATASI İÇİN AŞAĞIDAKİ SATIR EKLENDİ
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
