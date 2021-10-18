using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AuditSeverityMicroService.RepositoryLayer
{
    public partial class AuditManagementSystemContext : DbContext
    {
        public AuditManagementSystemContext()
        {
        }

        public AuditManagementSystemContext(DbContextOptions<AuditManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditManagement> AuditManagements { get; set; }
        public virtual DbSet<Logindetail> Logindetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=AuditManagementSystem;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AuditManagement>(entity =>
            {
                entity.HasKey(e => e.ManagementId)
                    .HasName("PK__AuditMan__D21F1B36FBBC05D5");

                entity.ToTable("AuditManagement");

                entity.Property(e => e.ApplicationOwnerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AuditDate).HasColumnType("date");

                entity.Property(e => e.AuditId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AuditType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectExecutionStatus)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectManagerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RemedialActionDuration)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Logindetail>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .HasName("PK__Logindet__761ABEF0A7F3A554");

                entity.Property(e => e.ProjectId).ValueGeneratedNever();

                entity.Property(e => e.Passwrd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
