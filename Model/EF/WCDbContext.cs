namespace up_down.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class WCDbContext : DbContext
    {
        public WCDbContext()
            : base("name=BookstoreDb1")
        {
        }

        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<comic> comic { get; set; }
        public virtual DbSet<comic_category> comic_category { get; set; }
        public virtual DbSet<comment> comment { get; set; }
        public virtual DbSet<chapter> chapter { get; set; }
        public virtual DbSet<devolution> devolution { get; set; }
        public virtual DbSet<document> document { get; set; }
        public virtual DbSet<image_chapter> image_chapter { get; set; }
        public virtual DbSet<nation> nation { get; set; }
        public virtual DbSet<status_comic> status_comic { get; set; }
        public virtual DbSet<status_comment> status_comment { get; set; }
        public virtual DbSet<status_user> status_user { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>()
                .HasMany(e => e.comic_category)
                .WithOptional(e => e.category)
                .WillCascadeOnDelete();

            modelBuilder.Entity<comic>()
                .HasMany(e => e.comic_category)
                .WithOptional(e => e.comic)
                .WillCascadeOnDelete();

            modelBuilder.Entity<chapter>()
                .HasMany(e => e.comment)
                .WithOptional(e => e.chapter)
                .WillCascadeOnDelete();

            modelBuilder.Entity<chapter>()
                .HasMany(e => e.image_chapter)
                .WithOptional(e => e.chapter)
                .WillCascadeOnDelete();

            modelBuilder.Entity<devolution>()
                .HasMany(e => e.user)
                .WithOptional(e => e.devolution)
                .WillCascadeOnDelete();

            modelBuilder.Entity<document>()
                .HasMany(e => e.image_chapter)
                .WithOptional(e => e.document)
                .WillCascadeOnDelete();

            modelBuilder.Entity<nation>()
                .HasMany(e => e.comic)
                .WithOptional(e => e.nation)
                .WillCascadeOnDelete();

            modelBuilder.Entity<status_comic>()
                .HasMany(e => e.comic)
                .WithOptional(e => e.status_comic)
                .WillCascadeOnDelete();

            modelBuilder.Entity<status_comment>()
                .HasMany(e => e.comment)
                .WithOptional(e => e.status_comment)
                .WillCascadeOnDelete();

            modelBuilder.Entity<status_user>()
                .HasMany(e => e.user)
                .WithOptional(e => e.status_user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<user>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.UserPass)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.UserMail)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.comic)
                .WithOptional(e => e.user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<user>()
                .HasMany(e => e.comment)
                .WithOptional(e => e.user)
                .WillCascadeOnDelete();
        }
    }
}
