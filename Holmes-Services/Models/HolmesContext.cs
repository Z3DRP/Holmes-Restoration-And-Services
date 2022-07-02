using Holmes_Services.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Holmes_Services.Models
{
    public class HolmesContext : DbContext
    {
        public HolmesContext(DbContextOptions<HolmesContext> options)
           : base(options) { }

        public DbSet<Deck_Type> Deck_Types { get; set; }
        public DbSet<Rail_Type> Rail_Types { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Price_Groups> Price_Groups { get; set; }
        public DbSet<Decking> Deckings { get; set; }
        public DbSet<Railing> Railings { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<Job> Jobs { get; set; }



        protected override void OnModelCreating(ModelBuilder model)
        {
            //// set primary keys and foreign keys for tables

            // customers
            model.Entity<Customer>().HasKey(c => new { c.Id });
            //deck types 
            model.Entity<Deck_Type>().HasKey(d => new { d.Id });

            model.Entity<Deck_Type>().HasMany(dt => dt.Deckings)
                .WithOne(d => d.Type).HasForeignKey(d => d.Type_Id)
                .HasPrincipalKey(dt => dt.Type_Code);

            // use this if this current version dont work
            //model.Entity<Deck_Type>().HasOne(t => t.Type)
            //    .WithOne().HasForeignKey<Decking>(d => d.Type_Id)
            //    .HasPrincipalKey<Deck_Type>(d => d.Type_Code);

            //rail types
            model.Entity<Rail_Type>().HasKey(r => new { r.Id });

            model.Entity<Rail_Type>().HasMany(rt => rt.Railings)
                .WithOne(r => r.Type).HasForeignKey(r => r.Type_Id)
                .HasPrincipalKey(rt => rt.Type_Code);

            //model.Entity<Rail_Type>().HasOne(r => r.Type)
            //    .WithOne().HasForeignKey(rt => rt.Type_Id)
            //    .HasPrincipalKey<Rail_Type>(r => r.Type_Code);

            // price groups
            model.Entity<Price_Groups>().HasKey(g => new { g.Id });

            // way book said to configure a non primary key matching foreign key
            //model.Entity<Price_Groups>().HasOne(p => p.Decks)
            //    .WithOne().HasForeignKey<Decking>(p => p.Group_Id)
            //    .HasPrincipalKey<Price_Groups>(p => p.GroupId);

            model.Entity<Price_Groups>().HasMany(pg => pg.Decks)
                .WithOne(p => p.Group).HasForeignKey(p => p.Group_Id)
                .HasPrincipalKey(pg => pg.GroupId);

            model.Entity<Price_Groups>().HasMany(pg => pg.Rails)
                .WithOne(p => p.Group).HasForeignKey(p => p.Group_Id)
                .HasPrincipalKey(pg => pg.GroupId);

            //deckings
            model.Entity<Decking>().HasKey(d => new { d.Id });

            //model.Entity<Decking>().HasOne(t => t.Type)
            //   .WithMany().HasForeignKey(d => d.Type_Id);

            //railings 
            model.Entity<Railing>().HasKey(r => new { r.Id });

            //model.Entity<Railing>().HasOne(r => r.Type)
            //    .WithMany(rt => rt.Railings).HasForeignKey(r => r.Type_Id);

            //designs
            model.Entity<Design>().HasKey(d => new { d.Id });

            model.Entity<Design>().HasOne(c => c.Customer)
                .WithMany(c => c.Designs).HasForeignKey(d => d.Customer_Id);

            //model.Entity<Design>().HasOne(c => c.Customer)
            //    .WithMany(d => d.Designs).HasForeignKey(d => d.Customer_Id);

            model.Entity<Design>().HasOne(d => d.Deck)
                .WithMany(d => d.Designs).HasForeignKey(d => d.Decking_Id);

            model.Entity<Design>().HasOne(r => r.Rail)
                .WithMany(r => r.Designs).HasForeignKey(r => r.Railing_Id);

            //jobs
            model.Entity<Job>().HasKey(j => new { j.Id });

            model.Entity<Job>().HasOne(d => d.Design)
                .WithMany(dj => dj.Jobs).HasForeignKey(dj => dj.Design_Id);

            model.Entity<Job>().HasOne(c => c.Customer)
                .WithMany(c => c.Jobs).HasForeignKey(cj => cj.Customer_Id);

        }
    }
}
