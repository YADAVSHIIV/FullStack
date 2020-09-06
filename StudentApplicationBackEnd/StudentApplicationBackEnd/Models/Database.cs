using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplicationBackEnd.Models
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> dbContextOptions) : base(dbContextOptions)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<tbl_Student>(entity =>
            {
                entity.HasKey(prop => prop.ID).HasName("PK_tbl_Students");

                entity.Property(prop => prop.ID).ValueGeneratedOnAdd().IsRequired();
                entity.Property(prop => prop.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(prop => prop.LastName).HasMaxLength(500).IsRequired();
                entity.Property(prop => prop.Class).IsRequired();
                entity.HasMany<tbl_Mark>(s=>s.tbl_Marks).WithOne(g=>g.tbl_Student).IsRequired().HasForeignKey(s => s.StudentID).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_tbl_Students_tbl_Marks"); 

            });

            modelBuilder.Entity<tbl_Subject>(entity =>
            {
                entity.HasKey(prop => prop.ID).HasName("PK_tbl_Subjects");
                entity.HasIndex(prop => prop.Name).HasName("IDX_tbl_Subjects_Name").IsUnique();

                entity.Property(prop => prop.ID).ValueGeneratedOnAdd().IsRequired();
                entity.Property(prop => prop.Name).HasMaxLength(50).IsRequired();

                entity.HasMany<tbl_Mark>(s => s.tbl_Marks).WithOne(g => g.tbl_Subject).IsRequired().HasForeignKey(s => s.StudentID).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_tbl_Subjects_tbl_Marks");

                entity.HasData(new tbl_Subject {ID=1, Name = "Math" }, new tbl_Subject {ID=2, Name = "Physics" }, new tbl_Subject {ID=3, Name = "Chemistry" });
            }); 
            
            modelBuilder.Entity<tbl_Mark>(entity =>
            {
                entity.HasKey(prop => prop.ID).HasName("PK_tbl_Marks");

                entity.Property(prop => prop.ID).ValueGeneratedOnAdd().IsRequired();
                entity.Property(prop => prop.StudentID).IsRequired();
                entity.Property(prop => prop.SubjectID).IsRequired();
                entity.Property(prop => prop.StudentMark).IsRequired();
            });


        }

       public DbSet<tbl_Student> tbl_Students { get; set; }
       public DbSet<tbl_Subject> tbl_Subjects { get; set; }
       public DbSet<tbl_Mark> tbl_Marks { get; set; }

    }
}
