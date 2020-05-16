namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WCDbContext : DbContext
    {
        public WCDbContext()
            : base("name=BookstoreDb1")
        {
        }

        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<chapter> chapters { get; set; }
        public virtual DbSet<comic> comics { get; set; }
        public virtual DbSet<comic_category> comic_category { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<document> documents { get; set; }
        public virtual DbSet<image_chapter> image_chapter { get; set; }
        public virtual DbSet<nation> nations { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<status_comic> status_comic { get; set; }
        public virtual DbSet<status_comment> status_comment { get; set; }
        public virtual DbSet<status_user> status_user { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>()
                .HasMany(e => e.comic_category)
                .WithOptional(e => e.category)
                .WillCascadeOnDelete();

            modelBuilder.Entity<chapter>()
                .HasMany(e => e.comments)
                .WithOptional(e => e.chapter)
                .WillCascadeOnDelete();

            modelBuilder.Entity<chapter>()
                .HasMany(e => e.image_chapter)
                .WithOptional(e => e.chapter)
                .WillCascadeOnDelete();

            modelBuilder.Entity<comic>()
                .HasMany(e => e.comic_category)
                .WithOptional(e => e.comic)
                .WillCascadeOnDelete();

            modelBuilder.Entity<document>()
                .HasMany(e => e.image_chapter)
                .WithOptional(e => e.document)
                .WillCascadeOnDelete();

            modelBuilder.Entity<nation>()
                .HasMany(e => e.comics)
                .WithOptional(e => e.nation)
                .WillCascadeOnDelete();

            modelBuilder.Entity<role>()
                .HasMany(e => e.users)
                .WithOptional(e => e.Role)
                .WillCascadeOnDelete();

            modelBuilder.Entity<status_comic>()
                .HasMany(e => e.comics)
                .WithOptional(e => e.status_comic)
                .WillCascadeOnDelete();

            modelBuilder.Entity<status_comment>()
                .HasMany(e => e.comments)
                .WithOptional(e => e.status_comment)
                .WillCascadeOnDelete();

            modelBuilder.Entity<status_user>()
                .HasMany(e => e.users)
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
                .HasMany(e => e.comics)
                .WithOptional(e => e.user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<user>()
                .HasMany(e => e.comments)
                .WithOptional(e => e.user)
                .WillCascadeOnDelete();
        }
    }
}
